namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Measures the lengths of <see cref="Limb"/>s.
  /// </summary>
  public interface ILimbMeasurer
  {
    /// <summary>
    /// Measures the length of a given <see cref="Limb"/>.
    /// </summary>
    /// <param name="limb">The <see cref="Limb"/> to measure.</param>
    /// <param name="previousLength">The previous measurement of length.</param>
    /// <returns>The length of the given <paramref name="limb"/>, or <paramref name="previousLength"/> when it is unable to be calculated.</returns>
    float Measure(Limb limb, float previousLength);
  }
}
