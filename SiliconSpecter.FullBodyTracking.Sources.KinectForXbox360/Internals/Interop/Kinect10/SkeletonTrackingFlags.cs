using System;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10
{
  /// <summary>
  /// Settings which can be applied while configuring skeleton tracking.
  /// </summary>
  [Flags]
  public enum SkeletonTrackingFlags : uint
  {
    /// <summary>
    /// When set, the Kinect will block the thread until data is available.  Otherwise, it will return immediately without any new data if none is immediately available..
    /// </summary>
    BlockUntilData = 1,

    /// <summary>
    /// When set, the Kinect will allow the application to specify exactly which skeletons are be tracked in detail.
    /// </summary>
    AppChoosesSkeletons = 2,

    /// <summary>
    /// When set, tracking will be optimized for players who are sitting down.
    /// </summary>
    Seated = 4,

    /// <summary>
    /// When set, tracking will be optimized for players who are close to the Kinect.
    /// </summary>
    NearRange = 8,
  }
}
