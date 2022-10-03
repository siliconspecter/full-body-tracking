using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop
{
  /// <summary>
  /// A point in 2D space.
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct Point
  {
    /// <summary>
    /// The position on the X axis.
    /// </summary>
    public int X;

    /// <summary>
    /// The position on the Y axis.
    /// </summary>
    public int Y;
  }
}
