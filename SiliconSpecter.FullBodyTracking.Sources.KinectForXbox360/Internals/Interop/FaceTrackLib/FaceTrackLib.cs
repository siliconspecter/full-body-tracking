using System;
using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.FaceTrackLib
{
  /// <inheritdoc />
  public sealed class FaceTrackLib : IFaceTrackLib
  {
    [DllImport("FaceTrackLib.dll", EntryPoint = "FTCreateFaceTracker")]
    private static extern IFaceTracker FTCreateFaceTrackerImport(IntPtr reserved);

    /// <inheritdoc />
    public IFaceTracker FTCreateFaceTracker(IntPtr reserved)
    {
      return FTCreateFaceTrackerImport(reserved);
    }

    [DllImport("FaceTrackLib.dll", EntryPoint = "FTCreateImage")]
    private static extern IImage FTCreateImageImport();

    /// <inheritdoc />
    public IImage FTCreateImage()
    {
      return FTCreateImageImport();
    }
  }
}
