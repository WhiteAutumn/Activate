using System;
using System.Collections.Generic;
using UnityEngine;

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
		public List<GameObject> keys = new List<GameObject>();
	}
}