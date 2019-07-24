using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor for keyboard layouts
/// </summary>
[CustomEditor(typeof(KeyboardLayout))]
public class KeyboardLayoutEditor : Editor
{
	KeyboardLayout layout;
	
	void OnEnable()
	{
		layout = (KeyboardLayout) target;
	}

	void OnDestroy()
	{
		AssetDatabase.SaveAssets();
	}

	public override void OnInspectorGUI()
	{
		using (EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope())
		{
			//Create new style for large sized labels based of normal label style
			GUIStyle styleLargeLabel = new GUIStyle(GUI.skin.GetStyle("label"));
			styleLargeLabel.fontSize = 15;
			
			//Create new style for medium sized labels based of normal label style
			GUIStyle styleMediumLabel = new GUIStyle(GUI.skin.GetStyle("label"));
			styleMediumLabel.fontSize = 12;
			
			//Create new style for large sized foldouts based of normal foldout style
			GUIStyle styleLargeFoldout = new GUIStyle(GUI.skin.GetStyle("foldout"));
			styleLargeFoldout.fontSize = 15;
			
			

			//Draw field for changing key distance
			EditorGUILayout.LabelField("Key Distance", styleLargeLabel, GUILayout.Height(20));
			GUILayout.Space(5);
			layout.distance = EditorGUILayout.FloatField(layout.distance);
			GUILayout.Space(10);
		
			//Draw buttons to allow for adjusting row count
			EditorGUILayout.LabelField("Rows | " + layout.rowCount, styleLargeLabel, GUILayout.Height(20));
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(EditorGUI.indentLevel * 17);
			if (GUILayout.Button("+", GUILayout.MaxWidth(30), GUILayout.MaxHeight(20)))
			{
				layout.rowCount++;
			}
			if (GUILayout.Button("-", GUILayout.MaxWidth(30), GUILayout.MaxHeight(20)))
			{
				layout.rowCount = Mathf.Max(1, layout.rowCount - 1);
			}
			EditorGUILayout.EndHorizontal();
			GUILayout.Space(10);
		
			//Update row list to comply with updated row count
			while (layout.rowCount > layout.rows.Count)
			{
				layout.rows.Add(new KeyboardLayout.Row());
			}
			while (layout.rowCount < layout.rows.Count)
			{
				layout.rows.RemoveAt(layout.rows.Count - 1);
			}
			
			

			//For each row, draw row editor
			for (int i = 0; i < layout.rowCount; i++)
			{
				KeyboardLayout.Row row = layout.rows[i];
			
				//Draw foldout
				EditorGUI.indentLevel++;
				row.foldout = EditorGUILayout.Foldout(row.foldout, "Row " + (i + 1), true, styleLargeFoldout);
				GUILayout.Space(15);
				EditorGUI.indentLevel--;
				
				if (row.foldout)
				{
					EditorGUI.indentLevel++;
					
					//Draw field for changing row offset
					EditorGUILayout.LabelField("Row offset", styleMediumLabel, GUILayout.Height(15));
					GUILayout.Space(5);
					row.offset = EditorGUILayout.FloatField(row.offset);
					GUILayout.Space(10);

					//Draw buttons to allow for adjusting key count
					EditorGUILayout.LabelField("Keys | " + row.keyCount, styleMediumLabel, GUILayout.Height(20));
					GUILayout.BeginHorizontal();
					GUILayout.Space(EditorGUI.indentLevel * 16);
					if (GUILayout.Button("+", GUILayout.MaxWidth(30), GUILayout.MaxHeight(15)))
					{
						row.keyCount++;
					}
					if (GUILayout.Button("-", GUILayout.MaxWidth(30), GUILayout.MaxHeight(15)))
					{
						row.keyCount = Mathf.Max(0, row.keyCount - 1);
					}
					GUILayout.EndHorizontal();
					GUILayout.Space(20);
		
					//Update key list to comply with updated key count
					while (row.keyCount > row.keys.Count)
					{
						row.keys.Add(null);
					}
					while (row.keyCount < row.keys.Count)
					{
						row.keys.RemoveAt(row.keys.Count - 1);
					}

					for (int j = 0; j < row.keyCount; j++)
					{
						row.keys[j] = (GameObject) EditorGUILayout.ObjectField("Key " + (j + 1), row.keys[j], typeof(GameObject), false);
						GUILayout.Space(5);
					}
					
					EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
					EditorGUI.indentLevel--;
				}
			}

			//If changes have been made, mark object as dirty
			if (check.changed)
			{
				EditorUtility.SetDirty(layout);
			}
		}
	}
}