using UnityEditor;
using UnityEngine;

/// <summary>
/// Class <c>KeyboardEditor</c> is a custom editor for <see cref="Keyboard"/> to allow for the editing of <see cref="KeyboardLayout"/> scriptable objects inside of the keyboard editor.
/// </summary>
[CustomEditor(typeof(Keyboard))]
public class KeyboardEditor : Editor
{
    Keyboard keyboard;
    
    void OnEnable()
    {
        //Cache components
        keyboard = (Keyboard) target;
    }
    
    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            //Create new style for large sized labels based of normal label style
            GUIStyle styleLargeLabel = new GUIStyle(GUI.skin.GetStyle("label")) {fontSize = 15};
            
            //Disallow editing while in play mode
            if (Application.isPlaying)
            {
                EditorGUILayout.LabelField("Unavailable while in play mode", styleLargeLabel, GUILayout.Height(20));
            }
            else
            {
                //Draw default inspector
                base.OnInspectorGUI();

                //If a layout has been assigned, draw its editor
                if (keyboard.KeyboardLayout != null)
                {
                    //Draw label
                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Layout", styleLargeLabel, GUILayout.Height(20));
            
                    //Draw separator
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            
                    //Draw layout editor
                    Editor editor = CreateEditor(keyboard.KeyboardLayout);
                    EditorGUI.indentLevel++;
                    editor.OnInspectorGUI();
                    EditorGUI.indentLevel--;

                    //If changes have been made, trigger OnValidate() in keyboard to update keys
                    if (check.changed)
                    {
                        keyboard.UpdateListeners();
                        keyboard.UpdateKeys();
                    }
                }
            }
        }
    }
}