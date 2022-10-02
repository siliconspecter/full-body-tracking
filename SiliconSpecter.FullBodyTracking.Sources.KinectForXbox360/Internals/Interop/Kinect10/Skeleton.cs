using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10;

/// <summary>
/// Represents a <see cref="Skeleton"/> from the Kinect.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Skeleton
{
    /// <summary>
    /// The state of <see langword="this"/> <see cref="Skeleton"/>.
    /// </summary>
    public SkeletonStatus Status;

    /// <summary>
    /// The player identifier <see langword="this"/> <see cref="Skeleton"/> represents.
    /// </summary>
    public uint PlayerIdentifier;

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    public uint Unused;

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    public uint UserIndex;

    /// <summary>
    /// The approximate position of the <see cref="Skeleton"/>, for use when <see cref="Status"/> is <see cref="SkeletonStatus.PositionOnly"/>.
    /// </summary>
    public KinectVector4 Position;

    /// <summary>
    /// The approximate positions of the joints within the <see cref="Skeleton"/>, for use when <see cref="Status"/> is <see cref="SkeletonStatus.Tracked"/>.
    /// </summary>
    /// <remarks>See <see cref="Joint"/> for indices.</remarks>
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = (int)Joint.Count)]
    public KinectVector4[] JointPositions;

    /// <summary>
    /// The statuses of the joints within the <see cref="Skeleton"/>, for use when <see cref="Status"/> is <see cref="SkeletonStatus.Tracked"/>.
    /// </summary>
    /// <remarks>See <see cref="Joint"/> for indices.</remarks>
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = (int)Joint.Count)]
    public JointStatus[] JointStatuses;

    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    public uint Quality;
}
