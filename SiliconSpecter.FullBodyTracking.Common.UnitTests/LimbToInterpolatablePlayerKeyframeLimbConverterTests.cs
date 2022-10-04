using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common.UnitTests;

[TestClass]
public sealed class LimbToInterpolatablePlayerKeyframeLimbConverterTests
{
  [TestMethod]
  public void ReturnsDefaultWithoutExtensionOrPreviousKeyframe()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f)
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, null, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(0, -1, 0), actual.ExtensionNormal);
    Assert.AreEqual(1, actual.ExtensionProportion);
    Assert.AreEqual(defaultBendNormal, actual.BendNormal);
    Assert.AreEqual(defaultTipNormal, actual.TipNormal);
  }

  [TestMethod]
  public void ReturnsPreviousKeyframeWithoutExtension()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f)
    };
    var previousKeyframe = new InterpolatablePlayerKeyframeLimb
    {
      ExtensionNormal = new Vector3(0.434f, 0.924f, 0.168f),
      ExtensionProportion = 0.24f,
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, previousKeyframe, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(0.434f, 0.924f, 0.168f), actual.ExtensionNormal);
    Assert.AreEqual(0.24f, actual.ExtensionProportion);
    Assert.AreEqual(new Vector3(0.946f, 0.938f, 0.318f), actual.BendNormal);
    Assert.AreEqual(new Vector3(0.663f, 0.611f, 0.169f), actual.TipNormal);
  }

  [TestMethod]
  public void ReturnsDefaultWhenAllPointsOverlap()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        DistalPosition = new Vector3(10.3f, -8.7f, 4.2f),
        TipPosition = new Vector3(10.3f, -8.7f, 4.2f),
      }
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, null, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(0, -1, 0), actual.ExtensionNormal);
    Assert.AreEqual(1, actual.ExtensionProportion);
    Assert.AreEqual(defaultBendNormal, actual.BendNormal);
    Assert.AreEqual(defaultTipNormal, actual.TipNormal);
  }

  [TestMethod]
  public void ReturnsPreviousKeyframeWhenAllPointsOverlap()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        DistalPosition = new Vector3(10.3f, -8.7f, 4.2f),
        TipPosition = new Vector3(10.3f, -8.7f, 4.2f),
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframeLimb
    {
      ExtensionNormal = new Vector3(0.434f, 0.924f, 0.168f),
      ExtensionProportion = 0.24f,
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, previousKeyframe, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(0.434f, 0.924f, 0.168f), actual.ExtensionNormal);
    Assert.AreEqual(0.24f, actual.ExtensionProportion);
    Assert.AreEqual(new Vector3(0.946f, 0.938f, 0.318f), actual.BendNormal);
    Assert.AreEqual(new Vector3(0.663f, 0.611f, 0.169f), actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesExtensionNormalAndProportionWithoutPreviousKeyframe()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        DistalPosition = new Vector3(0.014f, 0.579f, 0.844f),
        TipPosition = new Vector3(0.014f, 0.579f, 0.844f),
      }
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, null, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.97482353f, 0.21110599f, 0.07178624f), actual.ExtensionNormal);
    Assert.AreEqual(0.40378398f, actual.ExtensionProportion);
    Assert.AreEqual(defaultBendNormal, actual.BendNormal);
    Assert.AreEqual(defaultTipNormal, actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesExtensionNormalAndProportionWithPreviousKeyframe()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        DistalPosition = new Vector3(0.014f, 0.579f, 0.844f),
        TipPosition = new Vector3(0.014f, 0.579f, 0.844f),
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframeLimb
    {
      ExtensionNormal = new Vector3(0.434f, 0.924f, 0.168f),
      ExtensionProportion = 0.24f,
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, previousKeyframe, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.97482353f, 0.21110599f, 0.07178624f), actual.ExtensionNormal);
    Assert.AreEqual(0.40378398f, actual.ExtensionProportion);
    Assert.AreEqual(new Vector3(0.946f, 0.938f, 0.318f), actual.BendNormal);
    Assert.AreEqual(new Vector3(0.663f, 0.611f, 0.169f), actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesTipNormalWithoutPreviousKeyframe()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        DistalPosition = new Vector3(10.3f, -8.7f, 4.2f),
        TipPosition = new Vector3(0.728f, 0.999f, 0.357f),
      }
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, null, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(0, -1, 0), actual.ExtensionNormal);
    Assert.AreEqual(1, actual.ExtensionProportion);
    Assert.AreEqual(defaultBendNormal, actual.BendNormal);
    Assert.AreEqual(new Vector3(-0.95780224f, 0.27534986f, 0.08244616f), actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesTipNormalWithPreviousKeyframe()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        DistalPosition = new Vector3(10.3f, -8.7f, 4.2f),
        TipPosition = new Vector3(0.728f, 0.999f, 0.357f),
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframeLimb
    {
      ExtensionNormal = new Vector3(0.434f, 0.924f, 0.168f),
      ExtensionProportion = 0.24f,
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, previousKeyframe, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(0.434f, 0.924f, 0.168f), actual.ExtensionNormal);
    Assert.AreEqual(0.24f, actual.ExtensionProportion);
    Assert.AreEqual(new Vector3(0.946f, 0.938f, 0.318f), actual.BendNormal);
    Assert.AreEqual(new Vector3(-0.95780224f, 0.27534986f, 0.08244616f), actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesExtensionNormalAndProportionWhenLockedStraightWithoutPreviousKeyframe()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        IntermediatePosition = new Vector3(3.0998f, -2.2047f, 1.8508f),
        DistalPosition = new Vector3(0.014f, 0.579f, 0.844f),
        TipPosition = new Vector3(0.014f, 0.579f, 0.844f),
      }
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, null, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.97482353f, 0.21110599f, 0.07178624f), actual.ExtensionNormal);
    Assert.AreEqual(0.40378398f, actual.ExtensionProportion);
    Assert.AreEqual(defaultBendNormal, actual.BendNormal);
    Assert.AreEqual(defaultTipNormal, actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesExtensionNormalAndProportionWhenLockedStraightWithPreviousKeyframe()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        IntermediatePosition = new Vector3(3.0998f, -2.2047f, 1.8508f),
        DistalPosition = new Vector3(0.014f, 0.579f, 0.844f),
        TipPosition = new Vector3(0.014f, 0.579f, 0.844f),
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframeLimb
    {
      ExtensionNormal = new Vector3(0.434f, 0.924f, 0.168f),
      ExtensionProportion = 0.24f,
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, previousKeyframe, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.97482353f, 0.21110599f, 0.07178624f), actual.ExtensionNormal);
    Assert.AreEqual(0.40378398f, actual.ExtensionProportion);
    Assert.AreEqual(new Vector3(0.946f, 0.938f, 0.318f), actual.BendNormal);
    Assert.AreEqual(new Vector3(0.663f, 0.611f, 0.169f), actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesExtensionNormalProportionAndBendNormalWithoutPreviousKeyframe()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        IntermediatePosition = new Vector3(0.043f, 0.499f, 0.688f),
        DistalPosition = new Vector3(0.014f, 0.579f, 0.844f),
        TipPosition = new Vector3(0.014f, 0.579f, 0.844f),
      }
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, null, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.97482353f, 0.21110599f, 0.07178624f), actual.ExtensionNormal);
    Assert.AreEqual(0.40378398f, actual.ExtensionProportion);
    Assert.AreEqual(new Vector3(0.044844657f, 0.50101984f, -0.8642732f), actual.BendNormal);
    Assert.AreEqual(defaultTipNormal, actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesExtensionNormalProportionAndBendNormalWithPreviousKeyframe()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        IntermediatePosition = new Vector3(0.043f, 0.499f, 0.688f),
        DistalPosition = new Vector3(0.014f, 0.579f, 0.844f),
        TipPosition = new Vector3(0.014f, 0.579f, 0.844f),
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframeLimb
    {
      ExtensionNormal = new Vector3(0.434f, 0.924f, 0.168f),
      ExtensionProportion = 0.24f,
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var length = 35.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, previousKeyframe, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.97482353f, 0.21110599f, 0.07178624f), actual.ExtensionNormal);
    Assert.AreEqual(0.40378398f, actual.ExtensionProportion);
    Assert.AreEqual(new Vector3(0.044844657f, 0.50101984f, -0.8642732f), actual.BendNormal);
    Assert.AreEqual(new Vector3(0.663f, 0.611f, 0.169f), actual.TipNormal);
  }

  [TestMethod]
  public void CapsExtensionProportionToOneWithoutPreviousKeyframe()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        DistalPosition = new Vector3(0.014f, 0.579f, 0.844f),
        TipPosition = new Vector3(0.014f, 0.579f, 0.844f),
      }
    };
    var length = 10.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, null, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.97482353f, 0.21110599f, 0.07178624f), actual.ExtensionNormal);
    Assert.AreEqual(1, actual.ExtensionProportion);
    Assert.AreEqual(defaultBendNormal, actual.BendNormal);
    Assert.AreEqual(defaultTipNormal, actual.TipNormal);
  }

  [TestMethod]
  public void CapsExtensionProportionToOneWithPreviousKeyframe()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        DistalPosition = new Vector3(0.014f, 0.579f, 0.844f),
        TipPosition = new Vector3(0.014f, 0.579f, 0.844f),
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframeLimb
    {
      ExtensionNormal = new Vector3(0.434f, 0.924f, 0.168f),
      ExtensionProportion = 0.24f,
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var length = 10.3f;
    var defaultBendNormal = new Vector3(0.777f, 0.817f, 0.547f);
    var defaultTipNormal = new Vector3(0.028f, 0.424f, 0.618f);
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var limbToInterpolatablePlayerKeyframeLimbConverter = new LimbToInterpolatablePlayerKeyframeLimbConverter();

    var actual = limbToInterpolatablePlayerKeyframeLimbConverter.Convert(limb, previousKeyframe, length, defaultBendNormal, defaultTipNormal, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.97482353f, 0.21110599f, 0.07178624f), actual.ExtensionNormal);
    Assert.AreEqual(1, actual.ExtensionProportion);
    Assert.AreEqual(new Vector3(0.946f, 0.938f, 0.318f), actual.BendNormal);
    Assert.AreEqual(new Vector3(0.663f, 0.611f, 0.169f), actual.TipNormal);
  }
}
