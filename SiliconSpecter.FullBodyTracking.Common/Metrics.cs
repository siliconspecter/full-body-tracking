namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Information regarding the actor controlling an avatar.
  /// </summary>
  public struct Metrics
  {
    /// <summary>
    /// The length of the actor's left arm when fully extended.
    /// </summary>
    public float LeftArmLength;

    /// <summary>
    /// The length of the actor's right arm when fully extended.
    /// </summary>
    public float RightArmLength;

    /// <summary>
    /// The length of the actor's left leg when fully extended.
    /// </summary>
    public float LeftLegLength;

    /// <summary>
    /// The length of the actor's right leg when fully extended.
    /// </summary>
    public float RightLegLength;

    /// <summary>
    /// The default value for <see cref="Metrics"/>.
    /// </summary>
    public static readonly Metrics Default = new Metrics
    {
      LeftArmLength = 0.5f,
      RightArmLength = 0.5f,
      LeftLegLength = 0.85f,
      RightLegLength = 0.85f,
    };
  }
}
