using System;
using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.FaceTrackLib
{
  /// <summary>
  /// An instance of the face tracking library.
  /// </summary>
  [ComImport, Guid("1A00A7BA-C217-11E0-AC90-0024811441FD"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IFaceTracker
  {
    /// <summary>
    /// Initializes <see langword="this"/> <see cref="IFaceTracker"/>.
    /// </summary>
    /// <param name="colorCameraConfiguration">The <see cref="CameraConfiguration"/> for the color camera.</param>
    /// <param name="depthCameraConfiguration">The <see cref="CameraConfiguration"/> for the depth camera.</param>
    /// <param name="depthToColorMappingFunction">The COM-marshalled implementation of <see cref="DepthToColorDelegate"/>.</param>
    /// <param name="modelPath">Unused; always <see langword="null"/>.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [PreserveSig]
    int Initialize(ref CameraConfiguration colorCameraConfiguration, ref CameraConfiguration depthCameraConfiguration, IntPtr depthToColorMappingFunction, string modelPath);

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    void Reset();

    /// <summary>
    /// Creates a new <see cref="IResult"/>.
    /// </summary>
    /// <param name="result">The created <see cref="IResult"/>, if any, otherwise, <see langword="null"/>.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [PreserveSig]
    int CreateFTResult(out IResult result);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="scale">Unused.</param>
    /// <param name="shapeUnitCoefficients">Unused.</param>
    /// <param name="shapeUnitCoefficientCount">Unused.</param>
    [Obsolete]
    void SetShapeUnits(float scale, float[] shapeUnitCoefficients, uint shapeUnitCoefficientCount);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="scale">Unused.</param>
    /// <param name="shapeUnitCoefficients">Unused.</param>
    /// <param name="shapeUnitCoefficientCount">Unused.</param>
    /// <param name="haveConverged">Unused.</param>
    [Obsolete]
    void GetShapeUnits(out float scale, out IntPtr shapeUnitCoefficients, ref uint shapeUnitCoefficientCount, out bool haveConverged);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="isEnabled">Unused.</param>
    [Obsolete]
    void SetShapeComputationState(bool isEnabled);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="isEnabled">Unused.</param>
    [Obsolete]
    void GetComputationState(out bool isEnabled);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="model">Unused.</param>
    [Obsolete]
    void GetFaceModel(out IntPtr model);

    /// <summary>
    /// Attempts to start tracking the face of a skeleton previously found by the Kinect.
    /// </summary>
    /// <param name="sensorData">The <see cref="SensorData"/> to use.</param>
    /// <param name="regionOfInterest">Unused; always <see cref="IntPtr.Zero"/>.</param>
    /// <param name="headPoints">The <see cref="HeadPoints"/> from the skeleton data.</param>
    /// <param name="result">The <see cref="IResult"/> to use.</param>
    /// <returns>Zero on success, non-zero on failure.  Note that success does not necessarily mean that a face was tracked.</returns>
    [PreserveSig]
    int StartTracking(ref SensorData sensorData, IntPtr regionOfInterest, ref HeadPoints headPoints, IResult result);

    /// <summary>
    /// Attempts to continue tracking the face of a skeleton previously found by the Kinect and then tracked using <see cref="StartTracking"/>.
    /// </summary>
    /// <param name="sensorData">The <see cref="SensorData"/> to use.</param>
    /// <param name="headPoints">The <see cref="HeadPoints"/> from the skeleton data.</param>
    /// <param name="result">The <see cref="IResult"/> to use.</param>
    /// <returns>Zero on success, non-zero on failure.  Failure sometimes happens when the face tracking is lost and should not be treated as critical.</returns>
    [PreserveSig]
    int ContinueTracking(ref SensorData sensorData, ref HeadPoints headPoints, IResult result);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="sensorData">Unused.</param>
    /// <param name="regionOfInterest">Unused.</param>
    /// <param name="faces">Unused.</param>
    /// <param name="facesCount">Unused.</param>
    /// <returns>Unused.</returns>
    [Obsolete]
    [PreserveSig]
    int DetectFaces(ref SensorData sensorData, ref IntPtr regionOfInterest, IntPtr faces, ref uint facesCount);
  }
}
