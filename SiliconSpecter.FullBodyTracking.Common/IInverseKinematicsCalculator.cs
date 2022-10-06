namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Calculates <see cref="InverseKinematics"/> from given <see cref="InterpolatablePlayerKeyframe"/>s.
  /// </summary>
  public interface IInverseKinematicsCalculator
  {
    /// <summary>
    /// Calculates a set of <see cref="InverseKinematics"/> from a given <see cref="InterpolatablePlayerKeyframe"/>.
    /// </summary>
    /// <param name="keyframe">The <see cref="InterpolatablePlayerKeyframe"/> from which to calculate <see cref="InverseKinematics"/>.</param>
    /// <param name="bindPose">The character's <see cref="BindPose"/>.</param>
    /// <returns>The <see cref="InverseKinematics"/> calculated from the given <paramref name="keyframe"/>.</returns>
    InverseKinematics Convert(InterpolatablePlayerKeyframe keyframe, BindPose bindPose);
  }
}
