using System;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <inheritdoc />
  public sealed class LimbInverseKinematicsCalculator : ILimbInverseKinematicsCalculator
  {
    /// <inheritdoc />
    public InverseKinematicsLimb Calculate
    (
      Quaternion facingRotation,
      InterpolatablePlayerKeyframeLimb keyframeLimb,
      BindPoseLimb bindPoseLimb,
      float roll
    )
    {
      var proximalLength = Vector3.Distance(bindPoseLimb.ProximalPosition, bindPoseLimb.IntermediatePosition);
      var distalLength = Vector3.Distance(bindPoseLimb.IntermediatePosition, bindPoseLimb.DistalPosition);

      var totalLength = proximalLength + distalLength;
      var normalizedProximalLength = proximalLength / totalLength;
      var normalizedDistalLength = distalLength / totalLength;

      var worldExtension = Vector3.Transform(keyframeLimb.Extension, facingRotation);

      var extension = worldExtension.Length();

      var approximateProximalExtension = extension * normalizedProximalLength;
      var approximateDistalExtension = extension * normalizedDistalLength;

      var proximalRadians = Math.Acos(Math.Max(-1, Math.Min(1, approximateProximalExtension / normalizedProximalLength)));
      var intermediateRadians = -Math.Acos(Math.Max(-1, Math.Min(1, approximateDistalExtension / normalizedDistalLength)));

      var worldBendNormal = Vector3.Transform(keyframeLimb.BendNormal, facingRotation);

      var upNormal = Vector3.Normalize(Vector3.Cross(worldBendNormal, worldExtension));

      if (float.IsNaN(upNormal.X))
      {
        upNormal = new Vector3(0, 1, 0);
      }

      var extensionRotation = Miscellaneous.LookAt(worldExtension, upNormal);

      if (float.IsNaN(extensionRotation.X))
      {
        extensionRotation = Quaternion.Identity;
      }

      var rollRotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), roll);

      return new InverseKinematicsLimb
      {
        Proximal = extensionRotation * Quaternion.CreateFromAxisAngle(new Vector3(0, -1, 0), (float)proximalRadians) * rollRotation,
        Intermediate = extensionRotation * Quaternion.CreateFromAxisAngle(new Vector3(0, -1, 0), (float)intermediateRadians) * rollRotation,
        Distal = Miscellaneous.LookAt(Vector3.Transform(keyframeLimb.TipNormal, facingRotation), upNormal) * rollRotation,
      };
    }
  }
}
