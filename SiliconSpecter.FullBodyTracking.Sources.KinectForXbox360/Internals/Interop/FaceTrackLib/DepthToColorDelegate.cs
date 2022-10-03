namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.FaceTrackLib;

/// <summary>
/// A callback given to the face tracking library which it can use to map depth image coordinates to color image coordinates.
/// </summary>
/// <param name="depthFrameWidth">The width of the depth image, in pixel columns.</param>
/// <param name="depthFrameHeight">The height of the depth image, in pixel rows.</param>
/// <param name="colorFrameWidth">The width of the color image, in pixel columns.</param>
/// <param name="colorFrameHeight">The height of the color image, in pixel rows.</param>
/// <param name="zoomFactor">The zoom factor; always one.</param>
/// <param name="viewOffset">The view offset; always zero.</param>
/// <param name="depthX">The depth coordinate on the X axis.</param>
/// <param name="depthY">The depth coordinate on the Y axis.</param>
/// <param name="depthZ">The depth coordinate on the Z axis.</param>
/// <param name="colorX">The color coordinate on the X axis.</param>
/// <param name="colorY">The color coordinate on the Y axis.</param>
/// <returns></returns>
public delegate int DepthToColorDelegate(
    uint depthFrameWidth,
    uint depthFrameHeight,
    uint colorFrameWidth,
    uint colorFrameHeight,
    float zoomFactor,
    Point viewOffset,
    int depthX,
    int depthY,
    ushort depthZ,
    out int colorX,
    out int colorY);
