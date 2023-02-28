using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common.UnitTests;

[TestClass]
public sealed class InverseKinematicsCalculatorTests
{
  [TestMethod]
  public void ExposesInjectedDependencies()
  {
    var limbInverseKinematicsCalculator = new Mock<ILimbInverseKinematicsCalculator>();

    var inverseKinematicsCalculator = new InverseKinematicsCalculator(limbInverseKinematicsCalculator.Object);

    Assert.AreSame(limbInverseKinematicsCalculator.Object, inverseKinematicsCalculator.LimbInverseKinematicsCalculator);
    limbInverseKinematicsCalculator.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void CalculatesInverseKinematicsWhenLeftLegIsLongest()
  {
    var limbInverseKinematicsCalculator = new Mock<ILimbInverseKinematicsCalculator>();
    var inverseKinematicsCalculator = new InverseKinematicsCalculator(limbInverseKinematicsCalculator.Object);
    var facingRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);
    var keyframeLeftArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.526f, 0.358f, 0.940f),
      BendNormal = new Vector3(0.184f, 0.135f, 0.375f),
      TipNormal = new Vector3(0.630f, 0.312f, 0.635f),
      TipLength = 0.376f,
    };
    var keyframeRightArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.351f, 0.618f, 0.219f),
      BendNormal = new Vector3(0.256f, 0.994f, 0.228f),
      TipNormal = new Vector3(0.636f, 0.038f, 0.551f),
      TipLength = 0.360f,
    };
    var keyframeLeftLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.278f, 0.053f, 0.166f),
      BendNormal = new Vector3(0.761f, 0.848f, 0.046f),
      TipNormal = new Vector3(0.802f, 0.624f, 0.401f),
      TipLength = 0.495f,
    };
    var keyframeRightLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.074f, 0.881f, 0.708f),
      BendNormal = new Vector3(0.114f, 0.940f, 0.551f),
      TipNormal = new Vector3(0.316f, 0.938f, 0.898f),
      TipLength = 0.400f,
    };
    var keyframe = new InterpolatablePlayerKeyframe
    {
      FacialAnimation = new FacialAnimation
      {
        Emote = Emote.Joy,
        LipRaised = 0.239f,
        JawLowered = 0.721f,
        MouthWidth = 0.203f,
      },
      Position = new Vector3(0.877f, 0.596f, 0.360f),
      HipsRotation = Quaternion.CreateFromYawPitchRoll(0.429f, 0.133f, 0.553f),
      ShouldersRotation = Quaternion.CreateFromYawPitchRoll(0.481f, 0.873f, 0.822f),
      HeadRotation = Quaternion.CreateFromYawPitchRoll(0.269f, 0.023f, 0.875f),
      FacingRotation = facingRotation,
      LeftArm = keyframeLeftArm,
      RightArm = keyframeRightArm,
      LeftLeg = keyframeLeftLeg,
      RightLeg = keyframeRightLeg,
    };
    var bindPoseLeftArm = new BindPoseLimb
    {
      ProximalPosition = new Vector3(4.19f, -0.1f, -0.33f),
      IntermediatePosition = new Vector3(2.69f, 7.29f, -1.42f),
      DistalPosition = new Vector3(-0.33f, -5.24f, 2.08f),
    };
    var bindPoseRightArm = new BindPoseLimb
    {
      ProximalPosition = new Vector3(-2.46f, -8.85f, 1.51f),
      IntermediatePosition = new Vector3(-1.47f, -4.96f, 4.73f),
      DistalPosition = new Vector3(3.39f, -5.25f, -8.38f),
    };
    var bindPoseLeftLeg = new BindPoseLimb
    {
      ProximalPosition = new Vector3(-5.11f, -7.15f, -2.98f),
      IntermediatePosition = new Vector3(-3.81f, 4.85f, 3.61f),
      DistalPosition = new Vector3(-0.32f, -3.71f, 0.33f),
    };
    var bindPoseRightLeg = new BindPoseLimb
    {
      ProximalPosition = new Vector3(1.11f, 3.28f, -5.75f),
      IntermediatePosition = new Vector3(1.87f, -0.49f, -5.14f),
      DistalPosition = new Vector3(-3.43f, -1.27f, 1.32f),
    };
    var bindPose = new BindPose
    {
      LeftArm = bindPoseLeftArm,
      RightArm = bindPoseRightArm,
      LeftLeg = bindPoseLeftLeg,
      RightLeg = bindPoseRightLeg,
    };
    var inverseKinematicsLeftArm = new InverseKinematicsLimb
    {
      Proximal = Quaternion.CreateFromYawPitchRoll(0.926f, 0.983f, 0.092f),
      Intermediate = Quaternion.CreateFromYawPitchRoll(0.503f, 0.214f, 0.216f),
      Distal = Quaternion.CreateFromYawPitchRoll(0.959f, 0.970f, 0.800f),
    };
    var inverseKinematicsRightArm = new InverseKinematicsLimb
    {
      Proximal = Quaternion.CreateFromYawPitchRoll(0.716f, 0.448f, 0.946f),
      Intermediate = Quaternion.CreateFromYawPitchRoll(0.680f, 0.039f, 0.125f),
      Distal = Quaternion.CreateFromYawPitchRoll(0.233f, 0.747f, 0.840f),
    };
    var inverseKinematicsLeftLeg = new InverseKinematicsLimb
    {
      Proximal = Quaternion.CreateFromYawPitchRoll(0.151f, 0.732f, 0.142f),
      Intermediate = Quaternion.CreateFromYawPitchRoll(0.098f, 0.405f, 0.025f),
      Distal = Quaternion.CreateFromYawPitchRoll(0.909f, 0.323f, 0.117f),
    };
    var inverseKinematicsRightLeg = new InverseKinematicsLimb
    {
      Proximal = Quaternion.CreateFromYawPitchRoll(0.525f, 0.195f, 0.934f),
      Intermediate = Quaternion.CreateFromYawPitchRoll(0.348f, 0.857f, 0.845f),
      Distal = Quaternion.CreateFromYawPitchRoll(0.353f, 0.582f, 0.231f),
    };
    limbInverseKinematicsCalculator.Setup(x => x.Calculate(It.IsAny<Quaternion>(), keyframeLeftArm, It.IsAny<BindPoseLimb>(), It.IsAny<float>())).Returns(inverseKinematicsLeftArm);
    limbInverseKinematicsCalculator.Setup(x => x.Calculate(It.IsAny<Quaternion>(), keyframeRightArm, It.IsAny<BindPoseLimb>(), It.IsAny<float>())).Returns(inverseKinematicsRightArm);
    limbInverseKinematicsCalculator.Setup(x => x.Calculate(It.IsAny<Quaternion>(), keyframeLeftLeg, It.IsAny<BindPoseLimb>(), It.IsAny<float>())).Returns(inverseKinematicsLeftLeg);
    limbInverseKinematicsCalculator.Setup(x => x.Calculate(It.IsAny<Quaternion>(), keyframeRightLeg, It.IsAny<BindPoseLimb>(), It.IsAny<float>())).Returns(inverseKinematicsRightLeg);

    var actual = inverseKinematicsCalculator.Calculate(keyframe, bindPose);

    Assert.AreEqual(23.560799, actual.HipsY, 0.0001);
    Assert.AreEqual(new Quaternion(-0.052647516f, -0.9274902f, -0.36805812f, 0.03903252f), actual.HipsRotation);
    Assert.AreEqual(new Quaternion(-0.219598f, -0.968671f, -0.052487105f, -0.103431895f), actual.ShoulderBone);
    Assert.AreEqual(inverseKinematicsLeftArm, actual.LeftArm);
    Assert.AreEqual(inverseKinematicsRightArm, actual.RightArm);
    Assert.AreEqual(inverseKinematicsLeftLeg, actual.LeftLeg);
    Assert.AreEqual(inverseKinematicsRightLeg, actual.RightLeg);
    limbInverseKinematicsCalculator.Verify(x => x.Calculate(facingRotation, keyframeLeftArm, bindPoseLeftArm, 0), Times.Once());
    limbInverseKinematicsCalculator.Verify(x => x.Calculate(facingRotation, keyframeRightArm, bindPoseRightArm, 3.14159265359f), Times.Once());
    limbInverseKinematicsCalculator.Verify(x => x.Calculate(facingRotation, keyframeLeftLeg, bindPoseLeftLeg, 1.57079632679f), Times.Once());
    limbInverseKinematicsCalculator.Verify(x => x.Calculate(facingRotation, keyframeRightLeg, bindPoseRightLeg, 1.57079632679f), Times.Once());
    limbInverseKinematicsCalculator.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void CalculatesInverseKinematicsWhenRightLegIsLongest()
  {
    var limbInverseKinematicsCalculator = new Mock<ILimbInverseKinematicsCalculator>();
    var inverseKinematicsCalculator = new InverseKinematicsCalculator(limbInverseKinematicsCalculator.Object);
    var facingRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);
    var keyframeLeftArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.526f, 0.358f, 0.940f),
      BendNormal = new Vector3(0.184f, 0.135f, 0.375f),
      TipNormal = new Vector3(0.630f, 0.312f, 0.635f),
      TipLength = 0.376f,
    };
    var keyframeRightArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.351f, 0.618f, 0.219f),
      BendNormal = new Vector3(0.256f, 0.994f, 0.228f),
      TipNormal = new Vector3(0.636f, 0.038f, 0.551f),
      TipLength = 0.360f,
    };
    var keyframeLeftLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.278f, 0.053f, 0.166f),
      BendNormal = new Vector3(0.761f, 0.848f, 0.046f),
      TipNormal = new Vector3(0.802f, 0.624f, 0.401f),
      TipLength = 0.495f,
    };
    var keyframeRightLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.074f, 0.881f, 0.708f),
      BendNormal = new Vector3(0.114f, 0.940f, 0.551f),
      TipNormal = new Vector3(0.316f, 0.938f, 0.898f),
      TipLength = 0.400f,
    };
    var keyframe = new InterpolatablePlayerKeyframe
    {
      FacialAnimation = new FacialAnimation
      {
        Emote = Emote.Joy,
        LipRaised = 0.239f,
        JawLowered = 0.721f,
        MouthWidth = 0.203f,
      },
      Position = new Vector3(0.877f, 0.596f, 0.360f),
      HipsRotation = Quaternion.CreateFromYawPitchRoll(0.429f, 0.133f, 0.553f),
      ShouldersRotation = Quaternion.CreateFromYawPitchRoll(0.481f, 0.873f, 0.822f),
      HeadRotation = Quaternion.CreateFromYawPitchRoll(0.269f, 0.023f, 0.875f),
      FacingRotation = facingRotation,
      LeftArm = keyframeLeftArm,
      RightArm = keyframeRightArm,
      LeftLeg = keyframeLeftLeg,
      RightLeg = keyframeRightLeg,
    };
    var bindPoseLeftArm = new BindPoseLimb
    {
      ProximalPosition = new Vector3(4.19f, -0.1f, -0.33f),
      IntermediatePosition = new Vector3(2.69f, 7.29f, -1.42f),
      DistalPosition = new Vector3(-0.33f, -5.24f, 2.08f),
    };
    var bindPoseRightArm = new BindPoseLimb
    {
      ProximalPosition = new Vector3(-2.46f, -8.85f, 1.51f),
      IntermediatePosition = new Vector3(-1.47f, -4.96f, 4.73f),
      DistalPosition = new Vector3(3.39f, -5.25f, -8.38f),
    };
    var bindPoseLeftLeg = new BindPoseLimb
    {
      ProximalPosition = new Vector3(1.11f, 3.28f, -5.75f),
      IntermediatePosition = new Vector3(1.87f, -0.49f, -5.14f),
      DistalPosition = new Vector3(-3.43f, -1.27f, 1.32f),
    };
    var bindPoseRightLeg = new BindPoseLimb
    {
      ProximalPosition = new Vector3(-5.11f, -7.15f, -2.98f),
      IntermediatePosition = new Vector3(-3.81f, 4.85f, 3.61f),
      DistalPosition = new Vector3(-0.32f, -3.71f, 0.33f),
    };
    var bindPose = new BindPose
    {
      LeftArm = bindPoseLeftArm,
      RightArm = bindPoseRightArm,
      LeftLeg = bindPoseLeftLeg,
      RightLeg = bindPoseRightLeg,
    };
    var inverseKinematicsLeftArm = new InverseKinematicsLimb
    {
      Proximal = Quaternion.CreateFromYawPitchRoll(0.926f, 0.983f, 0.092f),
      Intermediate = Quaternion.CreateFromYawPitchRoll(0.503f, 0.214f, 0.216f),
      Distal = Quaternion.CreateFromYawPitchRoll(0.959f, 0.970f, 0.800f),
    };
    var inverseKinematicsRightArm = new InverseKinematicsLimb
    {
      Proximal = Quaternion.CreateFromYawPitchRoll(0.716f, 0.448f, 0.946f),
      Intermediate = Quaternion.CreateFromYawPitchRoll(0.680f, 0.039f, 0.125f),
      Distal = Quaternion.CreateFromYawPitchRoll(0.233f, 0.747f, 0.840f),
    };
    var inverseKinematicsLeftLeg = new InverseKinematicsLimb
    {
      Proximal = Quaternion.CreateFromYawPitchRoll(0.151f, 0.732f, 0.142f),
      Intermediate = Quaternion.CreateFromYawPitchRoll(0.098f, 0.405f, 0.025f),
      Distal = Quaternion.CreateFromYawPitchRoll(0.909f, 0.323f, 0.117f),
    };
    var inverseKinematicsRightLeg = new InverseKinematicsLimb
    {
      Proximal = Quaternion.CreateFromYawPitchRoll(0.525f, 0.195f, 0.934f),
      Intermediate = Quaternion.CreateFromYawPitchRoll(0.348f, 0.857f, 0.845f),
      Distal = Quaternion.CreateFromYawPitchRoll(0.353f, 0.582f, 0.231f),
    };
    limbInverseKinematicsCalculator.Setup(x => x.Calculate(It.IsAny<Quaternion>(), keyframeLeftArm, It.IsAny<BindPoseLimb>(), It.IsAny<float>())).Returns(inverseKinematicsLeftArm);
    limbInverseKinematicsCalculator.Setup(x => x.Calculate(It.IsAny<Quaternion>(), keyframeRightArm, It.IsAny<BindPoseLimb>(), It.IsAny<float>())).Returns(inverseKinematicsRightArm);
    limbInverseKinematicsCalculator.Setup(x => x.Calculate(It.IsAny<Quaternion>(), keyframeLeftLeg, It.IsAny<BindPoseLimb>(), It.IsAny<float>())).Returns(inverseKinematicsLeftLeg);
    limbInverseKinematicsCalculator.Setup(x => x.Calculate(It.IsAny<Quaternion>(), keyframeRightLeg, It.IsAny<BindPoseLimb>(), It.IsAny<float>())).Returns(inverseKinematicsRightLeg);

    var actual = inverseKinematicsCalculator.Calculate(keyframe, bindPose);

    Assert.AreEqual(23.560799, actual.HipsY, 0.0001);
    Assert.AreEqual(new Quaternion(-0.052647516f, -0.9274902f, -0.36805812f, 0.03903252f), actual.HipsRotation);
    Assert.AreEqual(new Quaternion(-0.219598f, -0.968671f, -0.052487105f, -0.103431895f), actual.ShoulderBone);
    Assert.AreEqual(inverseKinematicsLeftArm, actual.LeftArm);
    Assert.AreEqual(inverseKinematicsRightArm, actual.RightArm);
    Assert.AreEqual(inverseKinematicsLeftLeg, actual.LeftLeg);
    Assert.AreEqual(inverseKinematicsRightLeg, actual.RightLeg);
    limbInverseKinematicsCalculator.Verify(x => x.Calculate(facingRotation, keyframeLeftArm, bindPoseLeftArm, 0), Times.Once());
    limbInverseKinematicsCalculator.Verify(x => x.Calculate(facingRotation, keyframeRightArm, bindPoseRightArm, 3.14159265359f), Times.Once());
    limbInverseKinematicsCalculator.Verify(x => x.Calculate(facingRotation, keyframeLeftLeg, bindPoseLeftLeg, 1.57079632679f), Times.Once());
    limbInverseKinematicsCalculator.Verify(x => x.Calculate(facingRotation, keyframeRightLeg, bindPoseRightLeg, 1.57079632679f), Times.Once());
    limbInverseKinematicsCalculator.VerifyNoOtherCalls();
  }
}
