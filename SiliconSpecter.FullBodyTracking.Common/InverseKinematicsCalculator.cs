using System;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <inheritdoc />
  public sealed class InverseKinematicsCalculator : IInverseKinematicsCalculator
  {
    /// <summary>
    /// The <see cref="ILimbInverseKinematicsCalculator"/> in use.
    /// </summary>
    public readonly ILimbInverseKinematicsCalculator LimbInverseKinematicsCalculator;

    /// <inheritdoc />
    /// <param name="limbInverseKinematicsCalculator">
    /// The <see cref="ILimbInverseKinematicsCalculator"/> to use.
    /// </param>
    public InverseKinematicsCalculator(ILimbInverseKinematicsCalculator limbInverseKinematicsCalculator)
    {
      LimbInverseKinematicsCalculator = limbInverseKinematicsCalculator;
    }

    /// <inheritdoc />
    public InverseKinematics Calculate(InterpolatablePlayerKeyframe keyframe, BindPose bindPose)
    {
      var leftLegLength = Vector3.Distance(bindPose.LeftLeg.ProximalPosition, bindPose.LeftLeg.IntermediatePosition) + Vector3.Distance(bindPose.LeftLeg.IntermediatePosition, bindPose.LeftLeg.DistalPosition);
      var rightLegLength = Vector3.Distance(bindPose.RightLeg.ProximalPosition, bindPose.RightLeg.IntermediatePosition) + Vector3.Distance(bindPose.RightLeg.IntermediatePosition, bindPose.RightLeg.DistalPosition);

      return new InverseKinematics
      {
        HipsY = Math.Max(leftLegLength, rightLegLength),
        HipsRotation = keyframe.FacingRotation * keyframe.HipsRotation,
        ShoulderBone = keyframe.FacingRotation * keyframe.ShouldersRotation,
        LeftArm = LimbInverseKinematicsCalculator.Calculate(keyframe.FacingRotation, keyframe.LeftArm, bindPose.LeftArm, 0),
        RightArm = LimbInverseKinematicsCalculator.Calculate(keyframe.FacingRotation, keyframe.RightArm, bindPose.RightArm, (float)Math.PI),
        LeftLeg = LimbInverseKinematicsCalculator.Calculate(keyframe.FacingRotation, keyframe.LeftLeg, bindPose.LeftLeg, (float)Math.PI / 2),
        RightLeg = LimbInverseKinematicsCalculator.Calculate(keyframe.FacingRotation, keyframe.RightLeg, bindPose.RightLeg, (float)Math.PI / 2),
      };
    }
  }
}
