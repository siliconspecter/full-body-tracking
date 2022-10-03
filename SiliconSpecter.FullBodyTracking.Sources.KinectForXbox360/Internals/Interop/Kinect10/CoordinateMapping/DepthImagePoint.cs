using System;
using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10.CoordinateMapping
{
  /// <summary>
  /// A point on a depth image.
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct DepthImagePoint
  {
    /// <summary>
    /// The coordinate on the X axis.
    /// </summary>
    public int X;

    /// <summary>
    /// The coordinate on the Y axis.
    /// </summary>
    public int Y;

    /// <summary>
    /// The coordinate on the Z axis.
    /// </summary>
    public int Depth;

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    public int Reserved;
  }
}
