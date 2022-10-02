using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.FaceTrackLib;

/// <summary>
/// A tracking session.
/// </summary>
[ComImport, Guid("1A00A7BB-C217-11E0-AC90-0024811441FD"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IResult
{
    /// <summary>
    /// Unused.
    /// </summary>
    [Obsolete]
    public void Reset();

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="destination">Unused.</param>
    /// <returns>Unused.</returns>
    [Obsolete]
    [PreserveSig]
    public int CopyTo(IResult destination);

    /// <summary>
    /// Retrieves the status of face tracking.
    /// </summary>
    /// <returns>When zero, face tracking has either established or continued to hold a lock.  When non-zero, face tracking has failed to either establish or hold a lock.</returns>
    [PreserveSig]
    public int GetStatus();

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="rectangle">Unused.</param>
    [Obsolete]
    public void GetFaceRect(out IntPtr rectangle);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="points">Unused.</param>
    /// <param name="pointCount">Unused.</param>
    [Obsolete]
    public void Get2DShapePoints(out IntPtr points, out uint pointCount);

    /// <summary>
    /// Retrieves the position of the face.
    /// </summary>
    /// <param name="scale">Unused.</param>
    /// <param name="rotation">A <see cref="FaceTrackingVector3"/> describing the rotation of the face.</param>
    /// <param name="translation">Unused.</param>
    public void Get3DPose(out float scale, out FaceTrackingVector3 rotation, out FaceTrackingVector3 translation);

    /// <summary>Retrieves the animation unit data.</summary>
    /// <param name="animationUnitCoefficients">A pointer to the animation unit data.  Each is a <see cref="float"/>.</param>
    /// <param name="animationUnitCoefficientCount">The number of animation units.  Should always be <see cref="AnimationUnit.Count"/>.</param>
    public void GetAUCoefficients(out IntPtr animationUnitCoefficients, out uint animationUnitCoefficientCount);
}
