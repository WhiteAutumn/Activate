﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A demonstration class of how <see cref="IKeyboardListener"/> could be implemented.
/// </summary>
public class KeyboardAdapter : MonoBehaviour, IKeyboardListener
{
    Text text;
    
    void Awake()
    {
        text = GetComponent<Text>();
    }

    public void OnLetterWritten(string letter)
    {
        if (letter == "backspace")
        {
            text.text = text.text.Substring(0, text.text.Length - 1);
        }
        else if (letter == "enter")
        {
            text.text = "";
        }
        else
        {
            text.text += letter;
        }
    }
}