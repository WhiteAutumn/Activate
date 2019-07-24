using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KeyboardLayout))]
public class KeyboardLayoutEditor : Editor
{
	public override void OnInspectorGUI()
	{
		KeyboardLayout layout = (KeyboardLayout) target;
		
		GUIStyle style = GUI.skin.GetStyle("label");
		style.fontSize = 14;
		
		if (GUILayout.Button("Save"))
		{
			EditorUtility.SetDirty(layout);
			AssetDatabase.SaveAssets();
		}
		GUILayout.Space(20);

		GUILayout.Label("Key Distance");
		layout.distance = EditorGUILayout.FloatField(layout.distance);
		GUILayout.Space(10);
		
		GUILayout.Label("Rows | " + layout.rowCount);
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("+", GUILayout.MaxWidth(30), GUILayout.MaxHeight(20)))
		{
			layout.rowCount++;
		}
		if (GUILayout.Button("-", GUILayout.MaxWidth(30), GUILayout.MaxHeight(20)))
		{
			layout.rowCount = Mathf.Max(1, layout.rowCount - 1);
		}
		EditorGUILayout.EndHorizontal();
		GUILayout.Space(20);
		
		while (layout.rowCount > layout.rows.Count)
		{
			layout.rows.Add(new KeyboardLayout.Row());
		}
		while (layout.rowCount < layout.rows.Count)
		{
			layout.rows.RemoveAt(layout.rows.Count - 1);
		}

		for (int i = 0; i < layout.rowCount; i++)
		{
			KeyboardLayout.Row row = layout.rows[i];
			
			row.foldout = EditorGUILayout.Foldout(row.foldout, "Row " + (i + 1));
			if (row.foldout)
			{
				
			}
		}
	}
}