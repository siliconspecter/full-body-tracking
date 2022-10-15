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
    /// <param name="previousKeyframe">The previous <see cref="InterpolatablePlayerKeyframeLimb"/>.</param>
    /// <param name="fallbackBendNormal">A normal pointing in the direction the limb is to be bent when no new value can be found (e.g. from the midpoint between the shoulder and wrist to the elbow), relative to the facing rotation.</param>
    /// <param name="lockedWhenIntermediateDistanceLessThan">When the intermediate joint is less than this distance from the midpoint of the proximal and distal joints, the limb is considered to be locked straight and the <paramref name="fallbackBendNormal"/> will be used rather than calculating a new one.</param>
    /// <param name="length">The length when fully extended, from <see cref="Metrics"/>.</param>
    /// <param name="cameraToInverseFacingRotation">A <see cref="Quaternion"/> which transforms positions from camera space to character-local space.</param>
    /// <returns>The <see cref="InterpolatablePlayerKeyframeLimb"/> converted from the given <paramref name="limb"/>.</returns>
    InterpolatablePlayerKeyframeLimb Convert(Limb limb, InterpolatablePlayerKeyframeLimb previousKeyframe, Vector3 fallbackBendNormal, float lockedWhenIntermediateDistanceLessThan, float length, Quaternion cameraToInverseFacingRotation);
  }
}
