namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.FaceTrackLib;

/// <summary>
/// Exposes native methods in FaceTrackLib.dll.
/// </summary>
public interface IFaceTrackLib
{
  /// <summary>
  /// Creates a new <see cref="IFaceTracker"/>.
  /// </summary>
  /// <param name="reserved">Unused.  Always <see cref="IntPtr.Zero"/>.</param>
  /// <returns>The created <see cref="IFaceTracker"/>, if any, otherwise, <see langword="null"/>.</returns>
  IFaceTracker FTCreateFaceTracker(IntPtr reserved);

  /// <summary>
  /// Creates a new <see cref="IImage"/>.
  /// </summary>
  /// <returns>The created <see cref="IImage"/>, if any, otherwise, <see langword="null"/>.</returns>
  IImage FTCreateImage();
}
