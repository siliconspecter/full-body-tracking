using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// The most recent information regarding a <see cref="Player{TFrameNumber}"/> being tracked.
  /// </summary>
  /// <typeparam name="TFrameNumber">A value which changes each time the <see cref="Player{TFrameNumber}"/> is refreshed.</typeparam>
  public struct Player<TFrameNumber>
  {
    /// <summary>
    /// This value will change each time the <see cref="Player{TFrameNumber}"/> is refreshed.
    /// </summary>
    public TFrameNumber FrameNumber;

    /// <summary>
    /// The approximate position of the <see cref="Player{TFrameNumber}"/>, in camera space.
    /// </summary>
    public Vector3 ApproximatePosition;

    /// <summary>
    /// When known, details regarding the <see cref="Player{TFrameNumber}"/>, otherwise, <see langword="null"/>.
    /// </summary>
    public PlayerDetails? Details;
  }
}
