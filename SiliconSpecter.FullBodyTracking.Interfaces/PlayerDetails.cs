using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Interfaces
{
  /// <summary>
  /// Detailed information regarding a <see cref="Player"/> being tracked.
  /// </summary>
  public struct PlayerDetails
  {
    /// <summary>
    /// When known, the normal from the bottom to the top of the <see cref="Player"/>'s head, otherwise, <see langword="null"/>.
    /// </summary>
    public Vector3? HeadUpNormal;

    /// <summary>
    /// When known, the normal from the back to the front of the <see cref="Player"/>'s head, otherwise, <see langword="null"/>.
    /// </summary>
    public Vector3? HeadForwardNormal;

    /// <summary>
    /// When known, the emote the <see cref="Player"/> is currently making, otherwise, <see langword="null"/>.
    /// </summary>
    public Emote? Emote;

    /// <summary>
    /// Positive values up to 1 indicate that the upper lip has drawn back to expose the upper teeth.
    /// Zero indicates that the upper lip is covering the upper teeth.
    /// Negative values down to -1 indicate that the upper lip is stretched further than the upper teeth.
    /// <see langword="null"/> when the data is unavailable.
    /// </summary>
    public float? LipRaised;

    /// <summary>
    /// Positive values up to 1 indicate that the jaw has been lowered.
    /// Zero indicates that the jaw is closed.
    /// <see langword="null"/> when the data is unavailable.
    /// </summary>
    public float? JawLowered;

    /// <summary>
    /// Positive values up to 1 indicate that the mouth is stretched wide.
    /// Negative values down to -1 indicate that the mouth is puckering.
    /// <see langword="null"/> when the data is unavailable.
    /// </summary>
    public float? MouthWidth;

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
