using System;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <inheritdoc />
  public sealed class Interpolator : IInterpolator
  {
    /// <inheritdoc />
    public InterpolatablePlayerKeyframe Interpolate(InterpolatablePlayerKeyframe from, InterpolatablePlayerKeyframe to, float mix)
    {
      return new InterpolatablePlayerKeyframe
      {
        FacialAnimation = new FacialAnimation
        {
          Emote = to.FacialAnimation.Emote,
          LipRaised = LinearlyInterpolate(from.FacialAnimation.LipRaised, to.FacialAnimation.LipRaised, mix),
          JawLowered = LinearlyInterpolate(from.FacialAnimation.JawLowered, to.FacialAnimation.JawLowered, mix),
          MouthWidth = LinearlyInterpolate(from.FacialAnimation.MouthWidth, to.FacialAnimation.MouthWidth, mix),
        },
        HipsPosition = Vector3.Lerp(from.HipsPosition, to.HipsPosition, mix),
        HipsRotation = Quaternion.Lerp(from.HipsRotation, to.HipsRotation, mix),
        ShouldersRotation = Quaternion.Lerp(from.ShouldersRotation, to.ShouldersRotation, mix),
        HeadRotation = Quaternion.Lerp(from.HeadRotation, to.HeadRotation, mix),
        FacingRotation = Quaternion.Lerp(from.FacingRotation, to.FacingRotation, mix),
        LeftArm = LinearlyInterpolate(from.LeftArm, to.LeftArm, mix),
        RightArm = LinearlyInterpolate(from.RightArm, to.RightArm, mix),
        LeftLeg = LinearlyInterpolate(from.LeftLeg, to.LeftLeg, mix),
        RightLeg = LinearlyInterpolate(from.RightLeg, to.RightLeg, mix),
      };
    }

    private static float LinearlyInterpolate(float from, float to, float mix)
    {
      return from + (to - from) * mix;
    }

    private static InterpolatablePlayerKeyframeLimb LinearlyInterpolate(InterpolatablePlayerKeyframeLimb from, InterpolatablePlayerKeyframeLimb to, float mix)
    {
      return new InterpolatablePlayerKeyframeLimb
      {
        Extension = Vector3.Lerp(from.Extension, to.Extension, mix),
        BendNormal = Vector3.Normalize(Vector3.Lerp(from.BendNormal, to.BendNormal, mix)),
        TipNormal = Vector3.Normalize(Vector3.Lerp(from.TipNormal, to.TipNormal, mix)),
      };
    }
  }
}
