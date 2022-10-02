using SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.FaceTrackLib;

/// <summary>
/// The locations of the
/// </summary>
public struct HeadPoints
{
    /// <summary>
    /// The position of the <see cref="Joint.Neck"/> <see cref="Joint"/> on the X axis; zero is in the center, positive values are to the left, and negative values are to the right, all in meters.
    /// </summary>
    public float NeckX;

    /// <summary>
    /// The position of the <see cref="Joint.Neck"/> <see cref="Joint"/> on the Y axis; zero is in the center, positive values are to the top, and negative values are to the bottom, all in meters.
    /// </summary>
    public float NeckY;

    /// <summary>
    /// The position of the <see cref="Joint.Neck"/> <see cref="Joint"/> on the Z axis; zero is in the sensor, positive values are distant, and negative values are near, all in meters.
    /// </summary>
    public float NeckZ;

    /// <summary>
    /// The position of the <see cref="Joint.TopOfHead"/> <see cref="Joint"/> on the X axis; zero is in the center, positive values are to the left, and negative values are to the right, all in meters.
    /// </summary>
    public float TopOfHeadX;

    /// <summary>
    /// The position of the <see cref="Joint.TopOfHead"/> <see cref="Joint"/> on the Y axis; zero is in the center, positive values are to the top, and negative values are to the bottom, all in meters.
    /// </summary>
    public float TopOfHeadY;

    /// <summary>
    /// The position of the <see cref="Joint.TopOfHead"/> <see cref="Joint"/> on the Z axis; zero is in the sensor, positive values are distant, and negative values are near, all in meters.
    /// </summary>
    public float TopOfHeadZ;
}
