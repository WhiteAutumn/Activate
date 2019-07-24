using UnityEditor;
using UnityEngine;

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
		base.OnInspectorGUI();

		if (keyboard.KeyboardLayout != null)
		{
			GUILayout.Space(20);
			GUILayout.Label("Layout");
			GUILayout.Space(5);

			using (EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope())
			{
				Editor editor = CreateEditor(keyboard.KeyboardLayout);
				editor.OnInspectorGUI();

				if (check.changed)
				{
					keyboard.OnValidate();
				}
			}
		}
	}
}