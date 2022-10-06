using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Details regarding a <see cref="BindPose"/>'s arm or leg.
  /// </summary>
  public struct BindPoseLimb
  {
    /// <summary>
    /// The position of the arm's shoulder joint/leg's hip joint, in local space.
    /// </summary>
    public Vector3 ProximalPosition;

    /// <summary>
    /// The position of the arm's elbow joint/leg's knee joint, in local space.
    /// </summary>
    public Vector3 IntermediatePosition;

    /// <summary>
    /// The position of the arm's wrist joint/leg's ankle joint, in local space.
    /// </summary>
    public Vector3 DistalPosition;
  }
}
