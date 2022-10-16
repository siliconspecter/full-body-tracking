using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Calculates <see cref="InverseKinematicsLimb"/>s from given
  /// <see cref="InterpolatablePlayerKeyframeLimb"/>s.
  /// </summary>
  public interface ILimbInverseKinematicsCalculator
  {
    /// <summary>
    /// Calculates a <see cref="InverseKinematicsLimb"/> from a given
    /// <see cref="InterpolatablePlayerKeyframeLimb"/>.
    /// </summary>
    /// <param name="facingRotation">
    /// An approximate facing rotation of the overall player, in world space.
    /// </param>
    /// <param name="keyframeLimb">
    /// The <see cref="InterpolatablePlayerKeyframeLimb"/> from which to
    /// calculate a <see cref="InverseKinematicsLimb"/>.
    /// </param>
    /// <param name="bindPoseLimb">
    /// The character's corresponding <see cref="BindPoseLimb"/>.
    /// </param>
    /// <param name="roll">The roll to apply to the bones, in radians.</param>
    /// <returns>
    /// The <see cref="InverseKinematicsLimb"/> calculated from the given
    /// <paramref name="keyframe"/>.
    /// </returns>
    InverseKinematicsLimb Calculate
    (
      Quaternion facingRotation,
      InterpolatablePlayerKeyframeLimb keyframeLimb,
      BindPoseLimb bindPoseLimb,
      float roll
    );
  }
}
