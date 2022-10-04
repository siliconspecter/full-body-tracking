using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Interfaces
{
  /// <summary>
  /// A keyframe of a player's data which can be interpolated.
  /// </summary>
  public struct InterpolatablePlayerKeyframe
  {
    /// <summary>
    /// The current facial animation.
    /// </summary>
    public FacialAnimation FacialAnimation;

    /// <summary>
    /// The position of the hips, in meters, in world space.
    /// </summary>
    public Vector3 HipsPosition;

    /// <summary>
    /// The rotation of the hips, relative to the facing rotation.
    /// </summary>
    public Quaternion HipsRotation;

    /// <summary>
    /// The rotation of the shoulders, relative to the facing rotation.
    /// </summary>
    public Quaternion ShouldersRotation;

    /// <summary>
    /// The rotation of the head, relative to the facing rotation.
    /// </summary>
    public Quaternion HeadRotation;

    /// <summary>
    /// An approximate facing rotation, in world space.
    /// </summary>
    public Quaternion FacingRotation;

    /// <summary>
    /// Details regarding the left arm.
    /// </summary>
    public InterpolatablePlayerKeyframeLimb LeftArm;

    /// <summary>
    /// Details regarding the right arm.
    /// </summary>
    public InterpolatablePlayerKeyframeLimb RightArm;

    /// <summary>
    /// Details regarding the left leg.
    /// </summary>
    public InterpolatablePlayerKeyframeLimb LeftLeg;

    /// <summary>
    /// Details regarding the right arm.
    /// </summary>
    public InterpolatablePlayerKeyframeLimb RightLeg;
  }
}
