using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SiliconSpecter.FullBodyTracking.Common.UnitTests;

[TestClass]
public sealed class MiscellaneousTests
{
  [TestMethod]
  public void CalculatesLookAtQuaternions()
  {
    var forward = new Vector3(-80.684f, -86.669f, 22.639f);
    var up = new Vector3(-95.412f, -66.808f, -57.308f);

    var actual = Miscellaneous.LookAt(forward, up);

    Assert.AreEqual(-0.14103484153747559, actual.X, 0.00001);
    Assert.AreEqual(-0.6214625835418701, actual.Y, 0.00001);
    Assert.AreEqual(0.6662810444831848, actual.Z, 0.00001);
    Assert.AreEqual(0.3872506022453308, actual.W, 0.00001);
  }
}
