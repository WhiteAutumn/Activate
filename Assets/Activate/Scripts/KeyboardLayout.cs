using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>KeyboardLayout</c> is a scriptable object for storing layouts used by <see cref="Keyboard"/>.
/// </summary>
[CreateAssetMenu]
public class KeyboardLayout : ScriptableObject
{
    public float distance = 0.5f;
    public int rowCount = 1;
    public int specialKeyCount;
    public List<Row> rows = new List<Row>();
    public List<SpecialKey> SpecialKeys = new List<SpecialKey>();

    [Serializable]
    public class Row
    {
        public bool foldout;
        
        public float offset;
        public int keyCount = 1;
        public List<GameObject> keys = new List<GameObject>();
    }

    [Serializable]
    public class SpecialKey
    {
        public bool foldout;

        public float offsetX;
        public float offsetZ;
        public GameObject key;
    }
}