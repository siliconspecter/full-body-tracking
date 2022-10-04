using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SiliconSpecter.FullBodyTracking.Common.UnitTests;

[TestClass]
public sealed class PlayerToMetricsConverterTests
{
  [TestMethod]
  public void MeasuresLimbsWithoutAPreviousSetOfMetricsOrDetails()
  {
    var limbMeasurer = new Mock<ILimbMeasurer>();
    var playerToMetricsConverter = new PlayerToMetricsConverter(limbMeasurer.Object);
    var player = new Player
    {
      ApproximatePosition = new Vector3(7.4f, -2.1f, 8.8f),
    };

    var actual = playerToMetricsConverter.Convert(player, null);

    Assert.AreEqual(0.635f, actual.LeftArmLength);
    Assert.AreEqual(0.635f, actual.RightArmLength);
    Assert.AreEqual(0.8125f, actual.LeftLegLength);
    Assert.AreEqual(0.8125f, actual.RightLegLength);
    limbMeasurer.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void MeasuresLimbsWithDetailsWithoutAPreviousSetOfMetrics()
  {
    var limbMeasurer = new Mock<ILimbMeasurer>();
    var playerToMetricsConverter = new PlayerToMetricsConverter(limbMeasurer.Object);
    var leftArm = new Limb
    {
      ProximalPosition = new Vector3(1.0f, 0.7f, 0.9f),
    };
    var rightArm = new Limb
    {
      ProximalPosition = new Vector3(0.9f, 0.5f, 0.2f),
    };
    var leftLeg = new Limb
    {
      ProximalPosition = new Vector3(0.8f, 0.8f, 0.1f),
    };
    var rightLeg = new Limb
    {
      ProximalPosition = new Vector3(0.8f, 0.9f, 0.8f),
    };
    var player = new Player
    {
      ApproximatePosition = new Vector3(7.4f, -2.1f, 8.8f),
      Details = new PlayerDetails
      {
        FacialAnimation = new FacialAnimation(),
        LeftArm = leftArm,
        RightArm = rightArm,
        LeftLeg = leftLeg,
        RightLeg = rightLeg,
      },
    };
    limbMeasurer.Setup(x => x.Measure(leftArm, 0.635f)).Returns(2.41f);
    limbMeasurer.Setup(x => x.Measure(rightArm, 0.635f)).Returns(-3.5f);
    limbMeasurer.Setup(x => x.Measure(leftLeg, 0.8125f)).Returns(21.5f);
    limbMeasurer.Setup(x => x.Measure(rightLeg, 0.8125f)).Returns(74.9f);

    var actual = playerToMetricsConverter.Convert(player, null);

    Assert.AreEqual(2.41f, actual.LeftArmLength);
    Assert.AreEqual(-3.5f, actual.RightArmLength);
    Assert.AreEqual(21.5f, actual.LeftLegLength);
    Assert.AreEqual(74.9f, actual.RightLegLength);
    limbMeasurer.Verify(x => x.Measure(leftArm, 0.635f), Times.Once());
    limbMeasurer.Verify(x => x.Measure(rightArm, 0.635f), Times.Once());
    limbMeasurer.Verify(x => x.Measure(leftLeg, 0.8125f), Times.Once());
    limbMeasurer.Verify(x => x.Measure(rightLeg, 0.8125f), Times.Once());
    limbMeasurer.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void MeasuresLimbsWithoutDetailsWithAPreviousSetOfMetrics()
  {
    var limbMeasurer = new Mock<ILimbMeasurer>();
    var playerToMetricsConverter = new PlayerToMetricsConverter(limbMeasurer.Object);
    var player = new Player
    {
      ApproximatePosition = new Vector3(7.4f, -2.1f, 8.8f),
    };
    var metrics = new Metrics
    {
      LeftArmLength = 12.4f,
      RightArmLength = 33.1f,
      LeftLegLength = 8.7f,
      RightLegLength = 41.82f,
    };

    var actual = playerToMetricsConverter.Convert(player, metrics);

    Assert.AreEqual(12.4f, actual.LeftArmLength);
    Assert.AreEqual(33.1f, actual.RightArmLength);
    Assert.AreEqual(8.7f, actual.LeftLegLength);
    Assert.AreEqual(41.82f, actual.RightLegLength);
    limbMeasurer.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void MeasuresLimbsWithDetailsWithAPreviousSetOfMetrics()
  {
    var limbMeasurer = new Mock<ILimbMeasurer>();
    var playerToMetricsConverter = new PlayerToMetricsConverter(limbMeasurer.Object);
    var leftArm = new Limb
    {
      ProximalPosition = new Vector3(1.0f, 0.7f, 0.9f),
    };
    var rightArm = new Limb
    {
      ProximalPosition = new Vector3(0.9f, 0.5f, 0.2f),
    };
    var leftLeg = new Limb
    {
      ProximalPosition = new Vector3(0.8f, 0.8f, 0.1f),
    };
    var rightLeg = new Limb
    {
      ProximalPosition = new Vector3(0.8f, 0.9f, 0.8f),
    };
    var player = new Player
    {
      ApproximatePosition = new Vector3(7.4f, -2.1f, 8.8f),
      Details = new PlayerDetails
      {
        FacialAnimation = new FacialAnimation(),
        LeftArm = leftArm,
        RightArm = rightArm,
        LeftLeg = leftLeg,
        RightLeg = rightLeg,
      },
    };
    var metrics = new Metrics
    {
      LeftArmLength = 12.4f,
      RightArmLength = 33.1f,
      LeftLegLength = 8.7f,
      RightLegLength = 41.82f,
    };
    limbMeasurer.Setup(x => x.Measure(leftArm, 12.4f)).Returns(2.41f);
    limbMeasurer.Setup(x => x.Measure(rightArm, 33.1f)).Returns(-3.5f);
    limbMeasurer.Setup(x => x.Measure(leftLeg, 8.7f)).Returns(21.5f);
    limbMeasurer.Setup(x => x.Measure(rightLeg, 41.82f)).Returns(74.9f);

    var actual = playerToMetricsConverter.Convert(player, metrics);

    Assert.AreEqual(2.41f, actual.LeftArmLength);
    Assert.AreEqual(-3.5f, actual.RightArmLength);
    Assert.AreEqual(21.5f, actual.LeftLegLength);
    Assert.AreEqual(74.9f, actual.RightLegLength);
    limbMeasurer.Verify(x => x.Measure(leftArm, 12.4f), Times.Once());
    limbMeasurer.Verify(x => x.Measure(rightArm, 33.1f), Times.Once());
    limbMeasurer.Verify(x => x.Measure(leftLeg, 8.7f), Times.Once());
    limbMeasurer.Verify(x => x.Measure(rightLeg, 41.82f), Times.Once());
    limbMeasurer.VerifyNoOtherCalls();
  }
}
