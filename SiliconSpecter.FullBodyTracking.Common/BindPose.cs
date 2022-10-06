namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Details regarding an unposed character model.
  /// </summary>
  public struct BindPose
  {
    /// <summary>
    /// The character's left arm.
    /// </summary>
    public BindPoseLimb LeftArm;

    /// <summary>
    /// The character's right arm.
    /// </summary>
    public BindPoseLimb RightArm;

    /// <summary>
    /// The character's left leg.
    /// </summary>
    public BindPoseLimb LeftLeg;

    /// <summary>
    /// The character's right leg.
    /// </summary>
    public BindPoseLimb RightLeg;
  }
}
