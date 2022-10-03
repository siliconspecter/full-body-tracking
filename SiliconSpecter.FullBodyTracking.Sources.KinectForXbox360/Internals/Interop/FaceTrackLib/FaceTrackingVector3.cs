using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.FaceTrackLib
{
  /// <summary>
  /// A 3-dimensional vector from the face tracking library.
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct FaceTrackingVector3
  {
    /// <summary>
    /// Typically:
    /// - For positions, zero is in the center, positive values are to the left, and negative values are to the right, all in meters.
    /// - For rotations, this is the pitch; zero is looking towards the camera, -90 is looking at the nadir, and +90 is looking at the zenith.
    /// </summary>
    public float X;

    /// <summary>
    /// Typically:
    /// - For positions, zero is in the center, positive values are to the top, and negative values are to the bottom, all in meters.
    /// - For rotations, this is the roll; zero is level, -90 is right ear to right shoulder, and +90 is left ear to left shoulder.
    /// </summary>
    public float Y;

    /// <summary>
    /// Typically:
    /// - For positions, zero is in the center, positive values are distant, and negative values are near, all in meters.
    /// - For rotations, this is the yaw; zero is looking towards the camera, -90 is looking over the right shoulder, and +90 is looking over the left shoulder.
    /// </summary>
    public float Z;
  }
}
