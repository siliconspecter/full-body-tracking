using System;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <inheritdoc />
  public sealed class LimbToInterpolatablePlayerKeyframeLimbConverter : ILimbToInterpolatablePlayerKeyframeLimbConverter
  {
    /// <inheritdoc />
    public InterpolatablePlayerKeyframeLimb Convert(Limb limb, InterpolatablePlayerKeyframeLimb previousKeyframe, Vector3 fallbackBendNormal, float lockedWhenIntermediateDistanceLessThan, float length, Quaternion cameraToInverseFacingRotation)
    {
      previousKeyframe.BendNormal = fallbackBendNormal;

      if (limb.Extension.HasValue)
      {
        if (limb.ProximalPosition != limb.Extension.Value.DistalPosition)
        {
          var extensionNormal = Vector3.Normalize(Vector3.Transform(limb.Extension.Value.DistalPosition - limb.ProximalPosition, cameraToInverseFacingRotation));
          previousKeyframe.Extension = extensionNormal * Math.Max(0, Math.Min(1, Vector3.Distance(limb.ProximalPosition, limb.Extension.Value.DistalPosition) / length));

          if (limb.Extension.Value.IntermediatePosition.HasValue)
          {
            var proximalToIntermediate = Vector3.Transform(limb.Extension.Value.IntermediatePosition.Value - limb.ProximalPosition, cameraToInverseFacingRotation);

            var elbowDirection = proximalToIntermediate - extensionNormal * Vector3.Dot(extensionNormal, proximalToIntermediate);

            if (elbowDirection.LengthSquared() > lockedWhenIntermediateDistanceLessThan * lockedWhenIntermediateDistanceLessThan)
            {
              previousKeyframe.BendNormal = Vector3.Normalize(elbowDirection);
            }
          }
        }

        if (limb.Extension.Value.TipPosition != limb.Extension.Value.DistalPosition)
        {
          previousKeyframe.TipNormal = Vector3.Normalize(Vector3.Transform(limb.Extension.Value.TipPosition - limb.Extension.Value.DistalPosition, cameraToInverseFacingRotation));
        }
      }

      return previousKeyframe;
    }
  }
}
