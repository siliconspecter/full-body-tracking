namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10
{
  /// <summary>
  /// The state of a joint within a <see cref="Skeleton"/>.
  /// </summary>
  public enum JointStatus
  {
    /// <summary>
    /// The Kinect does not have suggestions regarding the joint's position.
    /// </summary>
    NotTracked = 0,

    /// <summary>
    /// The Kinect is inferring the position of the joint from other information.
    /// </summary>
    Inferred = 1,

    /// <summary>
    /// The Kinect has a high-confidence lock on the position of the joint.
    /// </summary>
    Tracked = 2,
  }
}
