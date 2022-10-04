using System;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Represents a full-body tracking system.
  /// </summary>
  /// <remarks>Implementations must be thread-safe</remarks>
  /// <typeparam name="TPlayerIdentifier">An identifier for a player.</typeparam>
  public interface ISource<TPlayerIdentifier> : IDisposable
  {
    /// <summary>
    /// Raised when the motion capture starts to track a new player.
    /// </summary>
    event NewPlayerHandler<TPlayerIdentifier> NewPlayer;

    /// <summary>
    /// Informs the motion capture system that it should start tracking a specific player in detail if possible.
    /// </summary>
    /// <remarks>No effect if the given <paramref name="playerIdentifier"/> is already being focused upon or either never has or no longer refers to a known player.</remarks>
    /// <param name="playerIdentifier">The identifier of the player to track in detail.</param>
    /// <exception cref="Exception">Thrown when <see langword="this"/> <see cref="ISource{TPlayerIdentifier}"/> has been <see cref="IDisposable.Dispose()"/>d.</exception>
    void TrackInDetail(TPlayerIdentifier playerIdentifier);

    /// <summary>
    /// Informs the motion capture system that it should stop tracking a specific player in detail if possible.
    /// </summary>
    /// <remarks>No effect if the given <paramref name="playerIdentifier"/> is not already being focused upon or either never has or no longer refers to a known player.</remarks>
    /// <param name="playerIdentifier">The identifier of the player to no longer track in detail.</param>
    /// <exception cref="Exception">Thrown when <see langword="this"/> <see cref="ISource{TPlayerIdentifier}"/> has been <see cref="IDisposable.Dispose()"/>d.</exception>
    void StopTrackingInDetail(TPlayerIdentifier playerIdentifier);

    /// <summary>
    /// Retrieves the most recent information regarding a player being tracked.
    /// </summary>
    /// <param name="playerIdentifier">The identifier of the player information is to be retrieved regarding.</param>
    /// <returns>When the player is still being tracked, the most recent information regarding them.  Otherwise, <see langword="null"/>.</returns>
    /// <exception cref="Exception">Thrown when <see langword="this"/> <see cref="ISource{TPlayerIdentifier}"/> has been <see cref="IDisposable.Dispose()"/>d.</exception>
    Player? Get(TPlayerIdentifier playerIdentifier);

    /// <summary>
    /// The number of tracking updates expected per second.
    /// </summary>
    int ExpectedUpdatesPerSecond { get; }
  }
}
