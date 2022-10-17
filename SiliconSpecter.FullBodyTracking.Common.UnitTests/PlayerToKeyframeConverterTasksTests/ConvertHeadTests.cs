using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common.UnitTests.PlayerToKeyframeConverterTasksTests;

[TestClass]
public sealed class ConvertHeadTests
{
  [TestMethod]
  public void ReturnsPreviousRotationWithoutForwardOrUpNormals()
  {
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();
    var headRotation = Quaternion.CreateFromYawPitchRoll(0.481f, 0.873f, 0.822f);
    var facingRotation = Quaternion.CreateFromYawPitchRoll(0.429f, 0.133f, 0.553f);
    var cameraRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);
    var inverseFacingRotation = Quaternion.Inverse(facingRotation);
    var cameraToInverseFacingRotation = inverseFacingRotation * cameraRotation;

    var actual = playerToKeyframeConverterTasks.ConvertHead(facingRotation, inverseFacingRotation, headRotation, null, null, cameraRotation, cameraToInverseFacingRotation);

    Assert.AreEqual(headRotation, actual);
  }

  [TestMethod]
  public void ReprojectsPreviousRotationWithOnlyAnUpNormal()
  {
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();
    var headRotation = Quaternion.CreateFromYawPitchRoll(0.481f, 0.873f, 0.822f);
    var facingRotation = Quaternion.CreateFromYawPitchRoll(0.429f, 0.133f, 0.553f);
    var cameraRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);
    var inverseFacingRotation = Quaternion.Inverse(facingRotation);
    var cameraToInverseFacingRotation = inverseFacingRotation * cameraRotation;
    var headUpNormal = new Vector3(-0.629f, -0.706f, 0.229f);

    var actual = playerToKeyframeConverterTasks.ConvertHead(facingRotation, inverseFacingRotation, headRotation, null, headUpNormal, cameraRotation, cameraToInverseFacingRotation);

    Assert.AreEqual(new Quaternion(-0.39849043f, 0.42331213f, -0.67920536f, 0.44798678f), actual);
  }

  [TestMethod]
  public void ReprojectsPreviousRotationWithOnlyAForwardNormal()
  {
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();
    var headRotation = Quaternion.CreateFromYawPitchRoll(0.481f, 0.873f, 0.822f);
    var facingRotation = Quaternion.CreateFromYawPitchRoll(0.429f, 0.133f, 0.553f);
    var cameraRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);
    var inverseFacingRotation = Quaternion.Inverse(facingRotation);
    var cameraToInverseFacingRotation = inverseFacingRotation * cameraRotation;
    var headForwardNormal = new Vector3(-0.629f, -0.706f, 0.229f);

    var actual = playerToKeyframeConverterTasks.ConvertHead(facingRotation, inverseFacingRotation, headRotation, headForwardNormal, null, cameraRotation, cameraToInverseFacingRotation);

    Assert.AreEqual(new Quaternion(0.62131906f, 0.4060095f, 0.2305946f, 0.62924147f), actual);
  }

  [TestMethod]
  public void ReplacesPreviousRotationWithBothUpAndForwardNormals()
  {
    var playerToKeyframeConverterTasks = new PlayerToKeyframeConverterTasks();
    var headRotation = Quaternion.CreateFromYawPitchRoll(0.481f, 0.873f, 0.822f);
    var facingRotation = Quaternion.CreateFromYawPitchRoll(0.429f, 0.133f, 0.553f);
    var cameraRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);
    var inverseFacingRotation = Quaternion.Inverse(facingRotation);
    var cameraToInverseFacingRotation = inverseFacingRotation * cameraRotation;
    var headForwardNormal = new Vector3(0.669f, 0.272f, 0.785f);
    var headUpNormal = new Vector3(-0.629f, -0.706f, 0.229f);

    var actual = playerToKeyframeConverterTasks.ConvertHead(facingRotation, inverseFacingRotation, headRotation, headForwardNormal, headUpNormal, cameraRotation, cameraToInverseFacingRotation);

    Assert.AreEqual(new Quaternion(0.546018f, -0.31224847f, -0.7296111f, 0.26838872f), actual);
  }
}
