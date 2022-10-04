using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// A limb within a keyframe of a player's data which can be interpolated.
  /// </summary>
  public struct InterpolatablePlayerKeyframeLimb
  {
    /// <summary>
    /// A vector representing the position of the limb, pointing in the direction the limb is extended, relative to the facing rotation, where a unit vector is fully extended and a zero vector is fully folded back to the proximal.
    /// </summary>
    public Vector3 Extension;

    /// <summary>
    /// A normal pointing in the direction the limb is bent (e.g. from the midpoint between the shoulder and wrist to the elbow), relative to the facing rotation.
    /// </summary>
    public Vector3 BendNormal;

    /// <summary>
    /// A normal pointing in the direction the tip points (e.g. from the wrist to the tip of the middle finger), relative to the facing rotation.
    /// </summary>
    public Vector3 TipNormal;
  }
}
