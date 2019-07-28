using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>KeyboardAdapter</c> demonstrates how the output of a <see cref="Keyboard"/> can be accessed by implementing <see cref="IKeyboardListener"/>.
/// </summary>
public class KeyboardAdapter : MonoBehaviour, IKeyboardListener
{
    Text text;
    
    void Awake()
    {
        //Cache components
        text = GetComponent<Text>();
    }

    public void OnLetterWritten(string letter)
    {
        switch (letter)
        {
            case "backspace":
                text.text = text.text.Substring(0, text.text.Length - 1);
                break;
            case "enter":
                text.text = "";
                break;
            default:
                text.text += letter;
                break;
        }
    }
}