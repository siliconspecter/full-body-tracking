using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <inheritdoc />
  public sealed class LimbMeasurer : ILimbMeasurer
  {
    /// <inheritdoc />
    public float Measure(Limb limb, float previousLength)
    {
      if (!limb.Extension.HasValue || !limb.Extension.Value.IntermediatePosition.HasValue)
      {
        return previousLength;
      }

      return Vector3.Distance(limb.ProximalPosition, limb.Extension.Value.IntermediatePosition.Value) + Vector3.Distance(limb.Extension.Value.IntermediatePosition.Value, limb.Extension.Value.DistalPosition);
    }
  }
}
