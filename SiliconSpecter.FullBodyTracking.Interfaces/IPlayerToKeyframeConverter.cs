using System;

namespace SiliconSpecter.FullBodyTracking.Interfaces
{
  /// <summary>
  /// Converts <see cref="Player"/>s to <see cref="InterpolatablePlayerKeyframe"/>s.
  /// </summary>
  public interface IPlayerToKeyframeConverter
  {
    /// <summary>
    /// Converts a given <see cref="Player"/> to an equivalent <see cref="InterpolatablePlayerKeyframe"/>.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to convert to a <see cref="InterpolatablePlayerKeyframe"/>.</param>
    /// <param name="metrics">The <see cref="Metrics"/> of the actor.</param>
    /// <returns>The <see cref="InterpolatablePlayerKeyframe"/> converted from the given <paramref name="player"/>.</returns>
    /// <exception cref="Exception">Thrown when <see cref="Player.Details"/> is <see langword="null"/>.</exception>
    InterpolatablePlayerKeyframe Convert(Player player, Metrics metrics);
  }
}
