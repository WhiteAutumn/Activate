using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KeyboardLayout))]
public class KeyboardLayoutEditor : Editor
{
	public override void OnInspectorGUI()
	{
		KeyboardLayout layout = (KeyboardLayout) target;
		
		//Save button, if pressed saves all changes to disk
		if (GUILayout.Button("Save"))
		{
			EditorUtility.SetDirty(layout);
			AssetDatabase.SaveAssets();
		}

		layout.distance = EditorGUILayout.FloatField("Key Distance", layout.distance);
		
		
	}
}