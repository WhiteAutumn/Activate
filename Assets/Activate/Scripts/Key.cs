using System;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
	public float ActivationThreshold = 0.05f;
	public string Signal;
	public List<GameObject> KeyListeners;
	
	List<IPassiveKeyListener> passiveListeners;
	List<IActiveKeyListener> activeKeyListeners;
	Transform keyTransform;
	bool toggleable = true;

	void OnValidate()
	{
		passiveListeners = new List<IPassiveKeyListener>();
		activeKeyListeners = new List<IActiveKeyListener>();
		
		foreach (Transform child in transform)
		{
			if (child.CompareTag("keycap"))
			{
				keyTransform = child;
			}
		}

		foreach (GameObject listener in KeyListeners)
		{
			if (listener != null)
			{
				foreach (Component component in listener.GetComponents<Component>())
				{
					if (component is IPassiveKeyListener passiveListener)
					{
						passiveListeners.Add(passiveListener);
					}
					if (component is IActiveKeyListener activeListener)
					{
						activeKeyListeners.Add(activeListener);
					}
				}
			}
		}
	}

	public void Update()
	{
		if (toggleable && keyTransform.localPosition.y < -ActivationThreshold)
		{
			toggleable = false;
			
			foreach (IPassiveKeyListener listener in passiveListeners)
			{
				listener.OnKeyDown(gameObject, Signal);
			}
		}
		else if (!toggleable && keyTransform.localPosition.y > -ActivationThreshold)
		{
			toggleable = true;
		}
	}

	public void FixedUpdate()
	{
		foreach (IActiveKeyListener listener in activeKeyListeners)
		{
			listener.OnKeyUpdate(gameObject, Signal, -keyTransform.localPosition.y);
		}
	}
}