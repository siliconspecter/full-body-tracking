using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// A bone within a <see cref="Skeleton"/>.
  /// </summary>
  public struct Bone
  {
    /// <summary>
    /// The position of the bone, in meters, in world space.
    /// </summary>
    public Vector3 Position;

    /// <summary>
    /// The rotation of the bone, in world space.
    /// </summary>
    public Quaternion Rotation;
  }
}