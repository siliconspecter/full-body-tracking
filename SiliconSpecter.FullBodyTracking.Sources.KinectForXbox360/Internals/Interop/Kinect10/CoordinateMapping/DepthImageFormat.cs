namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10.CoordinateMapping
{
  /// <summary>
  /// The format of a depth image stream.
  /// </summary>
  public enum DepthImageFormat
  {
    /// <summary>
    /// 640x480, 30Hz.
    /// </summary>
    SixHundredAndFortyByFourHundredAndHeightAtThirtyFramesPerSecond = 1,

    /// <summary>
    /// 320x240, 30Hz.
    /// </summary>
    ThreeHundredAndTwentyByTwoHundredAndFortyAtThirtyFramesPerSecond = 2,

    /// <summary>
    /// 80x60, 30Hz.
    /// </summary>
    EightyBySixtyAtThirtyFramesPerSecond = 3,
  }
}
