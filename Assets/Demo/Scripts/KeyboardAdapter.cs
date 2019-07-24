using UnityEngine;
using UnityEngine.UI;

public class KeyboardAdapter : MonoBehaviour, IKeyboardListener
{
	Text text;
	
	void Awake()
	{
		text = GetComponent<Text>();
	}

	public void OnLetterWritten(string letter)
	{
		text.text += letter;
	}
}