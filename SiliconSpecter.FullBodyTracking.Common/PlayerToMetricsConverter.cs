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
    public Metrics Convert(Player<TFrameNumber> player, Metrics? metrics)
    {
      var output = metrics ?? new Metrics
      {
        LeftArmLength = 0.635f,
        RightArmLength = 0.635f,
        LeftLegLength = 0.8125f,
        RightLegLength = 0.8125f,
      };

      if (player.Details.HasValue)
      {
        output.LeftArmLength = LimbMeasurer.Measure(player.Details.Value.LeftArm, output.LeftArmLength);
        output.RightArmLength = LimbMeasurer.Measure(player.Details.Value.RightArm, output.RightArmLength);
        output.LeftLegLength = LimbMeasurer.Measure(player.Details.Value.LeftLeg, output.LeftLegLength);
        output.RightLegLength = LimbMeasurer.Measure(player.Details.Value.RightLeg, output.RightLegLength);
      }

      return output;
    }
  }
}
