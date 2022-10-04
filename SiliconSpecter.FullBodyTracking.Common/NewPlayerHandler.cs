namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Handles a motion capture system starting to track a new player.
  /// </summary>
  /// <typeparam name="TPlayerIdentifier">An identifier for a player.</typeparam>
  /// <param name="playerIdentifier">The identifier of the player now being tracked.</param>
  public delegate void NewPlayerHandler<TPlayerIdentifier>(TPlayerIdentifier playerIdentifier);
}
