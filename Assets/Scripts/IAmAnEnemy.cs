using UnityEngine;
/// <summary>
/// Marker interface for enemies.
/// Please make sure enemies implement this interface.
/// </summary>
public interface IAmAnEnemy
{
    /// <summary>
    /// Obtain the GameObject of this enemy.
    /// </summary>
    /// <returns>the gameobject</returns>
    public GameObject GetGameObject();
}


