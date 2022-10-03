using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.FaceTrackLib;
using SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kernel32;
using SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10;
using SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10.CoordinateMapping;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]
namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.UnitTests;

[TestClass]
public sealed class KinectTests
{
  [TestMethod]
  public void ThrowsAnExceptionWhenTheRequestedKinectCannotBeCreatedDueToAnErrorCode()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    ISensor? sensor = null;

    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensor)).Returns(14234);

    try
    {
      new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14);
      Assert.Fail();
    }
    catch (Exception exception)
    {
      Assert.AreEqual("Failed to create an instance of Kinect \"Test Unique Identifier\"; error code 14234.", exception.Message);
      Assert.IsNull(exception.InnerException);
    }

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ThrowsAnExceptionWhenTheRequestedKinectCannotBeCreatedAsNullWasReturned()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    ISensor? sensor = null;

    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensor)).Returns(0);

    try
    {
      new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14);
      Assert.Fail();
    }
    catch (Exception exception)
    {
      Assert.AreEqual("Failed to create an instance of Kinect \"Test Unique Identifier\"; no instance was output.", exception.Message);
      Assert.IsNull(exception.InnerException);
    }
    Thread.Sleep(5000);

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ThrowsAnExceptionWhenTheKinectCannotBeInitialized()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    var sensor = new Mock<ISensor>();

    var sensorReference = sensor.Object;
    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensorReference)).Returns(0);
    sensor.Setup(x => x.NuiInitialize(It.IsAny<InitializeFlags>())).Returns(14234);

    try
    {
      new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14);
      Assert.Fail();
    }
    catch (Exception exception)
    {
      Assert.AreEqual("Failed to initialize the Kinect; error code 14234.", exception.Message);
      Assert.IsNull(exception.InnerException);
    }
    Thread.Sleep(5000);

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
    sensor.Verify(x => x.NuiInitialize(InitializeFlags.UseSkeletalTracking | InitializeFlags.UseColor | InitializeFlags.UseDepthAndPlayerIndex), Times.Once());
    sensor.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ThrowsAnExceptionWhenTheKinectAngleCannotBeAdjusted()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    var sensor = new Mock<ISensor>();
    var coordinateMapper = new Mock<ICoordinateMapper>();

    var sensorReference = sensor.Object;
    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensorReference)).Returns(0);
    sensor.Setup(x => x.NuiInitialize(It.IsAny<InitializeFlags>())).Returns(0);
    sensor.Setup(x => x.NuiCameraElevationSetAngle(It.IsAny<int>())).Returns(14234);
    var coordinateMapperReference = coordinateMapper.Object;
    sensor.Setup(x => x.NuiGetCoordinateMapper(out coordinateMapperReference)).Returns(0);
    sensor.Setup(x => x.NuiSkeletonTrackingEnable(It.IsAny<IntPtr>(), It.IsAny<SkeletonTrackingFlags>())).Returns(0);
    var colorStreamReference = new IntPtr(39843);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.Color, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out colorStreamReference)).Returns(0);
    var depthStreamReference = new IntPtr(2313);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out depthStreamReference)).Returns(0);

    try
    {
      new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14);
      Assert.Fail();
    }
    catch (Exception exception)
    {
      Assert.AreEqual("Failed to set the Kinect's angle; error code 14234.", exception.Message);
      Assert.IsNull(exception.InnerException);
    }
    Thread.Sleep(5000);

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
    sensor.Verify(x => x.NuiInitialize(InitializeFlags.UseSkeletalTracking | InitializeFlags.UseColor | InitializeFlags.UseDepthAndPlayerIndex), Times.Once());
    sensor.Verify(x => x.NuiCameraElevationSetAngle(-14), Times.Once());
    sensor.Verify(x => x.NuiGetCoordinateMapper(out It.Ref<ICoordinateMapper?>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiSkeletonTrackingEnable(IntPtr.Zero, SkeletonTrackingFlags.AppChoosesSkeletons), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.Color, ImageResolution.SixHundredAndFortyByFourHundredAndEighty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, ImageResolution.ThreeHundredAndTwentyByTwoHundredAndForty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiShutdown(), Times.Once());
    sensor.VerifyNoOtherCalls();
    coordinateMapper.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ThrowsAnExceptionWhenRetrievingACoordinateMapperFailsAsAnErrorCodeIsReturned()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    var sensor = new Mock<ISensor>();
    ICoordinateMapper? coordinateMapper = null;

    var sensorReference = sensor.Object;
    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensorReference)).Returns(0);
    sensor.Setup(x => x.NuiInitialize(It.IsAny<InitializeFlags>())).Returns(0);
    sensor.Setup(x => x.NuiCameraElevationSetAngle(It.IsAny<int>())).Returns(0);
    sensor.Setup(x => x.NuiGetCoordinateMapper(out coordinateMapper)).Returns(14234);
    sensor.Setup(x => x.NuiSkeletonTrackingEnable(It.IsAny<IntPtr>(), It.IsAny<SkeletonTrackingFlags>())).Returns(0);
    var colorStreamReference = new IntPtr(39843);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.Color, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out colorStreamReference)).Returns(0);
    var depthStreamReference = new IntPtr(2313);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out depthStreamReference)).Returns(0);

    try
    {
      new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14);
      Assert.Fail();
    }
    catch (Exception exception)
    {
      Assert.AreEqual("Failed to get a coordinate mapper; error code 14234.", exception.Message);
      Assert.IsNull(exception.InnerException);
    }
    Thread.Sleep(5000);

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
    sensor.Verify(x => x.NuiInitialize(InitializeFlags.UseSkeletalTracking | InitializeFlags.UseColor | InitializeFlags.UseDepthAndPlayerIndex), Times.Once());
    sensor.Verify(x => x.NuiCameraElevationSetAngle(-14), Times.AtMostOnce());
    sensor.Verify(x => x.NuiGetCoordinateMapper(out It.Ref<ICoordinateMapper?>.IsAny), Times.Once());
    sensor.Verify(x => x.NuiSkeletonTrackingEnable(IntPtr.Zero, SkeletonTrackingFlags.AppChoosesSkeletons), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.Color, ImageResolution.SixHundredAndFortyByFourHundredAndEighty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, ImageResolution.ThreeHundredAndTwentyByTwoHundredAndForty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiShutdown(), Times.Once());
    sensor.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ThrowsAnExceptionWhenRetrievingACoordinateMapperFailsAsNoCoordinateMapperIsOutput()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    var sensor = new Mock<ISensor>();
    ICoordinateMapper? coordinateMapper = null;

    var sensorReference = sensor.Object;
    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensorReference)).Returns(0);
    sensor.Setup(x => x.NuiInitialize(It.IsAny<InitializeFlags>())).Returns(0);
    sensor.Setup(x => x.NuiCameraElevationSetAngle(It.IsAny<int>())).Returns(0);
    sensor.Setup(x => x.NuiGetCoordinateMapper(out coordinateMapper)).Returns(0);
    sensor.Setup(x => x.NuiSkeletonTrackingEnable(It.IsAny<IntPtr>(), It.IsAny<SkeletonTrackingFlags>())).Returns(0);
    var colorStreamReference = new IntPtr(39843);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.Color, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out colorStreamReference)).Returns(0);
    var depthStreamReference = new IntPtr(2313);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out depthStreamReference)).Returns(0);

    try
    {
      new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14);
      Assert.Fail();
    }
    catch (Exception exception)
    {
      Assert.AreEqual("Failed to get a coordinate mapper; no instance was output.", exception.Message);
      Assert.IsNull(exception.InnerException);
    }
    Thread.Sleep(5000);

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
    sensor.Verify(x => x.NuiInitialize(InitializeFlags.UseSkeletalTracking | InitializeFlags.UseColor | InitializeFlags.UseDepthAndPlayerIndex), Times.Once());
    sensor.Verify(x => x.NuiCameraElevationSetAngle(-14), Times.AtMostOnce());
    sensor.Verify(x => x.NuiGetCoordinateMapper(out It.Ref<ICoordinateMapper?>.IsAny), Times.Once());
    sensor.Verify(x => x.NuiSkeletonTrackingEnable(IntPtr.Zero, SkeletonTrackingFlags.AppChoosesSkeletons), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.Color, ImageResolution.SixHundredAndFortyByFourHundredAndEighty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, ImageResolution.ThreeHundredAndTwentyByTwoHundredAndForty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiShutdown(), Times.Once());
    sensor.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ThrowsAnExceptionWhenEnablingSkeletonTrackingFailsAsAnErrorCodeIsReturned()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    var sensor = new Mock<ISensor>();
    var coordinateMapper = new Mock<ICoordinateMapper>();

    var sensorReference = sensor.Object;
    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensorReference)).Returns(0);
    sensor.Setup(x => x.NuiInitialize(It.IsAny<InitializeFlags>())).Returns(0);
    sensor.Setup(x => x.NuiCameraElevationSetAngle(It.IsAny<int>())).Returns(0);
    var coordinateMapperReference = coordinateMapper.Object;
    sensor.Setup(x => x.NuiGetCoordinateMapper(out coordinateMapperReference)).Returns(0);
    sensor.Setup(x => x.NuiSkeletonTrackingEnable(It.IsAny<IntPtr>(), It.IsAny<SkeletonTrackingFlags>())).Returns(3489348);
    var colorStreamReference = new IntPtr(39843);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.Color, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out colorStreamReference)).Returns(0);
    var depthStreamReference = new IntPtr(2313);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out depthStreamReference)).Returns(0);

    try
    {
      new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14);
      Assert.Fail();
    }
    catch (Exception exception)
    {
      Assert.AreEqual("Failed to enable the Kinect's skeleton tracking; error code 3489348.", exception.Message);
      Assert.IsNull(exception.InnerException);
    }
    Thread.Sleep(5000);

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
    sensor.Verify(x => x.NuiInitialize(InitializeFlags.UseSkeletalTracking | InitializeFlags.UseColor | InitializeFlags.UseDepthAndPlayerIndex), Times.Once());
    sensor.Verify(x => x.NuiCameraElevationSetAngle(-14), Times.AtMostOnce());
    sensor.Verify(x => x.NuiGetCoordinateMapper(out It.Ref<ICoordinateMapper?>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiSkeletonTrackingEnable(IntPtr.Zero, SkeletonTrackingFlags.AppChoosesSkeletons), Times.Once());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.Color, ImageResolution.SixHundredAndFortyByFourHundredAndEighty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, ImageResolution.ThreeHundredAndTwentyByTwoHundredAndForty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiShutdown(), Times.Once());
    sensor.VerifyNoOtherCalls();
    coordinateMapper.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ThrowsAnExceptionWhenCreatingAColorStreamFailsAsAnErrorCodeIsReturned()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    var sensor = new Mock<ISensor>();
    var coordinateMapper = new Mock<ICoordinateMapper>();

    var sensorReference = sensor.Object;
    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensorReference)).Returns(0);
    sensor.Setup(x => x.NuiInitialize(It.IsAny<InitializeFlags>())).Returns(0);
    sensor.Setup(x => x.NuiCameraElevationSetAngle(It.IsAny<int>())).Returns(0);
    var coordinateMapperReference = coordinateMapper.Object;
    sensor.Setup(x => x.NuiGetCoordinateMapper(out coordinateMapperReference)).Returns(0);
    sensor.Setup(x => x.NuiSkeletonTrackingEnable(It.IsAny<IntPtr>(), It.IsAny<SkeletonTrackingFlags>())).Returns(0);
    var colorStreamReference = IntPtr.Zero;
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.Color, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out colorStreamReference)).Returns(3493);
    var depthStreamReference = new IntPtr(2313);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out depthStreamReference)).Returns(0);

    try
    {
      new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14);
      Assert.Fail();
    }
    catch (Exception exception)
    {
      Assert.AreEqual("Failed to enable the Kinect's color stream; error code 3493.", exception.Message);
      Assert.IsNull(exception.InnerException);
    }
    Thread.Sleep(5000);

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
    sensor.Verify(x => x.NuiInitialize(InitializeFlags.UseSkeletalTracking | InitializeFlags.UseColor | InitializeFlags.UseDepthAndPlayerIndex), Times.Once());
    sensor.Verify(x => x.NuiCameraElevationSetAngle(-14), Times.AtMostOnce());
    sensor.Verify(x => x.NuiGetCoordinateMapper(out It.Ref<ICoordinateMapper?>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiSkeletonTrackingEnable(IntPtr.Zero, SkeletonTrackingFlags.AppChoosesSkeletons), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.Color, ImageResolution.SixHundredAndFortyByFourHundredAndEighty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.Once());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, ImageResolution.ThreeHundredAndTwentyByTwoHundredAndForty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiShutdown(), Times.Once());
    sensor.VerifyNoOtherCalls();
    coordinateMapper.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ThrowsAnExceptionWhenCreatingAColorStreamFailsAsNoColorStreamIsOutput()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    var sensor = new Mock<ISensor>();
    var coordinateMapper = new Mock<ICoordinateMapper>();

    var sensorReference = sensor.Object;
    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensorReference)).Returns(0);
    sensor.Setup(x => x.NuiInitialize(It.IsAny<InitializeFlags>())).Returns(0);
    sensor.Setup(x => x.NuiCameraElevationSetAngle(It.IsAny<int>())).Returns(0);
    var coordinateMapperReference = coordinateMapper.Object;
    sensor.Setup(x => x.NuiGetCoordinateMapper(out coordinateMapperReference)).Returns(0);
    sensor.Setup(x => x.NuiSkeletonTrackingEnable(It.IsAny<IntPtr>(), It.IsAny<SkeletonTrackingFlags>())).Returns(0);
    var colorStreamReference = IntPtr.Zero;
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.Color, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out colorStreamReference)).Returns(0);
    var depthStreamReference = new IntPtr(2313);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out depthStreamReference)).Returns(0);

    try
    {
      new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14);
      Assert.Fail();
    }
    catch (Exception exception)
    {
      Assert.AreEqual("Failed to enable the Kinect's color stream; no instance was output.", exception.Message);
      Assert.IsNull(exception.InnerException);
    }
    Thread.Sleep(5000);

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
    sensor.Verify(x => x.NuiInitialize(InitializeFlags.UseSkeletalTracking | InitializeFlags.UseColor | InitializeFlags.UseDepthAndPlayerIndex), Times.Once());
    sensor.Verify(x => x.NuiCameraElevationSetAngle(-14), Times.AtMostOnce());
    sensor.Verify(x => x.NuiGetCoordinateMapper(out It.Ref<ICoordinateMapper?>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiSkeletonTrackingEnable(IntPtr.Zero, SkeletonTrackingFlags.AppChoosesSkeletons), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.Color, ImageResolution.SixHundredAndFortyByFourHundredAndEighty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.Once());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, ImageResolution.ThreeHundredAndTwentyByTwoHundredAndForty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiShutdown(), Times.Once());
    sensor.VerifyNoOtherCalls();
    coordinateMapper.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ThrowsAnExceptionWhenCreatingADepthStreamFailsAsAnErrorCodeIsReturned()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    var sensor = new Mock<ISensor>();
    var coordinateMapper = new Mock<ICoordinateMapper>();

    var sensorReference = sensor.Object;
    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensorReference)).Returns(0);
    sensor.Setup(x => x.NuiInitialize(It.IsAny<InitializeFlags>())).Returns(0);
    sensor.Setup(x => x.NuiCameraElevationSetAngle(It.IsAny<int>())).Returns(0);
    var coordinateMapperReference = coordinateMapper.Object;
    sensor.Setup(x => x.NuiGetCoordinateMapper(out coordinateMapperReference)).Returns(0);
    sensor.Setup(x => x.NuiSkeletonTrackingEnable(It.IsAny<IntPtr>(), It.IsAny<SkeletonTrackingFlags>())).Returns(0);
    var colorStreamReference = new IntPtr(2313);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.Color, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out colorStreamReference)).Returns(0);
    var depthStreamReference = IntPtr.Zero;
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out depthStreamReference)).Returns(3493);

    try
    {
      new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14);
      Assert.Fail();
    }
    catch (Exception exception)
    {
      Assert.AreEqual("Failed to enable the Kinect's depth stream; error code 3493.", exception.Message);
      Assert.IsNull(exception.InnerException);
    }
    Thread.Sleep(5000);

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
    sensor.Verify(x => x.NuiInitialize(InitializeFlags.UseSkeletalTracking | InitializeFlags.UseColor | InitializeFlags.UseDepthAndPlayerIndex), Times.Once());
    sensor.Verify(x => x.NuiCameraElevationSetAngle(-14), Times.AtMostOnce());
    sensor.Verify(x => x.NuiGetCoordinateMapper(out It.Ref<ICoordinateMapper?>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiSkeletonTrackingEnable(IntPtr.Zero, SkeletonTrackingFlags.AppChoosesSkeletons), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.Color, ImageResolution.SixHundredAndFortyByFourHundredAndEighty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, ImageResolution.ThreeHundredAndTwentyByTwoHundredAndForty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.Once());
    sensor.Verify(x => x.NuiShutdown(), Times.Once());
    sensor.VerifyNoOtherCalls();
    coordinateMapper.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void ThrowsAnExceptionWhenCreatingADepthStreamFailsAsNoColorStreamIsOutput()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    var sensor = new Mock<ISensor>();
    var coordinateMapper = new Mock<ICoordinateMapper>();

    var sensorReference = sensor.Object;
    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensorReference)).Returns(0);
    sensor.Setup(x => x.NuiInitialize(It.IsAny<InitializeFlags>())).Returns(0);
    sensor.Setup(x => x.NuiCameraElevationSetAngle(It.IsAny<int>())).Returns(0);
    var coordinateMapperReference = coordinateMapper.Object;
    sensor.Setup(x => x.NuiGetCoordinateMapper(out coordinateMapperReference)).Returns(0);
    sensor.Setup(x => x.NuiSkeletonTrackingEnable(It.IsAny<IntPtr>(), It.IsAny<SkeletonTrackingFlags>())).Returns(0);
    var colorStreamReference = new IntPtr(2313);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.Color, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out colorStreamReference)).Returns(0);
    var depthStreamReference = IntPtr.Zero;
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out depthStreamReference)).Returns(0);

    try
    {
      new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14);
      Assert.Fail();
    }
    catch (Exception exception)
    {
      Assert.AreEqual("Failed to enable the Kinect's depth stream; no instance was output.", exception.Message);
      Assert.IsNull(exception.InnerException);
    }
    Thread.Sleep(5000);

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
    sensor.Verify(x => x.NuiInitialize(InitializeFlags.UseSkeletalTracking | InitializeFlags.UseColor | InitializeFlags.UseDepthAndPlayerIndex), Times.Once());
    sensor.Verify(x => x.NuiCameraElevationSetAngle(-14), Times.AtMostOnce());
    sensor.Verify(x => x.NuiGetCoordinateMapper(out It.Ref<ICoordinateMapper?>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiSkeletonTrackingEnable(IntPtr.Zero, SkeletonTrackingFlags.AppChoosesSkeletons), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.Color, ImageResolution.SixHundredAndFortyByFourHundredAndEighty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, ImageResolution.ThreeHundredAndTwentyByTwoHundredAndForty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.Once());
    sensor.Verify(x => x.NuiShutdown(), Times.Once());
    sensor.VerifyNoOtherCalls();
    coordinateMapper.VerifyNoOtherCalls();
  }

  [TestMethod]
  public void SuccessfulWithoutHavingASingleFrameComeThrough()
  {
    var faceTrackLib = new Mock<IFaceTrackLib>();
    var kernel32 = new Mock<IKernel32>();
    var kinect10 = new Mock<IKinect10>();
    var sensor = new Mock<ISensor>();
    var coordinateMapper = new Mock<ICoordinateMapper>();

    var sensorReference = sensor.Object;
    kinect10.Setup(x => x.NuiCreateSensorById(It.IsAny<string>(), out sensorReference)).Returns(0);
    sensor.Setup(x => x.NuiInitialize(It.IsAny<InitializeFlags>())).Returns(0);
    sensor.Setup(x => x.NuiCameraElevationSetAngle(It.IsAny<int>())).Returns(0);
    var coordinateMapperReference = coordinateMapper.Object;
    sensor.Setup(x => x.NuiGetCoordinateMapper(out coordinateMapperReference)).Returns(0);
    sensor.Setup(x => x.NuiSkeletonTrackingEnable(It.IsAny<IntPtr>(), It.IsAny<SkeletonTrackingFlags>())).Returns(0);
    var colorStreamReference = new IntPtr(2313);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.Color, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out colorStreamReference)).Returns(0);
    var depthStreamReference = new IntPtr(644422);
    sensor.Setup(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, It.IsAny<ImageResolution>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<IntPtr>(), out depthStreamReference)).Returns(0);
    var skeletonFrame = default(SkeletonFrame);
    sensor.Setup(x => x.NuiSkeletonGetNextFrame(It.IsAny<uint>(), ref skeletonFrame)).Returns(1234983);

    using (new Kinect(faceTrackLib.Object, kernel32.Object, kinect10.Object, "Test Unique Identifier", -14))
    {
      Thread.Sleep(5000);
    }
    Thread.Sleep(5000);

    faceTrackLib.VerifyNoOtherCalls();
    kernel32.VerifyNoOtherCalls();
    kinect10.Verify(x => x.NuiCreateSensorById("Test Unique Identifier", out It.Ref<ISensor?>.IsAny), Times.Once());
    kinect10.VerifyNoOtherCalls();
    sensor.Verify(x => x.NuiInitialize(InitializeFlags.UseSkeletalTracking | InitializeFlags.UseColor | InitializeFlags.UseDepthAndPlayerIndex), Times.Once());
    sensor.Verify(x => x.NuiCameraElevationSetAngle(-14), Times.AtMostOnce());
    sensor.Verify(x => x.NuiGetCoordinateMapper(out It.Ref<ICoordinateMapper?>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiSkeletonTrackingEnable(IntPtr.Zero, SkeletonTrackingFlags.AppChoosesSkeletons), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.Color, ImageResolution.SixHundredAndFortyByFourHundredAndEighty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.AtMostOnce());
    sensor.Verify(x => x.NuiImageStreamOpen(ImageType.DepthAndPlayerIndex, ImageResolution.ThreeHundredAndTwentyByTwoHundredAndForty, 0, 2, IntPtr.Zero, out It.Ref<IntPtr>.IsAny), Times.Once());
    sensor.Verify(x => x.NuiShutdown(), Times.Once());
    sensor.Verify(x => x.NuiSkeletonGetNextFrame(0, ref It.Ref<SkeletonFrame>.IsAny), Times.AtLeastOnce());
    sensor.VerifyNoOtherCalls();
    coordinateMapper.VerifyNoOtherCalls();
  }

  [TestMethod, Ignore]
  public void SuccessfulWithOneEmptyFrame()
  {
    throw new NotImplementedException();
  }

  [TestMethod, Ignore]
  public void SuccessfulWithOneNonEmptyFrame()
  {
    throw new NotImplementedException();
  }

  [TestMethod, Ignore]
  public void SuccessfulWithTwoEmptyFrames()
  {
    throw new NotImplementedException();
  }

  [TestMethod, Ignore]
  public void SuccessfulWithTwoFramesWhereSecondAddsPlayer()
  {
    throw new NotImplementedException();
  }

  [TestMethod, Ignore]
  public void SuccessfulWithTwoFramesWhereSecondReplacesPlayer()
  {
    throw new NotImplementedException();
  }

  [TestMethod, Ignore]
  public void SuccessfulWithTwoFramesWhereSecondReordersPlayers()
  {
    throw new NotImplementedException();
  }

  [TestMethod, Ignore]
  public void SuccessfulWithTwoFramesWhereSecondRemovesPlayer()
  {
    throw new NotImplementedException();
  }
}
