using System;

namespace SiliconSpecter.FullBodyTracking.Interfaces
{
  /// <summary>
  /// Converts <see cref="Player"/>s to <see cref="Metrics"/>.
  /// </summary>
  public interface IPlayerToMetricsConverter
  {
    /// <summary>
    /// Converts a given <see cref="Player"/> to an equivalent <see cref="Metrics"/>.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to convert to <see cref="Metrics"/>.</param>
    /// <param name="metrics">The previous <see cref="Metrics"/>, if any, otherwise, <see langword="null"/>.</param>
    /// <returns>The <see cref="Metrics"/> converted from the given <paramref name="player"/>.</returns>
    /// <exception cref="Exception">Thrown when <see cref="Player.Details"/> is <see langword="null"/>.</exception>
    InterpolatablePlayerKeyframe Convert(Player player, Metrics? metrics);
  }
}
