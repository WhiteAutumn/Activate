using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Key</c> monitors the position of keycap game object and registers key strokes.
/// </summary>
public class Key : MonoBehaviour
{
    /// <summary>
    /// Describes how much a key needs to be pressed down to activate.
    /// </summary>
    public float ActivationThreshold = 0.05f;
    /// <summary>
    /// The signal that will be sent with key events.
    /// </summary>
    public string Signal;
    /// <summary>
    /// A list of game objects that will be checked for key listeners.
    /// </summary>
    public List<GameObject> KeyListeners;
    
    List<IPassiveKeyListener> passiveListeners;
    List<IActiveKeyListener> activeKeyListeners;
    Transform keyTransform;
    bool toggleable = true;

    //TODO: Replace with dedicated method triggered by awake() and custom editor
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
        //Check if key has been pressed
        if (toggleable && keyTransform.localPosition.y < -ActivationThreshold)
        {
            toggleable = false;
            
            //Notify all listeners
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
        //Notify all listeners
        foreach (IActiveKeyListener listener in activeKeyListeners)
        {
            listener.OnKeyUpdate(gameObject, Signal, -keyTransform.localPosition.y);
        }
    }
}