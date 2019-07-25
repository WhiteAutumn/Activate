using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Keyboard : MonoBehaviour, IPassiveKeyListener
{
    public KeyboardLayout KeyboardLayout;
    public List<GameObject> KeyboardListeners;
    
    [SerializeField, HideInInspector] List<ManagedKey> managedKeys = new List<ManagedKey>();
    List<IKeyboardListener> listeners = new List<IKeyboardListener>();

    void OnValidate()
    {
        listeners = new List<IKeyboardListener>();

        foreach (GameObject listener in KeyboardListeners)
        {
            if (listener != null)
            {
                foreach (Component component in listener.GetComponents<Component>())
                {
                    if (component is IKeyboardListener keyboardListener)
                    {
                        listeners.Add(keyboardListener);
                    }
                }
            }
        }
    }

    public void OnKeyDown(GameObject source, string signal)
    {
        foreach (IKeyboardListener listener in listeners)
        {
            listener.OnLetterWritten(signal);
        }
    }

    public void UpdateKeys()
    {
#if UNITY_EDITOR
        List<ManagedKey> removeStack = new List<ManagedKey>(managedKeys);
        
        if (KeyboardLayout != null)
        {
            for (int i = 0; i < KeyboardLayout.rowCount; i++)
            {
                KeyboardLayout.Row row = KeyboardLayout.rows[i];

                for (int j = 0; j < row.keys.Count; j++)
                {
                    GameObject prefab = row.keys[j];

                    if (prefab != null)
                    {
                        GameObject key = null;
                        for (int k = 0; k < managedKeys.Count; k++)
                        {
                            if (managedKeys[k].sourcePrefab == prefab && managedKeys[k].row == i)
                            {
                                key = managedKeys[k].key;
                                removeStack.Remove(managedKeys[k]);
                            }
                        }
                        
                        if (key == null)
                        {
                            key = (GameObject) PrefabUtility.InstantiatePrefab(prefab, transform);
                            key.GetComponent<Key>().KeyListeners.Add(gameObject);
                            managedKeys.Add(new ManagedKey(key, prefab, i));
                        }
                        
                        key.transform.localPosition = new Vector3(KeyboardLayout.distance * j + KeyboardLayout.distance * row.offset, 0, -KeyboardLayout.distance * i);
                    }
                }
            }

            foreach (KeyboardLayout.SpecialKey specialKeyPrefab in KeyboardLayout.SpecialKeys)
            {
                if (specialKeyPrefab.key != null)
                {
                    GameObject specialKey = null;
                    foreach (ManagedKey managedKey in managedKeys)
                    {
                        if (managedKey.sourcePrefab == specialKeyPrefab.key)
                        {
                            specialKey = managedKey.key;
                            removeStack.Remove(managedKey);
                        }
                    }

                    if (specialKey == null)
                    {
                        specialKey = (GameObject) PrefabUtility.InstantiatePrefab(specialKeyPrefab.key, transform);
                        specialKey.GetComponent<Key>().KeyListeners.Add(gameObject);
                        managedKeys.Add(new ManagedKey(specialKey, specialKeyPrefab.key, 0));
                    }
                
                    specialKey.transform.localPosition = new Vector3(KeyboardLayout.distance * specialKeyPrefab.offsetX, 0, -KeyboardLayout.distance * specialKeyPrefab.offsetZ);
                }
            }
        }

        foreach (ManagedKey managedKey in removeStack)
        {
            if (managedKey.key != null)
                DestroyImmediate(managedKey.key);
            
            managedKeys.Remove(managedKey);
        }
#endif
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