namespace SiliconSpecter.FullBodyTracking.Interfaces
{
  /// <summary>
  /// A player which has been converted into a format which can be interpolated.
  /// </summary>
  public struct InterpolatablePlayer
  {
    /// <summary>
    /// The keyframe being interpolated from.
    /// </summary>
    public InterpolatablePlayerKeyframe Previous;

    /// <summary>
    /// The keyframe being interpolated to.
    /// </summary>
    public InterpolatablePlayerKeyframe Next;
  }
}
