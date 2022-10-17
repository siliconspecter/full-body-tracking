using System;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <inheritdoc />
  public sealed class PlayerToKeyframeConverterTasks : IPlayerToKeyframeConverterTasks
  {
    /// <inheritdoc />
    public Quaternion ConvertHead(Quaternion facingRotation, Quaternion inverseFacingRotation, Quaternion headRotation, Vector3? headForwardNormal, Vector3? headUpNormal, Quaternion cameraRotation, Quaternion cameraToInverseFacingRotation)
    {
      if (headForwardNormal.HasValue && headUpNormal.HasValue)
      {
        headRotation = Miscellaneous.LookAt(headForwardNormal.Value, headUpNormal.Value) * cameraToInverseFacingRotation;
      }
      else if (headForwardNormal.HasValue)
      {
        headRotation = Miscellaneous.LookAt(Vector3.Transform(headForwardNormal.Value, cameraRotation), Vector3.Transform(new Vector3(0, 1, 0), headRotation * facingRotation)) * inverseFacingRotation;
      }
      else if (headUpNormal.HasValue)
      {
        headRotation = Miscellaneous.LookAt(Vector3.Transform(new Vector3(0, 0, 1), headRotation * facingRotation), Vector3.Transform(headUpNormal.Value, cameraRotation)) * inverseFacingRotation;
      }

      return headRotation;
    }

    /// <inheritdoc />
    public InterpolatablePlayerKeyframeLimb ConvertLimb(Limb limb, InterpolatablePlayerKeyframeLimb previousKeyframe, Vector3 fallbackBendNormal, float lockedWhenIntermediateDistanceLessThan, float length, Quaternion cameraToInverseFacingRotation)
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

            var bendDirection = proximalToIntermediate - extensionNormal * Vector3.Dot(extensionNormal, proximalToIntermediate);

            if (bendDirection.LengthSquared() > lockedWhenIntermediateDistanceLessThan * lockedWhenIntermediateDistanceLessThan)
            {
              previousKeyframe.BendNormal = Vector3.Normalize(bendDirection);
            }
          }
        }

        if (limb.Extension.Value.TipPosition.HasValue && limb.Extension.Value.TipPosition != limb.Extension.Value.DistalPosition)
        {
          previousKeyframe.TipNormal = Vector3.Normalize(Vector3.Transform(limb.Extension.Value.TipPosition.Value - limb.Extension.Value.DistalPosition, cameraToInverseFacingRotation));
        }
        else if (limb.Extension.Value.IntermediatePosition.HasValue && limb.Extension.Value.IntermediatePosition.Value != limb.Extension.Value.DistalPosition)
        {
          previousKeyframe.TipNormal = Vector3.Normalize(Vector3.Transform(limb.Extension.Value.DistalPosition - limb.Extension.Value.IntermediatePosition.Value, cameraToInverseFacingRotation));
        }
        else
        {
          previousKeyframe.TipNormal = Vector3.Normalize(previousKeyframe.Extension);
        }
      }

      return previousKeyframe;
    }
  }
}
