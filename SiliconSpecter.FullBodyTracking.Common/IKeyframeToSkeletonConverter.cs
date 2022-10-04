using System;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Converts <see cref="InterpolatablePlayerKeyframe"/>s to <see cref="Skeleton"/>s.
  /// </summary>
  public interface IKeyframeToSkeletonConverter
  {
    /// <summary>
    /// Converts a given <see cref="InterpolatablePlayerKeyframe"/> to an equivalent <see cref="Skeleton"/> given a bind pose.
    /// </summary>
    /// <param name="keyframe">The <see cref="InterpolatablePlayerKeyframe"/> to convert to a <see cref="Skeleton"/>.</param>
    /// <param name="bindPose">The <see cref="Skeleton"/> of the bind pose.</param>
    /// <returns>The <see cref="Skeleton"/> converted from the given <paramref name="keyframe"/>.</returns>
    Skeleton Convert(InterpolatablePlayerKeyframe keyframe, Skeleton bindPose);
  }
}
