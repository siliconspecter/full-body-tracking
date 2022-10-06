using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Details regarding a limb of a posed character model.
  /// </summary>
  public struct InverseKinematicsLimb
  {
    /// <summary>
    /// The rotation of upper arm or leg relative to the shoulders or hips.
    /// </summary>
    public Quaternion Proximal;

    /// <summary>
    /// The rotation of lower arm or leg relative to the upper arm or leg.
    /// </summary>
    public Quaternion Intermediate;

    /// <summary>
    /// The rotation of hand or foot relative to the lower arm or leg.
    /// </summary>
    public Quaternion Distal;
  }
}
