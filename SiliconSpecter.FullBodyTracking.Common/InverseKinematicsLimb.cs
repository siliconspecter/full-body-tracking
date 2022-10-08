using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Details regarding a limb of a posed character model.
  /// </summary>
  public struct InverseKinematicsLimb
  {
    /// <summary>
    /// The rotation of upper arm or leg, in world space.
    /// </summary>
    public Quaternion Proximal;

    /// <summary>
    /// The rotation of lower arm or leg, in world space.
    /// </summary>
    public Quaternion Intermediate;

    /// <summary>
    /// The rotation of hand or foot, in world space.
    /// </summary>
    public Quaternion Distal;
  }
}
