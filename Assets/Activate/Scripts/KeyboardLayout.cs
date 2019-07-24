using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object for storing keyboard layouts
/// </summary>
[CreateAssetMenu]
public class KeyboardLayout : ScriptableObject
{
	public float distance = 0.5f;
	public int rowCount = 1;
	public List<Row> rows = new List<Row>();

	[Serializable]
	public class Row
	{
		public bool foldout;
		
		public float offset;
		public int keyCount = 1;
		public List<GameObject> keys = new List<GameObject>();
	}
}