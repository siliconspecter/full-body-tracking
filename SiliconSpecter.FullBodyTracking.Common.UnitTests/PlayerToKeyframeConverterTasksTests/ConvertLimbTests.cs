using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common.UnitTests.PlayerToKeyframeConverterTasksTests;

[TestClass]
public sealed class ConvertLimbTests
{
  [TestMethod]
  public void ReturnsPreviousKeyframeWithoutExtension()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f)
    };
    var previousKeyframe = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.434f, 0.924f, 0.168f),
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var fallbackBendNormal = new Vector3(0.760f, 0.245f, 0.742f);
    var lockedWhenIntermediateDistanceLessThan = 0.025f;
    var length = 35.3f;
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();

    var actual = playerToKeyframeConverterTasks.ConvertLimb(limb, previousKeyframe, fallbackBendNormal, lockedWhenIntermediateDistanceLessThan, length, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(0.434f, 0.924f, 0.168f), actual.Extension);
    Assert.AreEqual(new Vector3(0.760f, 0.245f, 0.742f), actual.BendNormal);
    Assert.AreEqual(new Vector3(0.663f, 0.611f, 0.169f), actual.TipNormal);
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
      Extension = new Vector3(0.434f, 0.924f, 0.168f),
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var fallbackBendNormal = new Vector3(0.760f, 0.245f, 0.742f);
    var lockedWhenIntermediateDistanceLessThan = 0.025f;
    var length = 35.3f;
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();

    var actual = playerToKeyframeConverterTasks.ConvertLimb(limb, previousKeyframe, fallbackBendNormal, lockedWhenIntermediateDistanceLessThan, length, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(0.434f, 0.924f, 0.168f), actual.Extension);
    Assert.AreEqual(new Vector3(0.760f, 0.245f, 0.742f), actual.BendNormal);
    Assert.AreEqual(new Vector3(0.41949385f, 0.893116f, 0.16238472f), actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesExtensionNormalAndProportion()
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
      Extension = new Vector3(0.434f, 0.924f, 0.168f),
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var fallbackBendNormal = new Vector3(0.760f, 0.245f, 0.742f);
    var lockedWhenIntermediateDistanceLessThan = 0.025f;
    var length = 35.3f;
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();

    var actual = playerToKeyframeConverterTasks.ConvertLimb(limb, previousKeyframe, fallbackBendNormal, lockedWhenIntermediateDistanceLessThan, length, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.39361814f, 0.08524121f, 0.028986134f), actual.Extension);
    Assert.AreEqual(new Vector3(0.760f, 0.245f, 0.742f), actual.BendNormal);
    Assert.AreEqual(new Vector3(-0.9748236f, 0.21110599f, 0.07178624f), actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesTipNormal()
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
      Extension = new Vector3(0.434f, 0.924f, 0.168f),
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var fallbackBendNormal = new Vector3(0.760f, 0.245f, 0.742f);
    var lockedWhenIntermediateDistanceLessThan = 0.025f;
    var length = 35.3f;
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();

    var actual = playerToKeyframeConverterTasks.ConvertLimb(limb, previousKeyframe, fallbackBendNormal, lockedWhenIntermediateDistanceLessThan, length, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(0.434f, 0.924f, 0.168f), actual.Extension);
    Assert.AreEqual(new Vector3(0.760f, 0.245f, 0.742f), actual.BendNormal);
    Assert.AreEqual(new Vector3(-0.95780224f, 0.27534986f, 0.08244616f), actual.TipNormal);
  }

  [TestMethod]
  public void GeneratesTipNormalFromIntermediateAndDistal()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        IntermediatePosition = new Vector3(0.043f, 0.499f, 0.688f),
        DistalPosition = new Vector3(0.014f, 0.579f, 0.844f),
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.434f, 0.924f, 0.168f),
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var fallbackBendNormal = new Vector3(0.760f, 0.245f, 0.742f);
    var lockedWhenIntermediateDistanceLessThan = 0.025f;
    var length = 35.3f;
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();

    var actual = playerToKeyframeConverterTasks.ConvertLimb(limb, previousKeyframe, fallbackBendNormal, lockedWhenIntermediateDistanceLessThan, length, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.39361814f, 0.08524121f, 0.028986134f), actual.Extension);
    Assert.AreEqual(new Vector3(0.044844657f, 0.50101984f, -0.8642732f), actual.BendNormal);
    Assert.AreEqual(new Vector3(-0.24291807f, -0.44736698f, 0.86072856f), actual.TipNormal);
  }

  [TestMethod]
  public void GeneratesTipNormalFromExtensionWhenIntermediateAndDistalOverlap()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        IntermediatePosition = new Vector3(0.014f, 0.579f, 0.844f),
        DistalPosition = new Vector3(0.014f, 0.579f, 0.844f),
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.434f, 0.924f, 0.168f),
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var fallbackBendNormal = new Vector3(0.760f, 0.245f, 0.742f);
    var lockedWhenIntermediateDistanceLessThan = 0.025f;
    var length = 35.3f;
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();

    var actual = playerToKeyframeConverterTasks.ConvertLimb(limb, previousKeyframe, fallbackBendNormal, lockedWhenIntermediateDistanceLessThan, length, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.39361814f, 0.08524121f, 0.028986134f), actual.Extension);
    Assert.AreEqual(new Vector3(0.760f, 0.245f, 0.742f), actual.BendNormal);
    Assert.AreEqual(new Vector3(-0.9748236f, 0.21110599f, 0.07178624f), actual.TipNormal);
  }

  [TestMethod]
  public void GeneratesTipNormalFromExtensionWhenIntermediateUnavailable()
  {
    var limb = new Limb
    {
      ProximalPosition = new Vector3(10.3f, -8.7f, 4.2f),
      Extension = new LimbExtension
      {
        DistalPosition = new Vector3(0.014f, 0.579f, 0.844f),
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.434f, 0.924f, 0.168f),
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var fallbackBendNormal = new Vector3(0.760f, 0.245f, 0.742f);
    var lockedWhenIntermediateDistanceLessThan = 0.025f;
    var length = 35.3f;
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();

    var actual = playerToKeyframeConverterTasks.ConvertLimb(limb, previousKeyframe, fallbackBendNormal, lockedWhenIntermediateDistanceLessThan, length, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.39361814f, 0.08524121f, 0.028986134f), actual.Extension);
    Assert.AreEqual(new Vector3(0.760f, 0.245f, 0.742f), actual.BendNormal);
    Assert.AreEqual(new Vector3(-0.9748236f, 0.21110599f, 0.07178624f), actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesExtensionNormalAndProportionWhenLockedStraight()
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
      Extension = new Vector3(0.434f, 0.924f, 0.168f),
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var fallbackBendNormal = new Vector3(0.760f, 0.245f, 0.742f);
    var lockedWhenIntermediateDistanceLessThan = 0.175f;
    var length = 35.3f;
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();

    var actual = playerToKeyframeConverterTasks.ConvertLimb(limb, previousKeyframe, fallbackBendNormal, lockedWhenIntermediateDistanceLessThan, length, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.39361814f, 0.08524121f, 0.028986134f), actual.Extension);
    Assert.AreEqual(fallbackBendNormal, actual.BendNormal);
    Assert.AreEqual(new Vector3(-0.24291807f, -0.44736698f, 0.86072856f), actual.TipNormal);
  }

  [TestMethod]
  public void CalculatesExtensionNormalProportionAndBendNormal()
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
      Extension = new Vector3(0.434f, 0.924f, 0.168f),
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var fallbackBendNormal = new Vector3(0.760f, 0.245f, 0.742f);
    var lockedWhenIntermediateDistanceLessThan = 0.1725f;
    var length = 35.3f;
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();

    var actual = playerToKeyframeConverterTasks.ConvertLimb(limb, previousKeyframe, fallbackBendNormal, lockedWhenIntermediateDistanceLessThan, length, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.39361814f, 0.08524121f, 0.028986134f), actual.Extension);
    Assert.AreEqual(new Vector3(0.044844657f, 0.50101984f, -0.8642732f), actual.BendNormal);
    Assert.AreEqual(new Vector3(-0.24291807f, -0.44736698f, 0.86072856f), actual.TipNormal);
  }

  [TestMethod]
  public void CapsExtensionProportionToOne()
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
      Extension = new Vector3(0.434f, 0.924f, 0.168f),
      BendNormal = new Vector3(0.946f, 0.938f, 0.318f),
      TipNormal = new Vector3(0.663f, 0.611f, 0.169f),
    };
    var fallbackBendNormal = new Vector3(0.760f, 0.245f, 0.742f);
    var lockedWhenIntermediateDistanceLessThan = 0.025f;
    var length = 10.3f;
    var cameraToInverseFacingRotation = Quaternion.CreateFromYawPitchRoll(0.198f, 0.791f, 0.670f);
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();

    var actual = playerToKeyframeConverterTasks.ConvertLimb(limb, previousKeyframe, fallbackBendNormal, lockedWhenIntermediateDistanceLessThan, length, cameraToInverseFacingRotation);

    Assert.AreEqual(new Vector3(-0.97482353f, 0.21110599f, 0.07178624f), actual.Extension);
    Assert.AreEqual(new Vector3(0.760f, 0.245f, 0.742f), actual.BendNormal);
    Assert.AreEqual(new Vector3(-0.9748236f, 0.211106f, 0.07178625f), actual.TipNormal);
  }
}
