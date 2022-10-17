namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <inheritdoc />
  public sealed class PlayerToMetricsConverter<TFrameNumber> : IPlayerToMetricsConverter<TFrameNumber>
  {
    /// <summary>
    /// The <see cref="ILimbMeasurer"/> in use.
    /// </summary>
    public readonly ILimbMeasurer LimbMeasurer;

    /// <inheritdoc />
    /// <param name="limbMeasurer">The <see cref="ILimbMeasurer"/> to use.</param>
    public PlayerToMetricsConverter(ILimbMeasurer limbMeasurer)
    {
      LimbMeasurer = limbMeasurer;
    }

    /// <inheritdoc />
    public Metrics Convert(Player<TFrameNumber> player, Metrics metrics, float mix)
    {
      if (player.Details.HasValue)
      {
        metrics.LeftArmLength = LimbMeasurer.Measure(player.Details.Value.LeftArm, metrics.LeftArmLength, mix);
        metrics.RightArmLength = LimbMeasurer.Measure(player.Details.Value.RightArm, metrics.RightArmLength, mix);
        metrics.LeftLegLength = LimbMeasurer.Measure(player.Details.Value.LeftLeg, metrics.LeftLegLength, mix);
        metrics.RightLegLength = LimbMeasurer.Measure(player.Details.Value.RightLeg, metrics.RightLegLength, mix);
      }

      return metrics;
    }
  }
}
