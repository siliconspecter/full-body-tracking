using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.FaceTrackLib;

/// <summary>
/// Information from the Kinect for the face tracking library.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SensorData
{
  /// <summary>
  /// The color image.
  /// </summary>
  public IImage ColorImage;

  /// <summary>
  /// The depth image.
  /// </summary>
  public IImage DepthImage;

  /// <summary>
  /// The zoom factor; always one.
  /// </summary>
  public float ZoomFactor;

  /// <summary>
  /// The view offset; always zero.
  /// </summary>
  public Point ViewOffset;
}
