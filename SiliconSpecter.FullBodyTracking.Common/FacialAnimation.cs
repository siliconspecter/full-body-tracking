using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Facial animation data.
  /// </summary>
  public struct FacialAnimation
  {
    /// <summary>
    /// When known, the emote the <see cref="Player"/> is currently making.
    /// </summary>
    public Emote Emote;

    /// <summary>
    /// Positive values up to 1 indicate that the upper lip has drawn back to expose the upper teeth.
    /// Zero indicates that the upper lip is covering the upper teeth.
    /// Negative values down to -1 indicate that the upper lip is stretched further than the upper teeth.
    /// </summary>
    public float LipRaised;

    /// <summary>
    /// Positive values up to 1 indicate that the jaw has been lowered.
    /// Zero indicates that the jaw is closed.
    /// </summary>
    public float JawLowered;

    /// <summary>
    /// Positive values up to 1 indicate that the mouth is stretched wide.
    /// Negative values down to -1 indicate that the mouth is puckering.
    /// </summary>
    public float MouthWidth;
  }
}
