using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Converts <see cref="Player"/>s to <see cref="InterpolatablePlayerKeyframe"/>s.
  /// </summary>
  public interface IPlayerToKeyframeConverter
  {
    /// <summary>
    /// Converts a given <see cref="Player"/> to an equivalent <see cref="InterpolatablePlayerKeyframe"/>.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to convert to a <see cref="InterpolatablePlayerKeyframe"/>.</param>
    /// <param name="previousKeyframe">The previous <see cref="InterpolatablePlayerKeyframe"/>, if any, otherwise, <see langword="null"/>.</param>
    /// <param name="metrics">The <see cref="Metrics"/> of the actor.</param>
    /// <param name="cameraPosition">The position of the camera, in meters.</param>
    /// <param name="cameraRotation">The rotation of the camera.</param>
    /// <returns>The <see cref="InterpolatablePlayerKeyframe"/> converted from the given <paramref name="player"/>.</returns>
    InterpolatablePlayerKeyframe Convert(Player player, InterpolatablePlayerKeyframe? previousKeyframe, Metrics metrics, Vector3 cameraPosition, Quaternion cameraRotation);
  }
}
