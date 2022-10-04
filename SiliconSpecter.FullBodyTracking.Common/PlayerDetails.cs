using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Detailed information regarding a <see cref="Player"/> being tracked.
  /// </summary>
  public struct PlayerDetails
  {
    /// <summary>
    /// When known, the normal from the bottom to the top of the <see cref="Player"/>'s head, in camera space, if known, otherwise, <see langword="null"/>.
    /// </summary>
    public Vector3? HeadUpNormal;

    /// <summary>
    /// When known, the normal from the back to the front of the <see cref="Player"/>'s head, in camera space, if known, otherwise, <see langword="null"/>.
    /// </summary>
    public Vector3? HeadForwardNormal;

    /// <summary>
    /// The current facial animation, if known, otherwise, <see langword="null"/>.
    /// </summary>
    public FacialAnimation? FacialAnimation;

    /// <summary>
    /// Details regarding the <see cref="Player"/>'s left arm.
    /// </summary>
    public Limb LeftArm;

    /// <summary>
    /// Details regarding the <see cref="Player"/>'s right arm.
    /// </summary>
    public Limb RightArm;

    /// <summary>
    /// Details regarding the <see cref="Player"/>'s left leg.
    /// </summary>
    public Limb LeftLeg;

    /// <summary>
    /// Details regarding the <see cref="Player"/>'s right leg.
    /// </summary>
    public Limb RightLeg;
  }
}
