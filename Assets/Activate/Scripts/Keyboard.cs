using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Class <c>Keyboard</c> manages and initializes keys from the provided <see cref="KeyboardLayout"/>.
/// </summary>
public class Keyboard : MonoBehaviour, IPassiveKeyListener
{
    /// <summary>
    /// The <c>KeyboardLayout</c> that the keyboard should use.
    /// </summary>
    public KeyboardLayout KeyboardLayout;
    /// <summary>
    /// A list of game objects that will be checked for keyboard listeners.
    /// <seealso cref="IKeyboardListener"/>
    /// </summary>
    public List<GameObject> KeyboardListeners;
    
    List<IKeyboardListener> listeners;
    [SerializeField, HideInInspector] List<ManagedKey> managedKeys = new List<ManagedKey>();
    
    void Awake()
    {
        UpdateListeners();
    }

    /// <summary>
    /// Finds and adds any listeners in <see cref="KeyboardListeners"/>.
    /// </summary>
    public void UpdateListeners()
    {
        //Clear previous listeners
        listeners = new List<IKeyboardListener>();

        foreach (var listener in KeyboardListeners)
        {
            if (listener == null)
                continue;
            
            //Find and add new listeners
            foreach (var component in listener.GetComponents<Component>())
            {
                if (component is IKeyboardListener keyboardListener)
                {
                    listeners.Add(keyboardListener);
                }
            }
        }
    }

    /// <summary>
    /// Removes unused keys and creates new as well as updates positions of all keys.
    /// </summary>
    public void UpdateKeys()
    {
#if UNITY_EDITOR
        
        if (KeyboardLayout != null)
        {
            /*
             * To check if all keys are still in the keyboard layout, all keys will be added to a list.
             * If they are still in the list by the end of the method they will be deleted.
             */
            List<ManagedKey> removeStack = new List<ManagedKey>(managedKeys);
            
            //For each row
            for (int i = 0; i < KeyboardLayout.rowCount; i++)
            {
                KeyboardLayout.Row row = KeyboardLayout.rows[i];

                //For each key prefab
                for (int j = 0; j < row.keys.Count; j++)
                {
                    GameObject prefab = row.keys[j];

                    if (prefab == null)
                        continue;
                    
                    GameObject key = null;
                    
                    //Check if key prefab is already instantiated
                    foreach (var managedKey in managedKeys)
                    {
                        if (managedKey.sourcePrefab != prefab || managedKey.row != i)
                            continue;
                        
                        key = managedKey.key;
                        removeStack.Remove(managedKey);
                    }
                    
                    //if not, instantiate prefab
                    if (key == null)
                    {
                        key = (GameObject) PrefabUtility.InstantiatePrefab(prefab, transform);
                        key.GetComponent<Key>().KeyListeners.Add(gameObject);
                        managedKeys.Add(new ManagedKey(key, prefab, i));
                    }
                       
                    //Update key position
                    key.transform.localPosition = new Vector3(KeyboardLayout.distance * j + KeyboardLayout.distance * row.offset, 0, -KeyboardLayout.distance * i);
                }
            }

            //For each special key prefab
            foreach (var specialKeyPrefab in KeyboardLayout.SpecialKeys)
            {
                if (specialKeyPrefab.key == null)
                    continue;
                
                GameObject specialKey = null;
                
                //Check if key prefab is already instantiated
                foreach (var managedKey in managedKeys)
                {
                    if (managedKey.sourcePrefab != specialKeyPrefab.key)
                        continue;
                        
                    specialKey = managedKey.key;
                    removeStack.Remove(managedKey);
                }

                //if not, instantiate prefab
                if (specialKey == null)
                {
                    specialKey = (GameObject) PrefabUtility.InstantiatePrefab(specialKeyPrefab.key, transform);
                    specialKey.GetComponent<Key>().KeyListeners.Add(gameObject);
                    managedKeys.Add(new ManagedKey(specialKey, specialKeyPrefab.key, 0));
                }
                
                //Update key position
                specialKey.transform.localPosition = new Vector3(KeyboardLayout.distance * specialKeyPrefab.offsetX, 0, -KeyboardLayout.distance * specialKeyPrefab.offsetZ);
            }
            
            //Remove keys that are no longer a part of the layout
            foreach (var managedKey in removeStack)
            {
                if (managedKey.key != null)
                    DestroyImmediate(managedKey.key);
            
                managedKeys.Remove(managedKey);
            }
        }
#endif
    }
    
    public void OnKeyDown(GameObject source, string signal)
    {
        //Notify all listeners
        foreach (var listener in listeners)
        {
            listener.OnLetterWritten(signal);
        }
    }

    [Serializable]
    public class ManagedKey
    {
        public GameObject key;
        public GameObject sourcePrefab;
        public int row;

        public ManagedKey(GameObject key, GameObject sourcePrefab, int row)
        {
            this.key = key;
            this.sourcePrefab = sourcePrefab;
            this.row = row;
        }
    }
}