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
    /// <seealso cref="IActiveKeyListener"/> <seealso cref="IPassiveKeyListener"/>
    /// </summary>
    public List<GameObject> KeyListeners;
    
    Transform keycapTransform;
    List<IPassiveKeyListener> passiveListeners;
    List<IActiveKeyListener> activeKeyListeners;
    bool pushable = true;

    void Awake()
    {
        //Find and cache keycap transform
        foreach (Transform child in transform)
        {
            Keycap keycap = child.GetComponent<Keycap>();
            if (keycap != null)
            {
                keycapTransform = keycap.transform;
            }
        }
        
        UpdateListeners();
    }

    /// <summary>
    /// Finds and adds any listeners in <see cref="KeyListeners"/>.
    /// </summary>
    public void UpdateListeners()
    {
        //Clear previous listeners
        passiveListeners = new List<IPassiveKeyListener>();
        activeKeyListeners = new List<IActiveKeyListener>();
        
        foreach (var listener in KeyListeners)
        {
            if (listener == null)
                continue;
            
            //Find and add new listeners
            foreach (var component in listener.GetComponents<Component>())
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
    
    public void Update()
    {
        //Check if key has been pressed
        if (pushable && keycapTransform.localPosition.y < -ActivationThreshold)
        {
            pushable = false;
            
            //Notify all listeners
            foreach (var listener in passiveListeners)
            {
                listener.OnKeyDown(gameObject, Signal);
            }
        }
        else if (!pushable && keycapTransform.localPosition.y > -ActivationThreshold)
        {
            pushable = true;
        }
    }

    public void FixedUpdate()
    {
        //Notify all listeners
        foreach (var listener in activeKeyListeners)
        {
            listener.OnKeyUpdate(gameObject, Signal, -keycapTransform.localPosition.y);
        }
    }
}