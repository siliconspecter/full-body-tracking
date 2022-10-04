using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Converts <see cref="Limb"/>s to <see cref="InterpolatablePlayerKeyframeLimb"/>s.
  /// </summary>
  public interface ILimbToInterpolatablePlayerKeyframeLimbConverter
  {
    /// <summary>
    /// Converts a given <see cref="Limb"/> to an equivalent <see cref="InterpolatablePlayerKeyframeLimb"/>.
    /// </summary>
    /// <param name="limb">The <see cref="Limb"/> to convert to a <see cref="InterpolatablePlayerKeyframeLimb"/>.</param>
    /// <param name="previousKeyframe">The previous <see cref="InterpolatablePlayerKeyframeLimb"/>, if any, otherwise, <see langword="null"/>.</param>
    /// <param name="length">The length when fully extended, from <see cref="Metrics"/>.</param>
    /// <param name="defaultBendNormal">The default bend normal to use when no other is available.</param>
    /// <param name="defaultTipNormal">The default tip normal to use when no other is available.</param>
    /// <param name="cameraToInverseFacingRotation">A <see cref="Quaternion"/> which transforms positions from camera space to character-local space.</param>
    /// <returns>The <see cref="InterpolatablePlayerKeyframeLimb"/> converted from the given <paramref name="limb"/>.</returns>
    InterpolatablePlayerKeyframeLimb Convert(Limb limb, InterpolatablePlayerKeyframeLimb? previousKeyframe, float length, Vector3 defaultBendNormal, Vector3 defaultTipNormal, Quaternion cameraToInverseFacingRotation);
  }
}
