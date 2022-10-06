namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// The emote currently being displayed by a <see cref="Player{TFrameNumber}"/>.
  /// </summary>
  public enum Emote
  {
    /// <summary>
    /// The <see cref="Player{TFrameNumber}"/> is not currently making any particular emote.
    /// </summary>
    Neutral,

    /// <summary>
    /// The <see cref="Player{TFrameNumber}"/> is currently making an angry emote.
    /// </summary>
    Angry,

    /// <summary>
    /// The <see cref="Player{TFrameNumber}"/> is currently making a surprised emote.
    /// </summary>
    Surprised,

    /// <summary>
    /// The <see cref="Player{TFrameNumber}"/> is currently making a joy (smiling) emote.
    /// </summary>
    //
    Joy,
  }
}
