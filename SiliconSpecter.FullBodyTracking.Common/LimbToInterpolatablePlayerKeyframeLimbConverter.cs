using System;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <inheritdoc />
  public sealed class LimbToInterpolatablePlayerKeyframeLimbConverter : ILimbToInterpolatablePlayerKeyframeLimbConverter
  {
    /// <inheritdoc />
    public InterpolatablePlayerKeyframeLimb Convert(Limb limb, InterpolatablePlayerKeyframeLimb? previousKeyframe, float length, Vector3 defaultBendNormal, Vector3 defaultTipNormal, Quaternion cameraToInverseFacingRotation)
    {
      var output = previousKeyframe ?? new InterpolatablePlayerKeyframeLimb
      {
        ExtensionNormal = new Vector3(0, -1, 0),
        ExtensionProportion = 1,
        BendNormal = defaultBendNormal,
        TipNormal = defaultTipNormal,
      };

      if (limb.Extension.HasValue)
      {
        if (limb.ProximalPosition != limb.Extension.Value.DistalPosition)
        {
          output.ExtensionNormal = Vector3.Normalize(Vector3.Transform(limb.Extension.Value.DistalPosition - limb.ProximalPosition, cameraToInverseFacingRotation));
          output.ExtensionProportion = Math.Max(0, Math.Min(1, Vector3.Distance(limb.ProximalPosition, limb.Extension.Value.DistalPosition) / length));

          if (limb.Extension.Value.IntermediatePosition.HasValue)
          {
            var proximalToIntermediate = Vector3.Transform(limb.Extension.Value.IntermediatePosition.Value - limb.ProximalPosition, cameraToInverseFacingRotation);

            var elbowDirection = proximalToIntermediate - output.ExtensionNormal * Vector3.Dot(output.ExtensionNormal, proximalToIntermediate);

            if (elbowDirection.LengthSquared() > 0.000625f)
            {
              output.BendNormal = Vector3.Normalize(elbowDirection);
            }
          }
        }

        if (limb.Extension.Value.TipPosition != limb.Extension.Value.DistalPosition)
        {
          output.TipNormal = Vector3.Normalize(Vector3.Transform(limb.Extension.Value.TipPosition - limb.Extension.Value.DistalPosition, cameraToInverseFacingRotation));
        }
      }

      return output;
    }
  }
}
