﻿using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Details regarding the extension of a <see cref="Player{TFrameNumber}"/>'s <see cref="Limb"/>.
  /// </summary>
  public struct LimbExtension
  {
    /// <summary>
    /// The position of the arm's elbow joint/leg's knee joint, in camera space, if known, otherwise, <see langword="null"/>.
    /// </summary>
    public Vector3? IntermediatePosition;

    /// <summary>
    /// The position of the arm's wrist joint/leg's ankle joint, in camera space.
    /// </summary>
    public Vector3 DistalPosition;

    /// <summary>
    /// The position of the arm's middle fingertip/foot's middle toetip, in camera space, if known, otherwise, <see langword="null"/>.
    /// </summary>
    public Vector3? TipPosition;
  }
}
