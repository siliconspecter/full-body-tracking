using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Details regarding a <see cref="Player"/>'s arm or leg.
  /// </summary>
  public struct Limb
  {
    /// <summary>
    /// The position of the arm's shoulder joint/leg's hip joint, in camera space.
    /// </summary>
    public Vector3 ProximalPosition;

    /// <summary>
    /// Details regarding the <see cref="Limb"/>'s extension, if known, otherwise, <see langword="null"/>.
    /// </summary>
    public LimbExtension? Extension;
  }
}
