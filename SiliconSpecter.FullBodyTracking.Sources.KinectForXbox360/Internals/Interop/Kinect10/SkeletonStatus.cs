namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10;


/// <summary>
/// The state of a <see cref="Skeleton"/>.
/// </summary>
public enum SkeletonStatus
{
    /// <summary>
    /// The Kinect does not see a <see cref="Skeleton"/>.
    /// </summary>
    NotTracked = 0,

    /// <summary>
    /// The Kinect is only tracking the <see cref="Skeleton.Position"/> of the <see cref="Skeleton"/>.
    /// </summary>
    PositionOnly = 1,

    /// <summary>
    /// The Kinect is tracking the positions of the individual joints within the <see cref="Skeleton"/>.
    /// </summary>
    Tracked = 2,
}
