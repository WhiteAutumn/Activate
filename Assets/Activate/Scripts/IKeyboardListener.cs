/// <summary>
/// Interface <c>IKeyboardListener</c> is used for <see cref="Keyboard"/> events.
/// Implement this and add the game object to <see cref="Keyboard.KeyboardListeners"/> to receive events.
/// </summary>
public interface IKeyboardListener
{
    /// <summary>
    /// Will fire when a <see cref="Key"/> attached to a <see cref="Keyboard"/> has been pressed.
    /// </summary>
    /// <param name="letter">A string representing the key pressed.</param>
    void OnLetterWritten(string letter);
}