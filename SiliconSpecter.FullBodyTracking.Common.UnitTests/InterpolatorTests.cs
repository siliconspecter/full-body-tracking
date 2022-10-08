using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SiliconSpecter.FullBodyTracking.Common.UnitTests;

[TestClass]
public sealed class InterpolatorTests
{
  [TestMethod]
  public void InterpolatesBetweenGivenKeyframes()
  {
    var from = new InterpolatablePlayerKeyframe
    {
      FacialAnimation = new FacialAnimation
      {
        Emote = Emote.Surprised,
        LipRaised = -0.43f,
        JawLowered = 0.98f,
        MouthWidth = 0.4f,
      },
      Position = new Vector3(0.638f, 0.346f, 0.779f),
      HipsRotation = Quaternion.CreateFromYawPitchRoll(0.387f, 0.948f, 0.321f),
      ShouldersRotation = Quaternion.CreateFromYawPitchRoll(0.718f, 0.424f, 0.893f),
      HeadRotation = Quaternion.CreateFromYawPitchRoll(0.968f, 0.195f, 0.642f),
      FacingRotation = Quaternion.CreateFromYawPitchRoll(0.225f, 0.613f, 0.495f),
      LeftArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.519f, 0.921f, 0.56f),
        BendNormal = new Vector3(0.176f, 0.226f, 0.561f),
        TipNormal = new Vector3(0.188f, 0.281f, 0.129f),
      },
      RightArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.258f, 0.095f, 0.917f),
        BendNormal = new Vector3(0.774f, 0.286f, 0.442f),
        TipNormal = new Vector3(0.027f, 0.322f, 0.499f),
      },
      LeftLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.56f, 0.841f, 0.41f),
        BendNormal = new Vector3(0.499f, 0.229f, 0.194f),
        TipNormal = new Vector3(0.288f, 0.421f, 0.274f),
      },
      RightLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.001f, 0.938f, 0.628f),
        BendNormal = new Vector3(0.516f, 0.374f, 0.032f),
        TipNormal = new Vector3(0.566f, 0.631f, 0.674f),
      },
    };
    var to = new InterpolatablePlayerKeyframe
    {
      FacialAnimation = new FacialAnimation
      {
        Emote = Emote.Joy,
        LipRaised = 0.525f,
        JawLowered = 0.57f,
        MouthWidth = 0.784f,
      },
      Position = new Vector3(0.448f, 0.425f, 0.622f),
      HipsRotation = Quaternion.CreateFromYawPitchRoll(0.105f, 0.81f, 0.256f),
      ShouldersRotation = Quaternion.CreateFromYawPitchRoll(0.465f, 0.578f, 0.515f),
      HeadRotation = Quaternion.CreateFromYawPitchRoll(0.55f, 0.161f, 0.585f),
      FacingRotation = Quaternion.CreateFromYawPitchRoll(0.623f, 0.542f, 0.482f),
      LeftArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.758f, 0.219f, 0.451f),
        BendNormal = new Vector3(0.583f, 0.721f, 0.643f),
        TipNormal = new Vector3(0.47f, 0.615f, 0.569f),
      },
      RightArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.6f, 0.695f, 0.467f),
        BendNormal = new Vector3(0.296f, 0.15f, 0.49f),
        TipNormal = new Vector3(0.773f, 0.615f, 0.151f),
      },
      LeftLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.299f, 0.602f, 0.822f),
        BendNormal = new Vector3(0.507f, 0.315f, 0.239f),
        TipNormal = new Vector3(0.647f, 0.687f, 0.549f),
      },
      RightLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.676f, 0.879f, 0.689f),
        BendNormal = new Vector3(0.567f, 0.923f, 0.42f),
        TipNormal = new Vector3(0.204f, 0.152f, 0.395f),
      },
    };
    var interpolator = new Interpolator();

    var actual = interpolator.Interpolate(from, to, 0.73f);

    Assert.AreEqual(Emote.Joy, actual.FacialAnimation.Emote);
    Assert.AreEqual(0.26714998f, actual.FacialAnimation.LipRaised);
    Assert.AreEqual(0.6807f, actual.FacialAnimation.JawLowered);
    Assert.AreEqual(0.68032f, actual.FacialAnimation.MouthWidth);
    Assert.AreEqual(new Vector3(0.4993f, 0.40367f, 0.66438997f), actual.Position);
    Assert.AreEqual(new Quaternion(0.41691497f, 0.024573192f, 0.08499536f, 0.9046291f), actual.HipsRotation);
    Assert.AreEqual(new Quaternion(0.3259561f, 0.16559158f, 0.21648727f, 0.9052432f), actual.ShouldersRotation);
    Assert.AreEqual(new Quaternion(0.17332587f, 0.28541598f, 0.25115985f, 0.90852326f), actual.HeadRotation);
    Assert.AreEqual(new Quaternion(0.31897894f, 0.17385356f, 0.15607862f, 0.9185133f), actual.FacingRotation);
    Assert.AreEqual(new Vector3(0.69347f, 0.40853998f, 0.48043f), actual.LeftArm.Extension);
    Assert.AreEqual(new Vector3(0.4843108f, 0.6012555f, 0.6355587f), actual.LeftArm.BendNormal);
    Assert.AreEqual(new Vector3(0.49494517f, 0.6595164f, 0.565745f), actual.LeftArm.TipNormal);
    Assert.AreEqual(new Vector3(0.50766003f, 0.53300005f, 0.5885f), actual.RightArm.Extension);
    Assert.AreEqual(new Vector3(0.63855106f, 0.2805022f, 0.7166387f), actual.RightArm.BendNormal);
    Assert.AreEqual(new Vector3(0.69627917f, 0.6528028f, 0.2984019f), actual.RightArm.TipNormal);
    Assert.AreEqual(new Vector3(0.36947f, 0.66653f, 0.71076006f), actual.LeftLeg.Extension);
    Assert.AreEqual(new Vector3(0.8068822f, 0.46634987f, 0.3625727f), actual.LeftLeg.BendNormal);
    Assert.AreEqual(new Vector3(0.57777053f, 0.64615935f, 0.49865755f), actual.LeftLeg.TipNormal);
    Assert.AreEqual(new Vector3(0.49375004f, 0.89493f, 0.67253006f), actual.RightLeg.Extension);
    Assert.AreEqual(new Vector3(0.5516578f, 0.77256817f, 0.31434414f), actual.RightLeg.BendNormal);
    Assert.AreEqual(new Vector3(0.48230338f, 0.44967988f, 0.75177884f), actual.RightLeg.TipNormal);
  }
}
