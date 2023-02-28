using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SiliconSpecter.FullBodyTracking.Common.UnitTests;

[TestClass]
public sealed class PlayerToKeyframeConverterTests
{
  [TestMethod]
  public void ExposesInjectedDependencies()
  {
    var playerToKeyframeConverterTasks = new Mock<IPlayerToKeyframeConverterTasks>();

    var playerToKeyframeConverter = new PlayerToKeyframeConverter<uint>(playerToKeyframeConverterTasks.Object);

    Assert.AreSame(playerToKeyframeConverterTasks.Object, playerToKeyframeConverter.PlayerToKeyframeConverterTasks);
    playerToKeyframeConverterTasks.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ReturnsPreviousKeyframeWithNewPositionWhenPlayerLacksDetails()
  {
    var playerToKeyframeConverterTasks = new Mock<IPlayerToKeyframeConverterTasks>();
    var playerToKeyframeConverter = new PlayerToKeyframeConverter<uint>(playerToKeyframeConverterTasks.Object);
    var player = new Player<uint>
    {
      FrameNumber = 248234,
      ApproximatePosition = new Vector3(-0.025f, -4.43f, 2.844f),
    };
    var previousKeyframe = new InterpolatablePlayerKeyframe
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
      FacingRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f),
      LeftArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.526f, 0.358f, 0.940f),
        BendNormal = new Vector3(0.184f, 0.135f, 0.375f),
        TipNormal = new Vector3(0.630f, 0.312f, 0.635f),
        TipLength = 0.376f,
      },
      RightArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.351f, 0.618f, 0.219f),
        BendNormal = new Vector3(0.256f, 0.994f, 0.228f),
        TipNormal = new Vector3(0.636f, 0.038f, 0.551f),
        TipLength = 0.360f,
      },
      LeftLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.278f, 0.053f, 0.166f),
        BendNormal = new Vector3(0.761f, 0.848f, 0.046f),
        TipNormal = new Vector3(0.802f, 0.624f, 0.401f),
        TipLength = 0.495f,
      },
      RightLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.074f, 0.881f, 0.708f),
        BendNormal = new Vector3(0.114f, 0.940f, 0.551f),
        TipNormal = new Vector3(0.316f, 0.938f, 0.898f),
        TipLength = 0.400f,
      },
    };
    var metrics = new Metrics
    {
      LeftArmLength = 12.4f,
      RightArmLength = 33.1f,
      LeftLegLength = 8.7f,
      RightLegLength = 41.82f,
    };
    var cameraPosition = new Vector3(-2.378f, -4.374f, -0.855f);
    var cameraRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);

    var actual = playerToKeyframeConverter.Convert(player, previousKeyframe, metrics, cameraPosition, cameraRotation);

    Assert.AreEqual(Emote.Joy, actual.FacialAnimation.Emote);
    Assert.AreEqual(0.239f, actual.FacialAnimation.LipRaised);
    Assert.AreEqual(0.721f, actual.FacialAnimation.JawLowered);
    Assert.AreEqual(0.203f, actual.FacialAnimation.MouthWidth);
    Assert.AreEqual(new Vector3(0.28663993f, -4.469082f, -5.3942194f), actual.Position);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.429f, 0.133f, 0.553f), actual.HipsRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.481f, 0.873f, 0.822f), actual.ShouldersRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.269f, 0.023f, 0.875f), actual.HeadRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f), actual.FacingRotation);
    Assert.AreEqual(new Vector3(0.526f, 0.358f, 0.940f), actual.LeftArm.Extension);
    Assert.AreEqual(new Vector3(0.184f, 0.135f, 0.375f), actual.LeftArm.BendNormal);
    Assert.AreEqual(new Vector3(0.630f, 0.312f, 0.635f), actual.LeftArm.TipNormal);
    Assert.AreEqual(new Vector3(0.351f, 0.618f, 0.219f), actual.RightArm.Extension);
    Assert.AreEqual(new Vector3(0.256f, 0.994f, 0.228f), actual.RightArm.BendNormal);
    Assert.AreEqual(new Vector3(0.636f, 0.038f, 0.551f), actual.RightArm.TipNormal);
    Assert.AreEqual(new Vector3(0.278f, 0.053f, 0.166f), actual.LeftLeg.Extension);
    Assert.AreEqual(new Vector3(0.761f, 0.848f, 0.046f), actual.LeftLeg.BendNormal);
    Assert.AreEqual(new Vector3(0.802f, 0.624f, 0.401f), actual.LeftLeg.TipNormal);
    Assert.AreEqual(new Vector3(0.074f, 0.881f, 0.708f), actual.RightLeg.Extension);
    Assert.AreEqual(new Vector3(0.114f, 0.940f, 0.551f), actual.RightLeg.BendNormal);
    Assert.AreEqual(new Vector3(0.316f, 0.938f, 0.898f), actual.RightLeg.TipNormal);
    playerToKeyframeConverterTasks.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ReturnsPreviousKeyframeWithNewPositionWhenShouldersOverlap()
  {
    var playerToKeyframeConverterTasks = new Mock<IPlayerToKeyframeConverterTasks>();
    var playerToKeyframeConverter = new PlayerToKeyframeConverter<uint>(playerToKeyframeConverterTasks.Object);
    var player = new Player<uint>
    {
      FrameNumber = 248234,
      ApproximatePosition = new Vector3(-0.025f, -4.43f, 2.844f),
      Details = new PlayerDetails
      {
        HeadUpNormal = new Vector3(0.066f, 0.476f, -0.013f),
        HeadForwardNormal = new Vector3(0.066f, 0.476f, -0.013f),
        LeftArm = new Limb
        {
          ProximalPosition = new Vector3(0.9f, 0.5f, 0.2f),
        },
        RightArm = new Limb
        {
          ProximalPosition = new Vector3(0.9f, 0.5f, 0.2f),
        },
        LeftLeg = new Limb
        {
          ProximalPosition = new Vector3(0.8f, 0.8f, 0.1f),
        },
        RightLeg = new Limb
        {
          ProximalPosition = new Vector3(0.8f, 0.9f, 0.8f),
        },
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframe
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
      FacingRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f),
      LeftArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.526f, 0.358f, 0.940f),
        BendNormal = new Vector3(0.184f, 0.135f, 0.375f),
        TipNormal = new Vector3(0.630f, 0.312f, 0.635f),
        TipLength = 0.376f,
      },
      RightArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.351f, 0.618f, 0.219f),
        BendNormal = new Vector3(0.256f, 0.994f, 0.228f),
        TipNormal = new Vector3(0.636f, 0.038f, 0.551f),
        TipLength = 0.360f,
      },
      LeftLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.278f, 0.053f, 0.166f),
        BendNormal = new Vector3(0.761f, 0.848f, 0.046f),
        TipNormal = new Vector3(0.802f, 0.624f, 0.401f),
        TipLength = 0.495f,
      },
      RightLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.074f, 0.881f, 0.708f),
        BendNormal = new Vector3(0.114f, 0.940f, 0.551f),
        TipNormal = new Vector3(0.316f, 0.938f, 0.898f),
        TipLength = 0.400f,
      },
    };
    var metrics = new Metrics
    {
      LeftArmLength = 12.4f,
      RightArmLength = 33.1f,
      LeftLegLength = 8.7f,
      RightLegLength = 41.82f,
    };
    var cameraPosition = new Vector3(-2.378f, -4.374f, -0.855f);
    var cameraRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);

    var actual = playerToKeyframeConverter.Convert(player, previousKeyframe, metrics, cameraPosition, cameraRotation);

    Assert.AreEqual(Emote.Joy, actual.FacialAnimation.Emote);
    Assert.AreEqual(0.239f, actual.FacialAnimation.LipRaised);
    Assert.AreEqual(0.721f, actual.FacialAnimation.JawLowered);
    Assert.AreEqual(0.203f, actual.FacialAnimation.MouthWidth);
    Assert.AreEqual(new Vector3(-3.2794166f, -45.326576f, -0.8503135f), actual.Position);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.429f, 0.133f, 0.553f), actual.HipsRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.481f, 0.873f, 0.822f), actual.ShouldersRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.269f, 0.023f, 0.875f), actual.HeadRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f), actual.FacingRotation);
    Assert.AreEqual(new Vector3(0.526f, 0.358f, 0.940f), actual.LeftArm.Extension);
    Assert.AreEqual(new Vector3(0.184f, 0.135f, 0.375f), actual.LeftArm.BendNormal);
    Assert.AreEqual(new Vector3(0.630f, 0.312f, 0.635f), actual.LeftArm.TipNormal);
    Assert.AreEqual(new Vector3(0.351f, 0.618f, 0.219f), actual.RightArm.Extension);
    Assert.AreEqual(new Vector3(0.256f, 0.994f, 0.228f), actual.RightArm.BendNormal);
    Assert.AreEqual(new Vector3(0.636f, 0.038f, 0.551f), actual.RightArm.TipNormal);
    Assert.AreEqual(new Vector3(0.278f, 0.053f, 0.166f), actual.LeftLeg.Extension);
    Assert.AreEqual(new Vector3(0.761f, 0.848f, 0.046f), actual.LeftLeg.BendNormal);
    Assert.AreEqual(new Vector3(0.802f, 0.624f, 0.401f), actual.LeftLeg.TipNormal);
    Assert.AreEqual(new Vector3(0.074f, 0.881f, 0.708f), actual.RightLeg.Extension);
    Assert.AreEqual(new Vector3(0.114f, 0.940f, 0.551f), actual.RightLeg.BendNormal);
    Assert.AreEqual(new Vector3(0.316f, 0.938f, 0.898f), actual.RightLeg.TipNormal);
    playerToKeyframeConverterTasks.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ReturnsPreviousKeyframeWithNewPositionWhenHipsOverlap()
  {
    var playerToKeyframeConverterTasks = new Mock<IPlayerToKeyframeConverterTasks>();
    var playerToKeyframeConverter = new PlayerToKeyframeConverter<uint>(playerToKeyframeConverterTasks.Object);
    var player = new Player<uint>
    {
      FrameNumber = 248234,
      ApproximatePosition = new Vector3(-0.025f, -4.43f, 2.844f),
      Details = new PlayerDetails
      {
        HeadUpNormal = new Vector3(0.066f, 0.476f, -0.013f),
        HeadForwardNormal = new Vector3(0.066f, 0.476f, -0.013f),
        LeftArm = new Limb
        {
          ProximalPosition = new Vector3(1.0f, 0.7f, 0.9f),
        },
        RightArm = new Limb
        {
          ProximalPosition = new Vector3(0.9f, 0.5f, 0.2f),
        },
        LeftLeg = new Limb
        {
          ProximalPosition = new Vector3(0.8f, 0.8f, 0.1f),
        },
        RightLeg = new Limb
        {
          ProximalPosition = new Vector3(0.8f, 0.8f, 0.1f),
        },
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframe
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
      FacingRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f),
      LeftArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.526f, 0.358f, 0.940f),
        BendNormal = new Vector3(0.184f, 0.135f, 0.375f),
        TipNormal = new Vector3(0.630f, 0.312f, 0.635f),
        TipLength = 0.376f,
      },
      RightArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.351f, 0.618f, 0.219f),
        BendNormal = new Vector3(0.256f, 0.994f, 0.228f),
        TipNormal = new Vector3(0.636f, 0.038f, 0.551f),
        TipLength = 0.360f,
      },
      LeftLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.278f, 0.053f, 0.166f),
        BendNormal = new Vector3(0.761f, 0.848f, 0.046f),
        TipNormal = new Vector3(0.802f, 0.624f, 0.401f),
        TipLength = 0.495f,
      },
      RightLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.074f, 0.881f, 0.708f),
        BendNormal = new Vector3(0.114f, 0.940f, 0.551f),
        TipNormal = new Vector3(0.316f, 0.938f, 0.898f),
        TipLength = 0.400f,
      },
    };
    var metrics = new Metrics
    {
      LeftArmLength = 12.4f,
      RightArmLength = 33.1f,
      LeftLegLength = 8.7f,
      RightLegLength = 41.82f,
    };
    var cameraPosition = new Vector3(-2.378f, -4.374f, -0.855f);
    var cameraRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);

    var actual = playerToKeyframeConverter.Convert(player, previousKeyframe, metrics, cameraPosition, cameraRotation);

    Assert.AreEqual(Emote.Joy, actual.FacialAnimation.Emote);
    Assert.AreEqual(0.239f, actual.FacialAnimation.LipRaised);
    Assert.AreEqual(0.721f, actual.FacialAnimation.JawLowered);
    Assert.AreEqual(0.203f, actual.FacialAnimation.MouthWidth);
    Assert.AreEqual(new Vector3(0.28663993f, -4.469082f, -5.3942194f), actual.Position);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.429f, 0.133f, 0.553f), actual.HipsRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.481f, 0.873f, 0.822f), actual.ShouldersRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.269f, 0.023f, 0.875f), actual.HeadRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f), actual.FacingRotation);
    Assert.AreEqual(new Vector3(0.526f, 0.358f, 0.940f), actual.LeftArm.Extension);
    Assert.AreEqual(new Vector3(0.184f, 0.135f, 0.375f), actual.LeftArm.BendNormal);
    Assert.AreEqual(new Vector3(0.630f, 0.312f, 0.635f), actual.LeftArm.TipNormal);
    Assert.AreEqual(new Vector3(0.351f, 0.618f, 0.219f), actual.RightArm.Extension);
    Assert.AreEqual(new Vector3(0.256f, 0.994f, 0.228f), actual.RightArm.BendNormal);
    Assert.AreEqual(new Vector3(0.636f, 0.038f, 0.551f), actual.RightArm.TipNormal);
    Assert.AreEqual(new Vector3(0.278f, 0.053f, 0.166f), actual.LeftLeg.Extension);
    Assert.AreEqual(new Vector3(0.761f, 0.848f, 0.046f), actual.LeftLeg.BendNormal);
    Assert.AreEqual(new Vector3(0.802f, 0.624f, 0.401f), actual.LeftLeg.TipNormal);
    Assert.AreEqual(new Vector3(0.074f, 0.881f, 0.708f), actual.RightLeg.Extension);
    Assert.AreEqual(new Vector3(0.114f, 0.940f, 0.551f), actual.RightLeg.BendNormal);
    Assert.AreEqual(new Vector3(0.316f, 0.938f, 0.898f), actual.RightLeg.TipNormal);
    playerToKeyframeConverterTasks.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ReturnsPreviousKeyframeWithNewPositionWhenShoulderAndHipCentersOverlap()
  {
    var playerToKeyframeConverterTasks = new Mock<IPlayerToKeyframeConverterTasks>();
    var playerToKeyframeConverter = new PlayerToKeyframeConverter<uint>(playerToKeyframeConverterTasks.Object);
    var player = new Player<uint>
    {
      FrameNumber = 248234,
      ApproximatePosition = new Vector3(-0.025f, -4.43f, 2.844f),
      Details = new PlayerDetails
      {
        HeadUpNormal = new Vector3(0.066f, 0.476f, -0.013f),
        HeadForwardNormal = new Vector3(0.066f, 0.476f, -0.013f),
        LeftArm = new Limb
        {
          ProximalPosition = new Vector3(1.0f, 0.7f, 0.9f),
        },
        RightArm = new Limb
        {
          ProximalPosition = new Vector3(0.8f, 0.7f, 0.9f),
        },
        LeftLeg = new Limb
        {
          ProximalPosition = new Vector3(0.9f, 0.6f, 0.9f),
        },
        RightLeg = new Limb
        {
          ProximalPosition = new Vector3(0.9f, 0.8f, 0.9f),
        },
      }
    };
    var previousKeyframe = new InterpolatablePlayerKeyframe
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
      FacingRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f),
      LeftArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.526f, 0.358f, 0.940f),
        BendNormal = new Vector3(0.184f, 0.135f, 0.375f),
        TipNormal = new Vector3(0.630f, 0.312f, 0.635f),
        TipLength = 0.376f,
      },
      RightArm = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.351f, 0.618f, 0.219f),
        BendNormal = new Vector3(0.256f, 0.994f, 0.228f),
        TipNormal = new Vector3(0.636f, 0.038f, 0.551f),
        TipLength = 0.360f,
      },
      LeftLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.278f, 0.053f, 0.166f),
        BendNormal = new Vector3(0.761f, 0.848f, 0.046f),
        TipNormal = new Vector3(0.802f, 0.624f, 0.401f),
        TipLength = 0.495f,
      },
      RightLeg = new InterpolatablePlayerKeyframeLimb
      {
        Extension = new Vector3(0.074f, 0.881f, 0.708f),
        BendNormal = new Vector3(0.114f, 0.940f, 0.551f),
        TipNormal = new Vector3(0.316f, 0.938f, 0.898f),
        TipLength = 0.400f,
      },
    };
    var metrics = new Metrics
    {
      LeftArmLength = 12.4f,
      RightArmLength = 33.1f,
      LeftLegLength = 8.7f,
      RightLegLength = 41.82f,
    };
    var cameraPosition = new Vector3(-2.378f, -4.374f, -0.855f);
    var cameraRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);

    var actual = playerToKeyframeConverter.Convert(player, previousKeyframe, metrics, cameraPosition, cameraRotation);

    Assert.AreEqual(Emote.Joy, actual.FacialAnimation.Emote);
    Assert.AreEqual(0.239f, actual.FacialAnimation.LipRaised);
    Assert.AreEqual(0.721f, actual.FacialAnimation.JawLowered);
    Assert.AreEqual(0.203f, actual.FacialAnimation.MouthWidth);
    Assert.AreEqual(new Vector3(-3.1708078f, -45.03302f, -1.2204893f), actual.Position);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.429f, 0.133f, 0.553f), actual.HipsRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.481f, 0.873f, 0.822f), actual.ShouldersRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(0.269f, 0.023f, 0.875f), actual.HeadRotation);
    Assert.AreEqual(Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f), actual.FacingRotation);
    Assert.AreEqual(new Vector3(0.526f, 0.358f, 0.940f), actual.LeftArm.Extension);
    Assert.AreEqual(new Vector3(0.184f, 0.135f, 0.375f), actual.LeftArm.BendNormal);
    Assert.AreEqual(new Vector3(0.630f, 0.312f, 0.635f), actual.LeftArm.TipNormal);
    Assert.AreEqual(new Vector3(0.351f, 0.618f, 0.219f), actual.RightArm.Extension);
    Assert.AreEqual(new Vector3(0.256f, 0.994f, 0.228f), actual.RightArm.BendNormal);
    Assert.AreEqual(new Vector3(0.636f, 0.038f, 0.551f), actual.RightArm.TipNormal);
    Assert.AreEqual(new Vector3(0.278f, 0.053f, 0.166f), actual.LeftLeg.Extension);
    Assert.AreEqual(new Vector3(0.761f, 0.848f, 0.046f), actual.LeftLeg.BendNormal);
    Assert.AreEqual(new Vector3(0.802f, 0.624f, 0.401f), actual.LeftLeg.TipNormal);
    Assert.AreEqual(new Vector3(0.074f, 0.881f, 0.708f), actual.RightLeg.Extension);
    Assert.AreEqual(new Vector3(0.114f, 0.940f, 0.551f), actual.RightLeg.BendNormal);
    Assert.AreEqual(new Vector3(0.316f, 0.938f, 0.898f), actual.RightLeg.TipNormal);
    playerToKeyframeConverterTasks.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void UpdatesWithoutFacialAnimation()
  {
    var playerToKeyframeConverterTasks = new Mock<IPlayerToKeyframeConverterTasks>();
    var playerToKeyframeConverter = new PlayerToKeyframeConverter<uint>(playerToKeyframeConverterTasks.Object);
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
    var headUpNormal = new Vector3(0.066f, 0.476f, -0.013f);
    var headForwardNormal = new Vector3(0.066f, 0.476f, -0.013f);
    var player = new Player<uint>
    {
      FrameNumber = 248234,
      ApproximatePosition = new Vector3(-0.025f, -4.43f, 2.844f),
      Details = new PlayerDetails
      {
        HeadUpNormal = headUpNormal,
        HeadForwardNormal = headForwardNormal,
        LeftArm = leftArm,
        RightArm = rightArm,
        LeftLeg = leftLeg,
        RightLeg = rightLeg,
      }
    };
    var previousKeyframeLeftArmBendNormal = new Vector3(0.184f, 0.135f, 0.375f);
    var previousKeyframeLeftArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.526f, 0.358f, 0.940f),
      BendNormal = previousKeyframeLeftArmBendNormal,
      TipNormal = new Vector3(0.630f, 0.312f, 0.635f),
      TipLength = 0.376f,
    };
    var previousKeyframeRightArmBendNormal = new Vector3(0.256f, 0.994f, 0.228f);
    var previousKeyframeRightArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.351f, 0.618f, 0.219f),
      BendNormal = previousKeyframeRightArmBendNormal,
      TipNormal = new Vector3(0.636f, 0.038f, 0.551f),
      TipLength = 0.360f,
    };
    var previousKeyframeLeftLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.278f, 0.053f, 0.166f),
      BendNormal = new Vector3(0.761f, 0.848f, 0.046f),
      TipNormal = new Vector3(0.802f, 0.624f, 0.401f),
      TipLength = 0.495f,
    };
    var previousKeyframeRightLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.074f, 0.881f, 0.708f),
      BendNormal = new Vector3(0.114f, 0.940f, 0.551f),
      TipNormal = new Vector3(0.316f, 0.938f, 0.898f),
      TipLength = 0.400f,
    };
    var previousKeyframe = new InterpolatablePlayerKeyframe
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
      FacingRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f),
      LeftArm = previousKeyframeLeftArm,
      RightArm = previousKeyframeRightArm,
      LeftLeg = previousKeyframeLeftLeg,
      RightLeg = previousKeyframeRightLeg,
    };
    var metrics = new Metrics
    {
      LeftArmLength = 12.4f,
      RightArmLength = 33.1f,
      LeftLegLength = 8.7f,
      RightLegLength = 41.82f,
    };
    var cameraPosition = new Vector3(-2.378f, -4.374f, -0.855f);
    var cameraRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);
    var convertedHead = Quaternion.CreateFromYawPitchRoll(-3.843f, -3.384f, 4.563f);
    playerToKeyframeConverterTasks.Setup(x => x.ConvertHead(It.IsAny<Quaternion>(), It.IsAny<Quaternion>(), It.IsAny<Quaternion>(), It.IsAny<Vector3?>(), It.IsAny<Vector3?>(), It.IsAny<Quaternion>(), It.IsAny<Quaternion>())).Returns(convertedHead);
    var convertedLeftArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.526f, 0.358f, 0.940f),
      BendNormal = new Vector3(0.184f, 0.135f, 0.375f),
      TipNormal = new Vector3(0.630f, 0.312f, 0.635f),
      TipLength = 0.376f,
    };
    playerToKeyframeConverterTasks.Setup(x => x.ConvertLimb(leftArm, It.IsAny<InterpolatablePlayerKeyframeLimb>(), It.IsAny<Vector3>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<Quaternion>())).Returns(convertedLeftArm);
    var convertedRightArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.351f, 0.618f, 0.219f),
      BendNormal = new Vector3(0.256f, 0.994f, 0.228f),
      TipNormal = new Vector3(0.636f, 0.038f, 0.551f),
      TipLength = 0.360f,
    };
    playerToKeyframeConverterTasks.Setup(x => x.ConvertLimb(rightArm, It.IsAny<InterpolatablePlayerKeyframeLimb>(), It.IsAny<Vector3>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<Quaternion>())).Returns(convertedRightArm);
    var convertedLeftLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.278f, 0.053f, 0.166f),
      BendNormal = new Vector3(0.761f, 0.848f, 0.046f),
      TipNormal = new Vector3(0.802f, 0.624f, 0.401f),
      TipLength = 0.495f,
    };
    playerToKeyframeConverterTasks.Setup(x => x.ConvertLimb(leftLeg, It.IsAny<InterpolatablePlayerKeyframeLimb>(), It.IsAny<Vector3>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<Quaternion>())).Returns(convertedLeftLeg);
    var convertedRightLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.074f, 0.881f, 0.708f),
      BendNormal = new Vector3(0.114f, 0.940f, 0.551f),
      TipNormal = new Vector3(0.316f, 0.938f, 0.898f),
      TipLength = 0.400f,
    };
    playerToKeyframeConverterTasks.Setup(x => x.ConvertLimb(rightLeg, It.IsAny<InterpolatablePlayerKeyframeLimb>(), It.IsAny<Vector3>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<Quaternion>())).Returns(convertedRightLeg);

    var actual = playerToKeyframeConverter.Convert(player, previousKeyframe, metrics, cameraPosition, cameraRotation);

    Assert.AreEqual(Emote.Joy, actual.FacialAnimation.Emote);
    Assert.AreEqual(0.239f, actual.FacialAnimation.LipRaised);
    Assert.AreEqual(0.721f, actual.FacialAnimation.JawLowered);
    Assert.AreEqual(0.203f, actual.FacialAnimation.MouthWidth);
    Assert.AreEqual(new Vector3(-3.2794166f, -45.326576f, -0.8503135f), actual.Position);
    Assert.AreEqual(new Quaternion(0.2527676f, -0.54571784f, 0.78534234f, 0.14675854f), actual.HipsRotation);
    Assert.AreEqual(new Quaternion(-0.7508658f, -0.21950011f, 0.308128f, -0.5413663f), actual.ShouldersRotation);
    Assert.AreEqual(convertedHead, actual.HeadRotation);
    Assert.AreEqual(new Quaternion(0, 0.9326091f, 0, 0.3608883f), actual.FacingRotation);
    Assert.AreEqual(convertedLeftArm, actual.LeftArm);
    Assert.AreEqual(convertedRightArm, actual.RightArm);
    Assert.AreEqual(convertedLeftLeg, actual.LeftLeg);
    Assert.AreEqual(convertedRightLeg, actual.RightLeg);
    var cameraToInverseFacingRotation = new Quaternion(0.46762705f, -0.08693318f, -0.06165062f, -0.8774775f);
    playerToKeyframeConverterTasks.Verify(x => x.ConvertHead(new Quaternion(0, 0.9326091f, 0, 0.3608883f), new Quaternion(-0, -0.9326091f, -0, 0.3608883f), new Quaternion(0.067131594f, 0.116628736f, 0.41842526f, 0.89822686f), headForwardNormal, headUpNormal, cameraRotation, cameraToInverseFacingRotation), Times.Once());
    playerToKeyframeConverterTasks.Verify(x => x.ConvertLimb(leftArm, previousKeyframeLeftArm, previousKeyframeLeftArmBendNormal, 0.025f, 12.4f, cameraToInverseFacingRotation), Times.Once());
    playerToKeyframeConverterTasks.Verify(x => x.ConvertLimb(rightArm, previousKeyframeRightArm, previousKeyframeRightArmBendNormal, 0.025f, 33.1f, cameraToInverseFacingRotation), Times.Once());
    var fallbackLegBendNormal = new Vector3(0, 0, 1);
    playerToKeyframeConverterTasks.Verify(x => x.ConvertLimb(leftLeg, previousKeyframeLeftLeg, fallbackLegBendNormal, 0.125f, 8.7f, cameraToInverseFacingRotation), Times.Once());
    playerToKeyframeConverterTasks.Verify(x => x.ConvertLimb(rightLeg, previousKeyframeRightLeg, fallbackLegBendNormal, 0.125f, 41.82f, cameraToInverseFacingRotation), Times.Once());
    playerToKeyframeConverterTasks.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void UpdatesWithFacialAnimation()
  {
    var playerToKeyframeConverterTasks = new Mock<IPlayerToKeyframeConverterTasks>();
    var playerToKeyframeConverter = new PlayerToKeyframeConverter<uint>(playerToKeyframeConverterTasks.Object);
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
    var headUpNormal = new Vector3(0.066f, 0.476f, -0.013f);
    var headForwardNormal = new Vector3(0.066f, 0.476f, -0.013f);
    var player = new Player<uint>
    {
      FrameNumber = 248234,
      ApproximatePosition = new Vector3(-0.025f, -4.43f, 2.844f),
      Details = new PlayerDetails
      {
        HeadUpNormal = headUpNormal,
        HeadForwardNormal = headForwardNormal,
        FacialAnimation = new FacialAnimation
        {
          Emote = Emote.Surprised,
          LipRaised = 0.630f,
          JawLowered = 0.484f,
          MouthWidth = 0.513f,
        },
        LeftArm = leftArm,
        RightArm = rightArm,
        LeftLeg = leftLeg,
        RightLeg = rightLeg,
      }
    };
    var previousKeyframeLeftArmBendNormal = new Vector3(0.184f, 0.135f, 0.375f);
    var previousKeyframeLeftArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.526f, 0.358f, 0.940f),
      BendNormal = previousKeyframeLeftArmBendNormal,
      TipNormal = new Vector3(0.630f, 0.312f, 0.635f),
      TipLength = 0.376f,
    };
    var previousKeyframeRightArmBendNormal = new Vector3(0.256f, 0.994f, 0.228f);
    var previousKeyframeRightArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.351f, 0.618f, 0.219f),
      BendNormal = previousKeyframeRightArmBendNormal,
      TipNormal = new Vector3(0.636f, 0.038f, 0.551f),
      TipLength = 0.360f,
    };
    var previousKeyframeLeftLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.278f, 0.053f, 0.166f),
      BendNormal = new Vector3(0.761f, 0.848f, 0.046f),
      TipNormal = new Vector3(0.802f, 0.624f, 0.401f),
      TipLength = 0.495f,
    };
    var previousKeyframeRightLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.074f, 0.881f, 0.708f),
      BendNormal = new Vector3(0.114f, 0.940f, 0.551f),
      TipNormal = new Vector3(0.316f, 0.938f, 0.898f),
      TipLength = 0.400f,
    };
    var previousKeyframe = new InterpolatablePlayerKeyframe
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
      FacingRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f),
      LeftArm = previousKeyframeLeftArm,
      RightArm = previousKeyframeRightArm,
      LeftLeg = previousKeyframeLeftLeg,
      RightLeg = previousKeyframeRightLeg,
    };
    var metrics = new Metrics
    {
      LeftArmLength = 12.4f,
      RightArmLength = 33.1f,
      LeftLegLength = 8.7f,
      RightLegLength = 41.82f,
    };
    var cameraPosition = new Vector3(-2.378f, -4.374f, -0.855f);
    var cameraRotation = Quaternion.CreateFromYawPitchRoll(-6.85f, -2.16f, 3.19f);
    var convertedHead = Quaternion.CreateFromYawPitchRoll(-3.843f, -3.384f, 4.563f);
    playerToKeyframeConverterTasks.Setup(x => x.ConvertHead(It.IsAny<Quaternion>(), It.IsAny<Quaternion>(), It.IsAny<Quaternion>(), It.IsAny<Vector3?>(), It.IsAny<Vector3?>(), It.IsAny<Quaternion>(), It.IsAny<Quaternion>())).Returns(convertedHead);
    var convertedLeftArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.526f, 0.358f, 0.940f),
      BendNormal = new Vector3(0.184f, 0.135f, 0.375f),
      TipNormal = new Vector3(0.630f, 0.312f, 0.635f),
      TipLength = 0.376f,
    };
    playerToKeyframeConverterTasks.Setup(x => x.ConvertLimb(leftArm, It.IsAny<InterpolatablePlayerKeyframeLimb>(), It.IsAny<Vector3>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<Quaternion>())).Returns(convertedLeftArm);
    var convertedRightArm = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.351f, 0.618f, 0.219f),
      BendNormal = new Vector3(0.256f, 0.994f, 0.228f),
      TipNormal = new Vector3(0.636f, 0.038f, 0.551f),
      TipLength = 0.360f,
    };
    playerToKeyframeConverterTasks.Setup(x => x.ConvertLimb(rightArm, It.IsAny<InterpolatablePlayerKeyframeLimb>(), It.IsAny<Vector3>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<Quaternion>())).Returns(convertedRightArm);
    var convertedLeftLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.278f, 0.053f, 0.166f),
      BendNormal = new Vector3(0.761f, 0.848f, 0.046f),
      TipNormal = new Vector3(0.802f, 0.624f, 0.401f),
      TipLength = 0.495f,
    };
    playerToKeyframeConverterTasks.Setup(x => x.ConvertLimb(leftLeg, It.IsAny<InterpolatablePlayerKeyframeLimb>(), It.IsAny<Vector3>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<Quaternion>())).Returns(convertedLeftLeg);
    var convertedRightLeg = new InterpolatablePlayerKeyframeLimb
    {
      Extension = new Vector3(0.074f, 0.881f, 0.708f),
      BendNormal = new Vector3(0.114f, 0.940f, 0.551f),
      TipNormal = new Vector3(0.316f, 0.938f, 0.898f),
      TipLength = 0.400f,
    };
    playerToKeyframeConverterTasks.Setup(x => x.ConvertLimb(rightLeg, It.IsAny<InterpolatablePlayerKeyframeLimb>(), It.IsAny<Vector3>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<Quaternion>())).Returns(convertedRightLeg);

    var actual = playerToKeyframeConverter.Convert(player, previousKeyframe, metrics, cameraPosition, cameraRotation);

    Assert.AreEqual(Emote.Surprised, actual.FacialAnimation.Emote);
    Assert.AreEqual(0.630f, actual.FacialAnimation.LipRaised);
    Assert.AreEqual(0.484f, actual.FacialAnimation.JawLowered);
    Assert.AreEqual(0.513f, actual.FacialAnimation.MouthWidth);
    Assert.AreEqual(new Vector3(-3.2794166f, -45.326576f, -0.8503135f), actual.Position);
    Assert.AreEqual(new Quaternion(0.2527676f, -0.54571784f, 0.78534234f, 0.14675854f), actual.HipsRotation);
    Assert.AreEqual(new Quaternion(-0.7508658f, -0.21950011f, 0.308128f, -0.5413663f), actual.ShouldersRotation);
    Assert.AreEqual(convertedHead, actual.HeadRotation);
    Assert.AreEqual(new Quaternion(0, 0.9326091f, 0, 0.3608883f), actual.FacingRotation);
    Assert.AreEqual(convertedLeftArm, actual.LeftArm);
    Assert.AreEqual(convertedRightArm, actual.RightArm);
    Assert.AreEqual(convertedLeftLeg, actual.LeftLeg);
    Assert.AreEqual(convertedRightLeg, actual.RightLeg);
    var cameraToInverseFacingRotation = new Quaternion(0.46762705f, -0.08693318f, -0.06165062f, -0.8774775f);
    playerToKeyframeConverterTasks.Verify(x => x.ConvertHead(new Quaternion(0, 0.9326091f, 0, 0.3608883f), new Quaternion(-0, -0.9326091f, -0, 0.3608883f), new Quaternion(0.067131594f, 0.116628736f, 0.41842526f, 0.89822686f), headForwardNormal, headUpNormal, cameraRotation, cameraToInverseFacingRotation), Times.Once());
    playerToKeyframeConverterTasks.Verify(x => x.ConvertLimb(leftArm, previousKeyframeLeftArm, previousKeyframeLeftArmBendNormal, 0.025f, 12.4f, cameraToInverseFacingRotation), Times.Once());
    playerToKeyframeConverterTasks.Verify(x => x.ConvertLimb(rightArm, previousKeyframeRightArm, previousKeyframeRightArmBendNormal, 0.025f, 33.1f, cameraToInverseFacingRotation), Times.Once());
    var fallbackLegBendNormal = new Vector3(0, 0, 1);
    playerToKeyframeConverterTasks.Verify(x => x.ConvertLimb(leftLeg, previousKeyframeLeftLeg, fallbackLegBendNormal, 0.125f, 8.7f, cameraToInverseFacingRotation), Times.Once());
    playerToKeyframeConverterTasks.Verify(x => x.ConvertLimb(rightLeg, previousKeyframeRightLeg, fallbackLegBendNormal, 0.125f, 41.82f, cameraToInverseFacingRotation), Times.Once());
    playerToKeyframeConverterTasks.VerifyNoOtherCalls();
  }
}
