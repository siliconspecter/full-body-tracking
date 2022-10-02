using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10;

/// <summary>
/// Represents a frame read from a <see cref="Skeleton"/> stream.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct SkeletonFrame
{
    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    public long Timestamp;

    /// <summary>
    /// Uniquely identifies the frame (used to distinguish between "a new frame with the same data" and "no new frame yet").
    /// </summary>
    public uint FrameNumber;

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    public uint Flags;

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    public KinectVector4 FloorClipPlane;

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    public KinectVector4 NormalToGravity;

    /// <summary>
    /// The number of <see cref="Skeleton"/>s the Kinect is able to track.
    /// </summary>
    public const int NumberOfSkeletons = 6;

    /// <summary>
    /// The number of <see cref="Skeleton"/>s the Kinect is able to track in detail.
    /// </summary>
    public const int NumberOfSkeletonsTrackedInDetail = 2;

    /// <summary>
    /// The <see cref="Skeleton"/>s the Kinect may currently be tracking.
    /// </summary>
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NumberOfSkeletons)]
    public Skeleton[] Skeletons;
}
