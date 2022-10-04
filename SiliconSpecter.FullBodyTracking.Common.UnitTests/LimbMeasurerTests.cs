using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common.UnitTests;

[TestClass]
public sealed class LimbMeasurerTests
{
  [TestMethod]
  public void ReturnsPreviousLengthWhenNoExtension()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f)
    };
    var previousLength = 11.3f;
    var limbMeasurer = new LimbMeasurer();

    var actual = limbMeasurer.Measure(limb, previousLength);

    Assert.AreEqual(11.3f, actual);
  }

  [TestMethod]
  public void ReturnsPreviousLengthWhenNoIntermediatePosition()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        DistalPosition = new Vector3(-2.4f, -17.4f, 2.1f),
        TipPosition = new Vector3(5.7f, 1.4f, 2.2f),
      },
    };
    var previousLength = 11.3f;
    var limbMeasurer = new LimbMeasurer();

    var actual = limbMeasurer.Measure(limb, previousLength);

    Assert.AreEqual(11.3f, actual);
  }

  [TestMethod]
  public void ReturnsTotalLengthWhenHasIntermediatePosition()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        IntermediatePosition = new Vector3(8.1f, 10.4f, -3.3f),
        DistalPosition = new Vector3(-2.4f, -17.4f, 2.1f),
        TipPosition = new Vector3(5.7f, 1.4f, 2.2f),
      },
    };
    var previousLength = 11.3f;
    var limbMeasurer = new LimbMeasurer();

    var actual = limbMeasurer.Measure(limb, previousLength);

    Assert.AreEqual(50.84082f, actual, 0.0001f);
  }
}
