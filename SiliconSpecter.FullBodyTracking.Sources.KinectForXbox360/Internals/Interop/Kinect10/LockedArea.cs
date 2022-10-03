using System;
using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10
{
  /// <summary>
  /// A locked area of a <see cref="IFrameTexture"/>.
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct LockedArea
  {
    /// <summary>
    /// The number of <see cref="byte"/>s per pixel row.
    /// </summary>
    public int Pitch;

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    public int Size;

    /// <summary>
    /// A pointer to the raw data within the locked area..
    /// </summary>
    public IntPtr Data;
  }
}
