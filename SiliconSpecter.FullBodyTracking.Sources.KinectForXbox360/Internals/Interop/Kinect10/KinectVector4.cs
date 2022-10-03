using System;
using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10
{
  /// <summary>
  /// A 4-dimensional vector from the Kinect.
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct KinectVector4
  {
    /// <summary>
    /// Typically, zero is in the center, positive values are to the left, and negative values are to the right, all in meters.
    /// </summary>
    public float X;

    /// <summary>
    /// Typically, zero is in the center, positive values are to the top, and negative values are to the bottom, all in meters.
    /// </summary>
    public float Y;

    /// <summary>
    /// Typically, zero is in the sensor, positive values are distant, and negative values are near, all in meters.
    /// </summary>
    public float Z;

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    public float W;
  }
}
