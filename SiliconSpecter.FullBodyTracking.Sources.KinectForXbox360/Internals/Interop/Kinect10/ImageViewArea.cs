using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10;

/// <summary>
/// An area of the image.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct ImageViewArea
{
    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    internal int DigitalZoom;

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    internal int CenterX;

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    internal int CenterY;
}
