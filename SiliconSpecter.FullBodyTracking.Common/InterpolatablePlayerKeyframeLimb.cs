using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// A limb within a keyframe of a player's data which can be interpolated.
  /// </summary>
  public struct InterpolatablePlayerKeyframeLimb
  {
    /// <summary>
    /// A normal pointing in the direction the limb is extended, relative to the facing rotation.
    /// </summary>
    public Vector3 ExtensionNormal;

    /// <summary>
    /// The amount the limb is extended, where 0 is not at all (a hand might be inside a shoulder for example) and 1 is fully extended.
    /// </summary>
    public float ExtensionProportion;

    /// <summary>
    /// A normal pointing in the direction the limb is bent (e.g. from the midpoint between the shoulder and wrist to the elbow).
    /// </summary>
    public Vector3 BendNormal;

    /// <summary>
    /// A normal pointing in the direction the tip points (e.g. from the wrist to the tip of the middle finger).
    /// </summary>
    public Vector3 TipNormal;
  }
}
