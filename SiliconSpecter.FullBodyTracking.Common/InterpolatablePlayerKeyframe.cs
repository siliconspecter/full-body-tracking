using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
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
    /// The position of the character (hip center on X and Z, lower foot on Y), in meters, in world space.
    /// </summary>
    public Vector3 Position;

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

    /// <summary>
    /// The default value for a <see cref="InterpolatablePlayerKeyframe"/>.
    /// </summary>
    public static readonly InterpolatablePlayerKeyframe Default = new InterpolatablePlayerKeyframe
    {
      FacialAnimation = new FacialAnimation
      {
        Emote = Emote.Neutral,
        LipRaised = 0,
        JawLowered = 0,
        MouthWidth = 0,
      },
      Position = Vector3.Zero,
      HipsRotation = Quaternion.Identity,
      ShouldersRotation = Quaternion.Identity,
      HeadRotation = Quaternion.Identity,
      FacingRotation = Quaternion.Identity,
      LeftArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0, -1, 0),
        BendNormal = new Vector3(0, 0, -1),
        TipNormal = new Vector3(0, -1, 0),
        TipLength = 0.15f,
      },
      RightArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0, -1, 0),
        BendNormal = new Vector3(0, 0, -1),
        TipNormal = new Vector3(0, -1, 0),
        TipLength = 0.15f,
      },
      LeftLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0, -1, 0),
        BendNormal = new Vector3(0, 0, 1),
        TipNormal = new Vector3(0, 0, 1),
        TipLength = 0.2f,
      },
      RightLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0, -1, 0),
        BendNormal = new Vector3(0, 0, 1),
        TipNormal = new Vector3(0, 0, 1),
        TipLength = 0.2f,
      }
    };
  }
}
