using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom Editor to allow for editing of keyboard layouts inside the keyboard's editor.
/// </summary>
[CustomEditor(typeof(Keyboard))]
public class KeyboardEditor : Editor
{
	Keyboard keyboard;
	
	void OnEnable()
	{
		keyboard = (Keyboard) target;
	}
	
	public override void OnInspectorGUI()
	{
		//Draw default inspector
		base.OnInspectorGUI();

		//If a layout has been designed, draw its editor
		if (keyboard.KeyboardLayout != null)
		{
			//Change label font size
			GUIStyle style = GUI.skin.GetStyle("label");
			style.fontSize = 16;
			
			//Draw label
			GUILayout.Space(15);
			GUILayout.Label("Layout");
			
			//Draw separator
			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

			using (EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope())
			{
				Editor editor = CreateEditor(keyboard.KeyboardLayout);
				EditorGUI.indentLevel++;
				editor.OnInspectorGUI();
				EditorGUI.indentLevel--;

				//If changes have been made, trigger OnValidate() in keyboard to update keys
				if (check.changed)
				{
					keyboard.OnValidate();
				}
			}
		}
	}
}