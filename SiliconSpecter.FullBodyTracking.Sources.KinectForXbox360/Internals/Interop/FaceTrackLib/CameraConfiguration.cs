using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.FaceTrackLib;

/// <summary>
/// The parameters of the camera being used for face tracking.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct CameraConfiguration
{
    /// <summary>
    /// The width of the images retrieved from the camera, in pixel columns.
    /// </summary>
    public uint Width;

    /// <summary>
    /// The height of the images retrieved from the camera, in pixel rows.
    /// </summary>
    public uint Height;

    /// <summary>
    /// The focal length of the camera.  When <see cref="Width"/> is 640, this is 531.15.  When <see cref="Width"/> is 320, this is 285.63.
    /// </summary>
    public float FocalLength;
}
