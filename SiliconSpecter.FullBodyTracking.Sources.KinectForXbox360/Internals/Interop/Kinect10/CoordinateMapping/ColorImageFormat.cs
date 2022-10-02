namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10.CoordinateMapping;

/// <summary>
/// The format of a color image stream.
/// </summary>
public enum ColorImageFormat
{
    /// <summary>
    /// Red, green and blue, 640x480, 30Hz.
    /// </summary>
    RedGreenAndBlueAtSixHundredAndFortyByFourHundredAndEightyAtThirtyFramesPerSecond = 1,

    /// <summary>
    /// Red, green and blue, 1280x960, 12Hz.
    /// </summary>
    RedGreenAndBlueAtOneThousandTwoHundredAndEightyByNineHundredAndSixtyAtTwelveFramesPerSecond = 2,

    /// <summary>
    /// Luma, blue and red, 640x480, 15Hz.
    /// </summary>
    LumaBlueRedAtSixHundredAndFortyByFourHundredAndEightyAtFifteenFramesPerSecond = 3,

    /// <summary>
    /// Raw luma, blue and red, 640x480, 15Hz.
    /// </summary>
    RawLumaBlueRedAtSixHundredAndFortyByFourHundredAndEightyAtFifteenFramesPerSecond = 4,

    /// <summary>
    /// Infrared, 640x480, 30Hz.
    /// </summary>
    InfraredAtSixHundredAndFortyByFourHundredAndEightyAtThirtyFramesPerSecond = 5,

    /// <summary>
    /// Raw bayer output, 640x480, 30Hz.
    /// </summary>
    RawBayerAtSixHundredAndFortyByFourHundredAndEightyAtThirtyFramesPerSecond = 6,

    /// <summary>
    /// Raw bayer output, 1280x960, 12Hz.
    /// </summary>
    RawBayerAtOneThousandTwoHundredAndEightyByNineHundredAndSixtyAtTwelveFramesPerSecond = 7,
}
