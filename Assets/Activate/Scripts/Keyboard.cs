using UnityEngine;

public class Keyboard : MonoBehaviour, IPassiveKeyListener
{
	public KeyboardLayout KeyboardLayout;
	
	public void OnKeyDown(GameObject source, string signal)
	{
		
	}

	public void OnValidate()
	{
		Debug.Log("ON VALIDATE!");
	}
}