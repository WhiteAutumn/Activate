using UnityEditor;

/// <summary>
/// Class <c>KeyEditor</c> is a custom editor for <see cref="Key"/> to check when something in the inspector has been changed.
/// </summary>
[CustomEditor(typeof(Key))]
public class KeyEditor : Editor
{
    Key key;

    void OnEnable()
    {
        //Cache components
        key = (Key) target;
    }

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            //Draw default inspector
            base.OnInspectorGUI();
            
            //If changes have been made, trigger UpdateListeners() in key
            if (check.changed)
                key.UpdateListeners();
        }
    }
}