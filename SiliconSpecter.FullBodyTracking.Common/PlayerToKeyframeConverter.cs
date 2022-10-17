using System;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <inheritdoc />
  public sealed class PlayerToKeyframeConverter<TFrameNumber> : IPlayerToKeyframeConverter<TFrameNumber>
  {
    /// <summary>
    /// The <see cref="IPlayerToKeyframeConverterTasks"/> in use.
    /// </summary>
    public readonly IPlayerToKeyframeConverterTasks PlayerToKeyframeConverterTasks;

    /// <inheritdoc />
    /// <param name="playerToKeyframeConverterTasks">
    /// The <see cref="IPlayerToKeyframeConverterTasks"/> to use.
    /// </param>
    public PlayerToKeyframeConverter(IPlayerToKeyframeConverterTasks playerToKeyframeConverterTasks)
    {
      PlayerToKeyframeConverterTasks = playerToKeyframeConverterTasks;
    }

    /// <inheritdoc />
    public InterpolatablePlayerKeyframe Convert(Player<TFrameNumber> player, InterpolatablePlayerKeyframe previousKeyframe, Metrics metrics, Vector3 cameraPosition, Quaternion cameraRotation)
    {
      previousKeyframe.Position = TransformPosition(player.ApproximatePosition, cameraPosition, cameraRotation);

      if (!player.Details.HasValue)
      {
        return previousKeyframe;
      }

      var details = player.Details.Value;

      if (details.FacialAnimation.HasValue)
      {
        previousKeyframe.FacialAnimation = details.FacialAnimation.Value;
      }

      var leftHipPosition = TransformPosition(details.LeftLeg.ProximalPosition, cameraPosition, cameraRotation);
      var rightHipPosition = TransformPosition(details.RightLeg.ProximalPosition, cameraPosition, cameraRotation);

      var leftShoulderPosition = TransformPosition(details.LeftArm.ProximalPosition, cameraPosition, cameraRotation);
      var rightShoulderPosition = TransformPosition(details.RightArm.ProximalPosition, cameraPosition, cameraRotation);

      if (leftHipPosition == rightHipPosition)
      {
        return previousKeyframe;
      }

      var hipCenterPosition = Vector3.Lerp(leftHipPosition, rightHipPosition, 0.5f);

      var position = hipCenterPosition;
      position.Y -= Math.Max(metrics.LeftLegLength, metrics.RightLegLength);
      previousKeyframe.Position = position;

      if (leftShoulderPosition == rightShoulderPosition)
      {
        return previousKeyframe;
      }

      var shoulderCenterPosition = Vector3.Lerp(leftShoulderPosition, rightShoulderPosition, 0.5f);

      if (hipCenterPosition == shoulderCenterPosition)
      {
        return previousKeyframe;
      }

      var hipCenterToShoulderCenterNormal = Vector3.Normalize(shoulderCenterPosition - hipCenterPosition);

      var leftHipToRightHipNormal = Vector3.Normalize(rightHipPosition - leftHipPosition);
      var hipsForwardNormal = Vector3.Normalize(Vector3.Cross(leftHipToRightHipNormal, hipCenterToShoulderCenterNormal));
      var hipsUpNormal = Vector3.Normalize(Vector3.Cross(hipsForwardNormal, leftHipToRightHipNormal));
      previousKeyframe.HipsRotation = Miscellaneous.LookAt(hipsForwardNormal, hipsUpNormal);

      var leftShoulderToRightShoulderNormal = Vector3.Normalize(rightShoulderPosition - leftShoulderPosition);
      var shouldersForwardNormal = Vector3.Normalize(Vector3.Cross(leftShoulderToRightShoulderNormal, hipCenterToShoulderCenterNormal));
      var shouldersUpNormal = Vector3.Normalize(Vector3.Cross(shouldersForwardNormal, leftShoulderToRightShoulderNormal));
      previousKeyframe.ShouldersRotation = Miscellaneous.LookAt(shouldersForwardNormal, shouldersUpNormal);

      previousKeyframe.FacingRotation = Miscellaneous.LookAt(Vector3.Normalize(Vector3.Lerp(hipsForwardNormal, shouldersForwardNormal, 0.5f) * new Vector3(1, 0, 1)), new Vector3(0, 1, 0));

      previousKeyframe.HipsRotation /= previousKeyframe.FacingRotation;
      previousKeyframe.ShouldersRotation /= previousKeyframe.FacingRotation;

      var inverseFacingRotation = Quaternion.Inverse(previousKeyframe.FacingRotation);

      var cameraToInverseFacingRotation = inverseFacingRotation * cameraRotation;

      previousKeyframe.HeadRotation = PlayerToKeyframeConverterTasks.ConvertHead(previousKeyframe.FacingRotation, inverseFacingRotation, previousKeyframe.HeadRotation, details.HeadForwardNormal, details.HeadUpNormal, cameraRotation, cameraToInverseFacingRotation);

      previousKeyframe.LeftArm = PlayerToKeyframeConverterTasks.ConvertLimb(details.LeftArm, previousKeyframe.LeftArm, previousKeyframe.LeftArm.BendNormal, 0.025f, metrics.LeftArmLength, cameraToInverseFacingRotation);
      previousKeyframe.RightArm = PlayerToKeyframeConverterTasks.ConvertLimb(details.RightArm, previousKeyframe.RightArm, previousKeyframe.RightArm.BendNormal, 0.025f, metrics.RightArmLength, cameraToInverseFacingRotation);
      previousKeyframe.LeftLeg = PlayerToKeyframeConverterTasks.ConvertLimb(details.LeftLeg, previousKeyframe.LeftLeg, new Vector3(0, 0, 1), 0.125f, metrics.LeftLegLength, cameraToInverseFacingRotation);
      previousKeyframe.RightLeg = PlayerToKeyframeConverterTasks.ConvertLimb(details.RightLeg, previousKeyframe.RightLeg, new Vector3(0, 0, 1), 0.125f, metrics.RightLegLength, cameraToInverseFacingRotation);

      return previousKeyframe;
    }

    private static Vector3 TransformPosition(Vector3 position, Vector3 cameraPosition, Quaternion cameraRotation)
    {
      return Vector3.Transform(position, cameraRotation) + cameraPosition;
    }
  }
}
