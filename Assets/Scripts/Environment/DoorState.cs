using UnityEngine;
using System;

[Serializable]
public enum DoorState
{
    /// <summary>
    /// Door is closed.
    /// </summary>
    CLOSED,
    /// <summary>
    /// Door is opening, but not fully open yet.
    /// </summary>
    OPENING,
    /// <summary>
    /// Door is fully open.
    /// </summary>
    OPEN,
    /// <summary>
    /// Door is closing, but not fully closed yet.
    /// </summary>
    CLOSING

}

