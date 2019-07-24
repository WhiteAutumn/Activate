using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Keyboard : MonoBehaviour, IPassiveKeyListener
{
	public KeyboardLayout KeyboardLayout;
	[SerializeField] List<ManagedKey> managedKeys = new List<ManagedKey>();

	public void OnKeyDown(GameObject source, string signal)
	{
		
	}

	public void OnValidate()
	{
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
		}

		foreach (ManagedKey managedKey in removeStack)
		{
			DestroyImmediate(managedKey.key);
			managedKeys.Remove(managedKey);
		}
		
		/*
		foreach (ManagedKey managedKey in managedKeys)
		{
			managedKey.removed = true;
		}

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
							if (managedKeys[k].sourcePrefab == prefab)
							{
								managedKeys[k].removed = false;
								key = managedKeys[k].key;
							}
						}
						if (key == null)
						{
							key = (GameObject) PrefabUtility.InstantiatePrefab(prefab, transform);
							key.GetComponent<Key>().KeyListeners.Add(gameObject);
							managedKeys.Add(new ManagedKey(key, prefab));
						}
						
						key.transform.localPosition = new Vector3(KeyboardLayout.distance * j + KeyboardLayout.distance * row.offset, 0, -KeyboardLayout.distance * i);
					}
				}
			}
		}
		
		List<ManagedKey> shouldRemove = new List<ManagedKey>();
		foreach (ManagedKey managedKey in managedKeys)
		{
			if (managedKey.removed)
			{
				DestroyImmediate(managedKey.key);
				shouldRemove.Add(managedKey);
			}
		}

		foreach (ManagedKey managedKey in shouldRemove)
		{
			managedKeys.Remove(managedKey);
		}
		*/
	}
	
	

	[Serializable]
	class ManagedKey
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