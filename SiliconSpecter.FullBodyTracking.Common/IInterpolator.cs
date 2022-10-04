namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Interpolates between <see cref="InterpolatablePlayerKeyframe"/>s.
  /// </summary>
  public interface IInterpolator
  {
    /// <summary>
    /// Interpolates between two given <see cref="InterpolatablePlayerKeyframe"/>s.
    /// </summary>
    /// <param name="from">The <see cref="InterpolatablePlayerKeyframe"/> to interpolate from.</param>
    /// <param name="to">The <see cref="InterpolatablePlayerKeyframe"/> to interpolate to.</param>
    /// <param name="mix">The blend, where zero is <paramref name="from"/> and one is <paramref name="to"/>.</param>
    /// <returns>The requested interpolation from <paramref name="from"/> to <paramref name="to"/>.</returns>
    InterpolatablePlayerKeyframe Interpolate(InterpolatablePlayerKeyframe from, InterpolatablePlayerKeyframe to, float mix);
  }
}
