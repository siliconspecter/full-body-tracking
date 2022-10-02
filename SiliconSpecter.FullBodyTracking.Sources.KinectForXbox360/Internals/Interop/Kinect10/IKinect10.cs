namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10;

/// <summary>
/// Exposes native methods in Kinect10.dll.
/// </summary>
public interface IKinect10
{
    /// <summary>
    /// Attempts to create an instance of a <see cref="ISensor"/> using its unique identifier.
    /// </summary>
    /// <param name="uniqueIdentifier">The unique identifier of the <see cref="ISensor"/> to create an instance of.</param>
    /// <param name="sensor">The created <see cref="ISensor"/> instance, if any, otherwise, <see langword="null"/></param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    int NuiCreateSensorById(string uniqueIdentifier, out ISensor? sensor);
}
