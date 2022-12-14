using System;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Converts <see cref="Player{TFrameNumber}"/>s to <see cref="Metrics"/>.
  /// </summary>
  /// <typeparam name="TFrameNumber">A value which changes each time a <see cref="Player{TFrameNumber}"/> is refreshed.</typeparam>
  public interface IPlayerToMetricsConverter<TFrameNumber>
  {
    /// <summary>
    /// Converts a given <see cref="Player{TFrameNumber}"/> to an equivalent <see cref="Metrics"/>.
    /// </summary>
    /// <param name="player">The <see cref="Player{TFrameNumber}"/> to convert to <see cref="Metrics"/>.</param>
    /// <param name="metrics">The previous <see cref="Metrics"/>.</param>
    /// <param name="mix">The amount of smoothing to apply, where zero never changes from <paramref name="metrics"/> and one is no smoothing at all.</param>
    /// <returns>The <see cref="Metrics"/> converted from the given <paramref name="player"/>.</returns>
    Metrics Convert(Player<TFrameNumber> player, Metrics metrics, float mix);
  }
}
