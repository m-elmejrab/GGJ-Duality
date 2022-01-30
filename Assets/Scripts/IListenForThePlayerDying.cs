
/// <summary>
/// A class that can be notified about the player's demise.
/// </summary>
public interface IListenForThePlayerDying
{
    /// <summary>
    /// Call this to notify this class that the player is dead.
    /// </summary>
    void ThePlayerIsDead();
}