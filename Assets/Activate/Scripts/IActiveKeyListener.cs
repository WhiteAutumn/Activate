using UnityEngine;

/// <summary>
/// Listener for <see cref="Key"/> events.
/// Implement this and add the game object to <see cref="Key.KeyListeners"/> to receive events.
/// </summary>
public interface IActiveKeyListener
{
    /// <summary>
    /// Will fire every <c>FixedUpdate()</c>
    /// </summary>
    /// <param name="source">The game object the event was fired from.</param>
    /// <param name="signal">A string representing the key that was pressed down.</param>
    /// <param name="value">A float representing how much the key is pressed down.</param>
    void OnKeyUpdate(GameObject source, string signal, float value);
}