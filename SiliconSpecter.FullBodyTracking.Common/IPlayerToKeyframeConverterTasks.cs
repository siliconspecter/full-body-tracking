using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Refactored sections of <see cref="IPlayerToKeyframeConverter{TFrameNumber}"/>.
  /// </summary>
  public interface IPlayerToKeyframeConverterTasks
  {
    /// <summary>
    /// Calculates the rotation of a head, in character-local space.
    /// </summary>
    /// <param name="facingRotation">
    /// A <see cref="Quaternion"/> which transforms positions from world space
    /// to character-local space.
    /// </param>
    /// <param name="inverseFacingRotation">
    /// A <see cref="Quaternion"/> which transforms positions from
    /// character-local space to world space.
    /// </param>
    /// <param name="headRotation">
    /// The previous rotation of the head in character-local space.
    /// </param>
    /// <param name="headForwardNormal">
    /// The normal from the back to the front of the player's head, in camera
    /// space, if known, otherwise, <see langword="null"/>.
    /// </param>
    /// <param name="headUpNormal">
    /// The normal from the bottom to the top of the head, in camera space, if
    /// known, otherwise, <see langword="null"/>.
    /// </param>
    /// <param name="cameraRotation">
    /// A <see cref="Quaternion"/> which transforms positions from camera space
    /// to world space.
    /// </param>
    /// <param name="cameraToInverseFacingRotation">A <see cref="Quaternion"/>
    /// which transforms positions from camera space to character-local space.
    /// </param>
    /// <returns>
    /// A <see cref="Quaternion"/> describing the rotation of the head, in
    /// character-local space.
    /// </returns>
    Quaternion ConvertHead(Quaternion facingRotation, Quaternion inverseFacingRotation, Quaternion headRotation, Vector3? headForwardNormal, Vector3? headUpNormal, Quaternion cameraRotation, Quaternion cameraToInverseFacingRotation);

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
    InterpolatablePlayerKeyframeLimb ConvertLimb(Limb limb, InterpolatablePlayerKeyframeLimb previousKeyframe, Vector3 fallbackBendNormal, float lockedWhenIntermediateDistanceLessThan, float length, Quaternion cameraToInverseFacingRotation);
  }
}
