using UnityEngine;

/// <summary>
/// Interface <c>IPassiveKeyListener</c> is used for <see cref="Key"/> events.
/// Implement this and add the game object to <see cref="Key.KeyListeners"/> to receive events.
/// </summary>
public interface IPassiveKeyListener
{
    /// <summary>
    /// Will fire when a key has been pressed down.
    /// </summary>
    /// <param name="source">The game object the event was fired from.</param>
    /// <param name="signal">A string representing the key that was pressed down.</param>
    void OnKeyDown(GameObject source, string signal);
}