namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10;

/// <summary>
/// The type of an image.
/// </summary>
public enum ImageType
{
    /// <summary>
    /// Each pixel of the image contains a depth coordinate and a player identifier.
    /// </summary>
    DepthAndPlayerIndex,

    /// <summary>
    /// Each pixel of the image contains a RGB color.
    /// </summary>
    Color,

    /// <summary>
    /// Each pixel of the image contains a YUV color.
    /// </summary>
    ColorYuv,

    /// <summary>
    /// Each pixel of the image contains an unprocessed YUV color.
    /// </summary>
    ColorRawYuv,

    /// <summary>
    /// Each pixel of the image contains a depth coordinate.
    /// </summary>
    Depth,

    /// <summary>
    /// Each pixel of the image contains an infrared intensity.
    /// </summary>
    Infrared,

    /// <summary>
    /// Each pixel of the image contains a raw Bayer sample.
    /// </summary>
    ColorRawBayer,
}
