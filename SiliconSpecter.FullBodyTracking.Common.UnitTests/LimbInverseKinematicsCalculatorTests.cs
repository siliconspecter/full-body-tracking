using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common.UnitTests;

[TestClass]
public sealed class LimbInverseKinematicsCalculatorTests
{
  [TestMethod]
  public void CalculatesInverseKinematicsForLimb()
  {
    var limbInverseKinematicsCalculator = new LimbInverseKinematicsCalculator();
    var facingRotation = Quaternion.CreateFromYawPitchRoll(-0.518f, 0.352f, -0.076f);
    var keyframeLimb = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.724f, -0.407f, 0.360f),
      BendNormal = new Vector3(-0.788f, 0.219f, 0.685f),
      TipNormal = new Vector3(-0.462f, 0.425f, -0.356f),
    };
    var bindPoseLimb = new BindPoseLimb
    {
      ProximalPosition = new Vector3(-0.104f, 0.164f, -0.633f),
      IntermediatePosition = new Vector3(0.682f, 0.372f, 0.222f),
      DistalPosition = new Vector3(0.222f, 0.947f, 0.379f),
    };
    var roll = 0.918f;

    var actual = limbInverseKinematicsCalculator.Calculate(facingRotation, keyframeLimb, bindPoseLimb, roll);

    Assert.AreEqual(new Quaternion(0.36763567f, -0.056287967f, 0.3616698f, 0.8549097f), actual.Proximal);
    Assert.AreEqual(new Quaternion(0.52803004f, 0.291799f, 0.44128388f, 0.6643087f), actual.Intermediate);
    Assert.AreEqual(new Quaternion(0.4066949f, 0.76647776f, 0.28368992f, -0.40820476f), actual.Distal);
  }

  [TestMethod]
  public void CalculatesInverseKinematicsForLimbWhenBendNormalInvalid()
  {
    var limbInverseKinematicsCalculator = new LimbInverseKinematicsCalculator();
    var facingRotation = Quaternion.CreateFromYawPitchRoll(-0.518f, 0.352f, -0.076f);
    var keyframeLimb = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.724f, -0.407f, 0.360f),
      BendNormal = new Vector3(0.724f, -0.407f, 0.360f),
      TipNormal = new Vector3(-0.462f, 0.425f, -0.356f),
    };
    var bindPoseLimb = new BindPoseLimb
    {
      ProximalPosition = new Vector3(-0.104f, 0.164f, -0.633f),
      IntermediatePosition = new Vector3(0.682f, 0.372f, 0.222f),
      DistalPosition = new Vector3(0.222f, 0.947f, 0.379f),
    };
    var roll = 0.918f;

    var actual = limbInverseKinematicsCalculator.Calculate(facingRotation, keyframeLimb, bindPoseLimb, roll);

    Assert.AreEqual(new Quaternion(0.31247327f, 0.034607083f, 0.24305305f, 0.9176535f), actual.Proximal);
    Assert.AreEqual(new Quaternion(0.5298928f, 0.3502329f, 0.28898805f, 0.71626556f), actual.Intermediate);
    Assert.AreEqual(new Quaternion(0.503053f, 0.7069841f, 0.22811146f, -0.44167435f), actual.Distal);
  }

  [TestMethod]
  public void CalculatesInverseKinematicsForLimbWhenUnextended()
  {
    var limbInverseKinematicsCalculator = new LimbInverseKinematicsCalculator();
    var facingRotation = Quaternion.CreateFromYawPitchRoll(-0.518f, 0.352f, -0.076f);
    var keyframeLimb = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0, 0, 0),
      BendNormal = new Vector3(-0.788f, 0.219f, 0.685f),
      TipNormal = new Vector3(-0.462f, 0.425f, -0.356f),
    };
    var bindPoseLimb = new BindPoseLimb
    {
      ProximalPosition = new Vector3(-0.104f, 0.164f, -0.633f),
      IntermediatePosition = new Vector3(0.682f, 0.372f, 0.222f),
      DistalPosition = new Vector3(0.222f, 0.947f, 0.379f),
    };
    var roll = 0.918f;

    var actual = limbInverseKinematicsCalculator.Calculate(facingRotation, keyframeLimb, bindPoseLimb, roll);

    Assert.AreEqual(new Quaternion(-0.31328493f, -0.6339184f, 0.31328493f, 0.6339184f), actual.Proximal);
    Assert.AreEqual(new Quaternion(0.31328493f, 0.6339184f, 0.31328493f, 0.6339184f), actual.Intermediate);
    Assert.AreEqual(new Quaternion(0.503053f, 0.7069841f, 0.22811146f, -0.44167435f), actual.Distal);
  }

  [TestMethod]
  public void CalculatesInverseKinematicsForLimbWhenOverextended()
  {
    var limbInverseKinematicsCalculator = new LimbInverseKinematicsCalculator();
    var facingRotation = Quaternion.CreateFromYawPitchRoll(-0.518f, 0.352f, -0.076f);
    var keyframeLimb = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.724f, -0.407f, 1.360f),
      BendNormal = new Vector3(-0.788f, 0.219f, 0.685f),
      TipNormal = new Vector3(-0.462f, 0.425f, -0.356f),
    };
    var bindPoseLimb = new BindPoseLimb
    {
      ProximalPosition = new Vector3(-0.104f, 0.164f, -0.633f),
      IntermediatePosition = new Vector3(0.682f, 0.372f, 0.222f),
      DistalPosition = new Vector3(0.222f, 0.947f, 0.379f),
    };
    var roll = 0.918f;

    var actual = limbInverseKinematicsCalculator.Calculate(facingRotation, keyframeLimb, bindPoseLimb, roll);

    Assert.AreEqual(new Quaternion(0.28043732f, -0.09645752f, 0.35713637f, 0.8857226f), actual.Proximal);
    Assert.AreEqual(new Quaternion(0.28043732f, -0.09645752f, 0.35713637f, 0.8857226f), actual.Intermediate);
    Assert.AreEqual(new Quaternion(0.41990304f, 0.75932235f, 0.27658063f, -0.4130547f), actual.Distal);
  }

  [TestMethod]
  public void CalculatesInverseKinematicsForLimbWhenOverextendedUp()
  {
    var limbInverseKinematicsCalculator = new LimbInverseKinematicsCalculator();
    var facingRotation = Quaternion.CreateFromYawPitchRoll(-0.518f, 0.352f, -0.076f);
    var keyframeLimb = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0, 10, 0),
      BendNormal = new Vector3(-0.788f, 0.219f, 0.685f),
      TipNormal = new Vector3(-0.462f, 0.425f, -0.356f),
    };
    var bindPoseLimb = new BindPoseLimb
    {
      ProximalPosition = new Vector3(-0.104f, 0.164f, -0.633f),
      IntermediatePosition = new Vector3(0.682f, 0.372f, 0.222f),
      DistalPosition = new Vector3(0.222f, 0.947f, 0.379f),
    };
    var roll = 0.918f;

    var actual = limbInverseKinematicsCalculator.Calculate(facingRotation, keyframeLimb, bindPoseLimb, roll);

    Assert.AreEqual(new Quaternion(-0.5056984f, 0.27591962f, 0.46853054f, 0.66978854f), actual.Proximal);
    Assert.AreEqual(new Quaternion(-0.5056984f, 0.27591962f, 0.46853054f, 0.66978854f), actual.Intermediate);
    Assert.AreEqual(new Quaternion(0.83607394f, -0.2320977f, -0.33753705f, -0.3649381f), actual.Distal);
  }

  [TestMethod]
  public void CalculatesInverseKinematicsForLimbWhenOverextendedDown()
  {
    var limbInverseKinematicsCalculator = new LimbInverseKinematicsCalculator();
    var facingRotation = Quaternion.CreateFromYawPitchRoll(-0.518f, 0.352f, -0.076f);
    var keyframeLimb = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0, -10, 0),
      BendNormal = new Vector3(-0.788f, 0.219f, 0.685f),
      TipNormal = new Vector3(-0.462f, 0.425f, -0.356f),
    };
    var bindPoseLimb = new BindPoseLimb
    {
      ProximalPosition = new Vector3(-0.104f, 0.164f, -0.633f),
      IntermediatePosition = new Vector3(0.682f, 0.372f, 0.222f),
      DistalPosition = new Vector3(0.222f, 0.947f, 0.379f),
    };
    var roll = 0.918f;

    var actual = limbInverseKinematicsCalculator.Calculate(facingRotation, keyframeLimb, bindPoseLimb, roll);

    Assert.AreEqual(new Quaternion(0.7790316f, -0.247482f, 0.2341245f, 0.52635366f), actual.Proximal);
    Assert.AreEqual(new Quaternion(0.7790316f, -0.247482f, 0.2341245f, 0.52635366f), actual.Intermediate);
    Assert.AreEqual(new Quaternion(0.2320977f, 0.83607394f, 0.3649381f, -0.33753705f), actual.Distal);
  }
}
