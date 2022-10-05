using SiliconSpecter.FullBodyTracking.Common;
using SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop;
using SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.FaceTrackLib;
using SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kernel32;
using SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10;
using SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10.CoordinateMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360
{

  /// <summary>
  /// Provides full-body tracking from a Kinect.
  /// </summary>
  public sealed class Kinect : ISource<uint>
  {
    private IFaceTrackLib _faceTrackLib;
    private IKernel32 _kernel32;
    private ISensor _sensor;
    private bool _sensorInitialized;
    private ICoordinateMapper _coordinateMapper;
    private IFaceTracker _faceTracker;
    private IImage _colorImage;
    private IImage _depthImage;
    private IResult[] _results;
    private DepthToColorDelegate _depthToColorDelegate;
    private IntPtr _depthToColorFunctionPointer;
    private string _workerError;
    private IntPtr _colorStream;
    private IntPtr _depthStream;
    private bool _stopWorker = false;
    private Thread _worker;
    private Dictionary<uint, Player> _players = new Dictionary<uint, Player>();
    private HashSet<uint> _identifiersOfPlayersTrackedInDetail = new HashSet<uint>();
    private readonly object _lock = new Object();

    private int DepthToColorCallback(uint depthFrameWidth, uint depthFrameHeight, uint colorFrameWidth, uint colorFrameHeight, float zoomFactor, Point viewOffset, int depthX, int depthY, ushort depthZ, out int colorX, out int colorY)
    {
      var output = -1;

      try
      {
        DepthImagePoint depthImagePoint = new DepthImagePoint
        {
          X = depthX,
          Y = depthY,
          Depth = depthZ,
        };

        output = _coordinateMapper.MapDepthPointToColorPoint(ImageResolution.ThreeHundredAndTwentyByTwoHundredAndForty, ref depthImagePoint, ImageType.Color, ImageResolution.SixHundredAndFortyByFourHundredAndEighty, out var colorPoint);

        colorX = colorPoint.X;
        colorY = colorPoint.Y;
      }
      catch
      {
        colorX = 0;
        colorY = 0;
      }

      return output;
    }

    private void Worker()
    {
      var playersToUpsertByIdentifiers = new Dictionary<uint, Player>();
      var identifiersOfPlayersToRemoveOrAdded = new HashSet<uint>();
      var resultsByPlayerIdentifiers = new Dictionary<uint, IResult>();
      var animationUnitCoefficients = new float[(int)AnimationUnit.Count];
      var identifiersOfPlayersTrackedInDetail = new uint[2];

      try
      {
        while (!_stopWorker)
        {
          var skeletonFrame = default(SkeletonFrame);

          var getNextFrameResult = _sensor.NuiSkeletonGetNextFrame(0, ref skeletonFrame);

          if (getNextFrameResult == 0)
          {
            if (_faceTracker == null)
            {
              _faceTracker = _faceTrackLib.FTCreateFaceTracker(IntPtr.Zero);

              if (_faceTracker == null)
              {
                throw new Exception("Failed to create a face tracker.");
              }

              var colorCameraConfig = new CameraConfiguration { Width = 640, Height = 480, FocalLength = 531.15f };
              var depthCameraConfig = new CameraConfiguration { Width = 320, Height = 240, FocalLength = 285.63f };

              var initializeFaceTrackerResult = _faceTracker.Initialize(colorCameraConfig, depthCameraConfig, _depthToColorFunctionPointer, null);

              if (initializeFaceTrackerResult != 0)
              {
                throw new Exception($"Failed to initialize the face tracker; error code {initializeFaceTrackerResult}.");
              }

              _colorImage = _faceTrackLib.FTCreateImage();

              if (_colorImage == null)
              {
                throw new Exception("Failed to create a color image for face tracking.");
              }

              _colorImage.Allocate(640, 480, ImageFormat.Blue8Green8Red8Reserved8);

              _depthImage = _faceTrackLib.FTCreateImage();

              if (_depthImage == null)
              {
                throw new Exception("Failed to create a depth image for face tracking.");
              }

              _depthImage.Allocate(320, 240, ImageFormat.Depth13UserIdentifier3);

              _results = new IResult[SkeletonFrame.NumberOfSkeletonsTrackedInDetail];

              for (var i = 0; i < _results.Length; i++)
              {
                var createFaceTrackerResultResult = _faceTracker.CreateFTResult(out _results[i]);

                if (createFaceTrackerResultResult != 0)
                {
                  throw new Exception($"Failed to create a face tracking result; error code {createFaceTrackerResultResult}.");
                }
              }
            }

            var colorImageFrameResult = _sensor.NuiImageStreamGetNextFrame(_colorStream, 0, out var colorImageFrame);

            if (colorImageFrameResult == 0)
            {
              try
              {
                var lockedArea = default(LockedArea);
                var rect = IntPtr.Zero;

                var lockResult = colorImageFrame.FrameTexture.LockRect(0, ref lockedArea, rect, 0);

                if (lockResult != 0)
                {
                  throw new Exception($"Failed to lock the color frame; error code {lockResult}.");
                }

                try
                {
                  var source = lockedArea.Data;
                  var destination = _colorImage.GetBuffer();
                  var destinationStride = (int)_colorImage.GetStride();

                  for (var i = 0; i < 480; i++)
                  {
                    _kernel32.CopyMemory(destination, source, 640 * 4);
                    source += lockedArea.Pitch;
                    destination += destinationStride;
                  }
                }
                finally
                {
                  var unlockResult = colorImageFrame.FrameTexture.UnlockRect(0);

                  if (unlockResult != 0)
                  {
                    throw new Exception($"Failed to unlock the color frame; error code {unlockResult}.");
                  }
                }
              }
              finally
              {
                var releaseResult = _sensor.NuiImageStreamReleaseFrame(_colorStream, ref colorImageFrame);

                if (releaseResult != 0)
                {
                  throw new Exception($"Failed to release a color frame; error code {releaseResult}.");
                }
              }
            }

            var depthImageFrameResult = _sensor.NuiImageStreamGetNextFrame(_depthStream, 0, out var depthImageFrame);

            if (depthImageFrameResult == 0)
            {
              try
              {
                var lockedArea = default(LockedArea);
                var rect = IntPtr.Zero;

                var lockResult = depthImageFrame.FrameTexture.LockRect(0, ref lockedArea, rect, 0);

                if (lockResult != 0)
                {
                  throw new Exception($"Failed to lock the depth frame; error code {lockResult}.");
                }

                try
                {
                  var source = lockedArea.Data;
                  var destination = _depthImage.GetBuffer();
                  var destinationStride = (int)_depthImage.GetStride();

                  for (var i = 0; i < 240; i++)
                  {
                    _kernel32.CopyMemory(destination, source, 320 * 2);
                    source += lockedArea.Pitch;
                    destination += destinationStride;
                  }
                }
                finally
                {
                  var unlockResult = depthImageFrame.FrameTexture.UnlockRect(0);

                  if (unlockResult != 0)
                  {
                    throw new Exception($"Failed to unlock the depth frame; error code {unlockResult}.");
                  }
                }
              }
              finally
              {
                var releaseResult = _sensor.NuiImageStreamReleaseFrame(_depthStream, ref depthImageFrame);

                if (releaseResult != 0)
                {
                  throw new Exception($"Failed to release a depth frame; error code {releaseResult}.");
                }
              }
            }

            identifiersOfPlayersToRemoveOrAdded.Clear();

            foreach (var playerIdentifier in _players.Keys)
            {
              identifiersOfPlayersToRemoveOrAdded.Add(playerIdentifier);
            }

            foreach (var skeleton in skeletonFrame.Skeletons)
            {
              switch (skeleton.Status)
              {
                case SkeletonStatus.NotTracked:
                  break;

                case SkeletonStatus.PositionOnly:
                  resultsByPlayerIdentifiers.Remove(skeleton.PlayerIdentifier);
                  goto case SkeletonStatus.Tracked;

                case SkeletonStatus.Tracked:
                  identifiersOfPlayersToRemoveOrAdded.Remove(skeleton.PlayerIdentifier);
                  break;

                default:
                  throw new Exception($"Unimplemented skeleton status {skeleton.Status}.");
              }
            }

            foreach (var playerIdentifier in identifiersOfPlayersToRemoveOrAdded)
            {
              resultsByPlayerIdentifiers.Remove(playerIdentifier);
            }

            playersToUpsertByIdentifiers.Clear();

            foreach (var skeleton in skeletonFrame.Skeletons)
            {
              switch (skeleton.Status)
              {
                case SkeletonStatus.NotTracked:
                  break;

                case SkeletonStatus.PositionOnly:
                  playersToUpsertByIdentifiers[skeleton.PlayerIdentifier] = new Player { ApproximatePosition = new Vector3(-skeleton.Position.X, skeleton.Position.Y, skeleton.Position.Z) };
                  break;

                case SkeletonStatus.Tracked:
                  var leftShoulder = GetJointPositionIfAvailable(skeleton, Joint.LeftShoulder);
                  var leftHip = GetJointPositionIfAvailable(skeleton, Joint.LeftHip);
                  var rightShoulder = GetJointPositionIfAvailable(skeleton, Joint.RightShoulder);
                  var rightHip = GetJointPositionIfAvailable(skeleton, Joint.RightHip);

                  if (!leftShoulder.HasValue || !leftHip.HasValue || !rightShoulder.HasValue || !rightHip.HasValue)
                  {
                    goto case SkeletonStatus.PositionOnly;
                  }
                  else
                  {
                    var neck = GetJointPositionIfAvailable(skeleton, Joint.Neck);
                    var topOfHead = GetJointPositionIfAvailable(skeleton, Joint.TopOfHead);

                    IResult result;

                    var headPointsKnown = neck.HasValue && topOfHead.HasValue & neck != topOfHead;

                    if (headPointsKnown)
                    {
                      var headPoints = new HeadPoints
                      {
                        NeckX = neck.Value.X,
                        NeckY = neck.Value.Y,
                        NeckZ = neck.Value.Z,
                        TopOfHeadX = topOfHead.Value.X,
                        TopOfHeadY = topOfHead.Value.Y,
                        TopOfHeadZ = topOfHead.Value.Z,
                      };

                      var sensorData = new SensorData
                      {
                        ColorImage = _colorImage,
                        DepthImage = _depthImage,
                        ZoomFactor = 1,
                        ViewOffset = default,
                      };

                      if (resultsByPlayerIdentifiers.TryGetValue(skeleton.PlayerIdentifier, out result))
                      {
                        var continueTrackingResult = _faceTracker.ContinueTracking(ref sensorData, ref headPoints, result);

                        if (continueTrackingResult != 0 || result.GetStatus() != 0)
                        {
                          result = null;
                          resultsByPlayerIdentifiers.Remove(skeleton.PlayerIdentifier);
                        }
                      }

                      if (result == null)
                      {
                        result = _results.First(x => !resultsByPlayerIdentifiers.Values.Contains(x));

                        var startTrackingResult = _faceTracker.StartTracking(ref sensorData, IntPtr.Zero, ref headPoints, result);

                        if (startTrackingResult != 0)
                        {
                          throw new Exception($"Failed to start face tracking; error code {startTrackingResult}.");
                        }

                        if (result.GetStatus() == 0)
                        {
                          resultsByPlayerIdentifiers[skeleton.PlayerIdentifier] = result;
                        }
                        else
                        {
                          result = null;
                        }
                      }
                    }
                    else
                    {
                      resultsByPlayerIdentifiers.Remove(skeleton.PlayerIdentifier);
                      result = null;
                    }

                    Vector3? headUpNormal = null;
                    Vector3? headForwardNormal = null;

                    FacialAnimation? facialAnimation = null;

                    if (result == null)
                    {
                      if (headPointsKnown)
                      {
                        headUpNormal = Vector3.Normalize(topOfHead.Value - neck.Value);
                      }
                    }
                    else
                    {
                      result.Get3DPose(out var scale, out var rotation, out var translation);

                      // TODO: Compute up normal from face tracking data.

                      var yaw = Math.PI * (180 - rotation.Y) / 180.0;
                      headForwardNormal = new Vector3((float)Math.Sin(yaw), 0, (float)Math.Cos(yaw));

                      result.GetAUCoefficients(out var auPointer, out var numberOfAnimationUnits);

                      if (auPointer != IntPtr.Zero)
                      {
                        var facialAnimationValue = new FacialAnimation();

                        Marshal.Copy(auPointer, animationUnitCoefficients, 0, Math.Min((int)numberOfAnimationUnits, (int)AnimationUnit.Count));

                        if (numberOfAnimationUnits > (int)AnimationUnit.LipRaiser)
                        {
                          facialAnimationValue.LipRaised = animationUnitCoefficients[(int)AnimationUnit.LipRaiser];
                        }

                        if (numberOfAnimationUnits > (int)AnimationUnit.JawLower)
                        {
                          facialAnimationValue.JawLowered = animationUnitCoefficients[(int)AnimationUnit.JawLower];
                        }

                        if (numberOfAnimationUnits > (int)AnimationUnit.LipStretcher)
                        {
                          facialAnimationValue.MouthWidth = animationUnitCoefficients[(int)AnimationUnit.LipStretcher];
                        }

                        // TODO: Determine emote.

                        facialAnimation = facialAnimationValue;
                      }
                    }

                    playersToUpsertByIdentifiers[skeleton.PlayerIdentifier] = new Player
                    {
                      ApproximatePosition = -new Vector3(-skeleton.Position.X, skeleton.Position.Y, skeleton.Position.Z),
                      Details = new PlayerDetails
                      {
                        HeadUpNormal = headUpNormal,
                        HeadForwardNormal = headForwardNormal,
                        FacialAnimation = facialAnimation,
                        LeftArm = ExtractLimb(skeleton, leftShoulder.Value, Joint.LeftElbow, Joint.LeftWrist, Joint.LeftMiddleFingertip),
                        RightArm = ExtractLimb(skeleton, rightShoulder.Value, Joint.RightElbow, Joint.RightWrist, Joint.RightMiddleFingertip),
                        LeftLeg = ExtractLimb(skeleton, leftHip.Value, Joint.LeftKnee, Joint.LeftAnkle, Joint.LeftMiddleToeTip),
                        RightLeg = ExtractLimb(skeleton, rightHip.Value, Joint.RightKnee, Joint.RightAnkle, Joint.RightMiddleToeTip),
                      },
                    };
                  }

                  break;

                default:
                  throw new Exception($"Unimplemented skeleton status \"{skeleton.Status}\".");
              }
            }

            lock (_lock)
            {
              foreach (var playerIdentifier in identifiersOfPlayersToRemoveOrAdded)
              {
                _players.Remove(playerIdentifier);
              }

              identifiersOfPlayersToRemoveOrAdded.Clear();

              foreach (var playerIdentifierAndPlayer in playersToUpsertByIdentifiers)
              {
                if (!_players.ContainsKey(playerIdentifierAndPlayer.Key))
                {
                  identifiersOfPlayersToRemoveOrAdded.Add(playerIdentifierAndPlayer.Key);
                }

                _players[playerIdentifierAndPlayer.Key] = playerIdentifierAndPlayer.Value;
              }
            }

            foreach (var playerIdentifier in identifiersOfPlayersToRemoveOrAdded)
            {
              var newPlayer = NewPlayer;

              if (newPlayer != null)
              {
                newPlayer(playerIdentifier);
              }
            }
          }
          else
          {
            Thread.Sleep(20);
          }

          var changed = false;

          lock (_lock)
          {
            for (var i = 0; i < identifiersOfPlayersTrackedInDetail.Length; i++)
            {
              if (identifiersOfPlayersTrackedInDetail[i] != 0 && !_identifiersOfPlayersTrackedInDetail.Contains(identifiersOfPlayersTrackedInDetail[i]))
              {
                identifiersOfPlayersTrackedInDetail[i] = 0;
                changed = true;
              }

              if (identifiersOfPlayersTrackedInDetail[i] == 0)
              {
                var next = _identifiersOfPlayersTrackedInDetail.Except(identifiersOfPlayersTrackedInDetail).Cast<uint?>().FirstOrDefault();

                if (next.HasValue)
                {
                  identifiersOfPlayersTrackedInDetail[i] = next.Value;
                  changed = true;
                }
              }
            }
          }

          if (changed)
          {
            var setTrackedSkeletonsResult = _sensor.NuiSkeletonSetTrackedSkeletons(identifiersOfPlayersTrackedInDetail);

            if (setTrackedSkeletonsResult != 0)
            {
              throw new Exception($"Failed to set tracked skeletons; error code {setTrackedSkeletonsResult}.");
            }
          }
        }
      }
      catch (Exception ex)
      {
        _workerError = ex.Message;
      }
    }

    private static Vector3? GetJointPositionIfAvailable(Internals.Interop.Kinect10.Skeleton skeleton, Joint joint)
    {
      var jointIndex = (int)joint;

      var status = skeleton.JointStatuses[jointIndex];

      switch (status)
      {
        case JointStatus.NotTracked:
          return null;

        case JointStatus.Inferred:
        case JointStatus.Tracked:
          var position = skeleton.JointPositions[jointIndex];
          return new Vector3(-position.X, position.Y, position.Z);

        default:
          throw new Exception($"Unimplemented joint status \"{status}\".");
      }
    }

    private static Limb ExtractLimb(Internals.Interop.Kinect10.Skeleton skeleton, Vector3 proximalPosition, Joint intermediateJoint, Joint distalJoint, Joint tipJoint)
    {
      var intermediatePosition = GetJointPositionIfAvailable(skeleton, intermediateJoint);
      var distalPosition = GetJointPositionIfAvailable(skeleton, distalJoint);
      var tipPosition = GetJointPositionIfAvailable(skeleton, tipJoint);

      return new Limb
      {
        ProximalPosition = proximalPosition,
        Extension = distalPosition.HasValue && tipPosition.HasValue ? (LimbExtension?)new LimbExtension
        {
          IntermediatePosition = intermediatePosition,
          DistalPosition = distalPosition.Value,
          TipPosition = tipPosition.Value,
        } : null,
      };
    }

    /// <inheritdoc />
    /// <param name="faceTrackLib">The native methods in FaceTrackLib.dll.  An instance of <see cref="FaceTrackLib"/> is likely desired.</param>
    /// <param name="kernel32">The native methods in kernel32.dll.  An instance of <see cref="Kernel32"/> is likely desired.</param>
    /// <param name="kinect10">The native methods in Kinect10.dll.  An instance of <see cref="Kinect10"/> is likely desired.</param>
    /// <param name="uniqueIdentifier">The unique identifier of the Kinect sensor to connect to.</param>
    /// <param name="pitchDegrees">The pitch to set using the Kinect's motorized stand; a number of degrees relative to the horizon, where positive values are above the horizon and negative values are below.  The limit is approximately +/- 27.</param>
    /// <exception cref="Exception">Thrown when the Kinect cannot be connected to.</exception>
    public Kinect(IFaceTrackLib faceTrackLib, IKernel32 kernel32, IKinect10 kinect10, string uniqueIdentifier, int pitchDegrees)
    {
      try
      {
        _faceTrackLib = faceTrackLib;
        _kernel32 = kernel32;

        var createSensorByIdResult = kinect10.NuiCreateSensorById(uniqueIdentifier, out _sensor);

        if (createSensorByIdResult != 0)
        {
          throw new Exception($"Failed to create an instance of Kinect \"{uniqueIdentifier}\"; error code {createSensorByIdResult}.");
        }

        if (_sensor == null)
        {
          throw new Exception($"Failed to create an instance of Kinect \"{uniqueIdentifier}\"; no instance was output.");
        }

        var initializeResult = _sensor.NuiInitialize(InitializeFlags.UseSkeletalTracking | InitializeFlags.UseColor | InitializeFlags.UseDepthAndPlayerIndex);

        if (initializeResult != 0)
        {
          throw new Exception($"Failed to initialize the Kinect; error code {initializeResult}.");
        }

        _sensorInitialized = true;

        var setAngleResult = _sensor.NuiCameraElevationSetAngle(pitchDegrees);

        if (setAngleResult != 0)
        {
          throw new Exception($"Failed to set the Kinect's angle; error code {setAngleResult}.");
        }

        var getCoordinateMapperResult = _sensor.NuiGetCoordinateMapper(out _coordinateMapper);

        if (getCoordinateMapperResult != 0)
        {
          throw new Exception($"Failed to get a coordinate mapper; error code {getCoordinateMapperResult}.");
        }

        if (_coordinateMapper == null)
        {
          throw new Exception($"Failed to get a coordinate mapper; no instance was output.");
        }

        var enableSkeletonTrackingResult = _sensor.NuiSkeletonTrackingEnable(IntPtr.Zero, SkeletonTrackingFlags.AppChoosesSkeletons);

        if (enableSkeletonTrackingResult != 0)
        {
          throw new Exception($"Failed to enable the Kinect's skeleton tracking; error code {enableSkeletonTrackingResult}.");
        }

        var colorStreamOpenResult = _sensor.NuiImageStreamOpen(ImageType.Color, ImageResolution.SixHundredAndFortyByFourHundredAndEighty, 0, 2, IntPtr.Zero, out _colorStream);

        if (colorStreamOpenResult != 0)
        {
          throw new Exception($"Failed to enable the Kinect's color stream; error code {colorStreamOpenResult}.");
        }

        if (_colorStream == IntPtr.Zero)
        {
          throw new Exception($"Failed to enable the Kinect's color stream; no instance was output.");
        }

        var depthStreamOpenResult = _sensor.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, ImageResolution.ThreeHundredAndTwentyByTwoHundredAndForty, 0, 2, IntPtr.Zero, out _depthStream);

        if (depthStreamOpenResult != 0)
        {
          throw new Exception($"Failed to enable the Kinect's depth stream; error code {depthStreamOpenResult}.");
        }

        if (_depthStream == IntPtr.Zero)
        {
          throw new Exception($"Failed to enable the Kinect's depth stream; no instance was output.");
        }

        _depthToColorDelegate = DepthToColorCallback;

        var _depthToColorFunctionPointer = Marshal.GetFunctionPointerForDelegate(_depthToColorDelegate);

        if (_depthToColorFunctionPointer == IntPtr.Zero)
        {
          throw new Exception("Failed to create a function pointer to map depth to color coordinates.");
        }

        _worker = new Thread(Worker);

        _worker.Start();
      }
      catch
      {
        Dispose();

        throw;
      }
    }

    /// <inheritdoc />
    public int ExpectedUpdatesPerSecond => 30;

    /// <inheritdoc />
    public event NewPlayerHandler<uint> NewPlayer;

    /// <inheritdoc />
    public Player? Get(uint playerIdentifier)
    {
      lock (_lock)
      {
        if (_sensor == null)
        {
          throw new ObjectDisposedException(null);
        }

        if (_workerError != null)
        {
          throw new Exception(_workerError);
        }

        if (!_players.TryGetValue(playerIdentifier, out var player))
        {
          return null;
        }

        return player;
      }
    }

    /// <inheritdoc />
    public void StopTrackingInDetail(uint playerIdentifier)
    {
      lock (_lock)
      {
        if (_sensor == null)
        {
          throw new ObjectDisposedException(null);
        }

        if (_workerError != null)
        {
          throw new Exception(_workerError);
        }

        _identifiersOfPlayersTrackedInDetail.Remove(playerIdentifier);
      }
    }

    /// <inheritdoc />
    public void TrackInDetail(uint playerIdentifier)
    {
      lock (_lock)
      {
        if (_sensor == null)
        {
          throw new ObjectDisposedException(null);
        }

        if (_workerError != null)
        {
          throw new Exception(_workerError);
        }

        if (_players.ContainsKey(playerIdentifier))
        {
          _identifiersOfPlayersTrackedInDetail.Add(playerIdentifier);
        }
      }
    }

    /// <inheritdoc />
    ~Kinect()
    {
      Dispose();
    }

    /// <inheritdoc />
    public void Dispose()
    {
      if (_worker != null)
      {
        _stopWorker = true;

        while (_worker.IsAlive)
        {
          Thread.Sleep(20);
        }

        _worker = null;
      }

      if (_results != null)
      {
        for (var i = 0; i < _results.Length; i++)
        {
          var result = _results[i];

          if (result != null)
          {
            _results[i] = null;
            ReleaseIfComObject(result);
          }
        }

        _results = null;
      }

      if (_colorImage != null)
      {
        var colorImage = _colorImage;
        _colorImage = null;
        ReleaseIfComObject(colorImage);
      }

      if (_depthImage != null)
      {
        var depthImage = _depthImage;
        _depthImage = null;
        ReleaseIfComObject(depthImage);
      }

      if (_faceTracker != null)
      {
        var faceTracker = _faceTracker;
        _faceTracker = null;
        ReleaseIfComObject(faceTracker);
      }

      _depthToColorFunctionPointer = IntPtr.Zero;
      _depthToColorDelegate = null;

      if (_coordinateMapper != null)
      {
        var coordinateMapper = _coordinateMapper;
        _coordinateMapper = null;
        ReleaseIfComObject(coordinateMapper);
      }

      if (_sensor != null)
      {
        if (_sensorInitialized)
        {
          _sensorInitialized = false;

          _sensor.NuiShutdown();
        }

        var sensor = _sensor;
        _sensor = null;
        ReleaseIfComObject(sensor);
      }

      _identifiersOfPlayersTrackedInDetail = null;
      _players = null;

      _kernel32 = null;
      _faceTrackLib = null;

      GC.SuppressFinalize(this);
    }

    private static void ReleaseIfComObject(object obj)
    {
      if (Marshal.IsComObject(obj))
      {
        // This is impossible to hit during a unit test.
        Marshal.FinalReleaseComObject(obj);
      }
    }
  }
}
