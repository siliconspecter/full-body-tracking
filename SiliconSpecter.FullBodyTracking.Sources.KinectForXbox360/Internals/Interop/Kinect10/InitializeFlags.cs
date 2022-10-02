namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10;

/// <summary>
/// Settings which can be applied while initializing an <see cref="ISensor"/>.
/// </summary>
[Flags]
public enum InitializeFlags : uint
{
    /// <summary>
    /// When set, 13-bit depth information and 3-bit player index information can be read.
    /// </summary>
    UseDepthAndPlayerIndex = 1,

    /// <summary>
    /// When set, color information can be read.
    /// </summary>
    UseColor = 2,

    /// <summary>
    /// When set, skeleton data can be read.
    /// </summary>
    UseSkeletalTracking = 8,

    /// <summary>
    /// When set, 16-bit depth information can be read.
    /// </summary>
    [Obsolete]
    UseDepth = 32,

    /// <summary>
    /// When set, microphone input/etc. can be read.
    /// </summary>
    [Obsolete]
    UseAudio = 268435456,
}
