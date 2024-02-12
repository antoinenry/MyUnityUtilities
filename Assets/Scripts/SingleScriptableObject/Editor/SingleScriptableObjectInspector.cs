using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SingleScriptableObject), true)]
public class SingleScriptableObjectInspector : Editor
{
    private SingleScriptableObject targetSSO;

    private void OnEnable()
    {
        targetSSO = target as SingleScriptableObject;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (targetSSO.IsCurrent)
        {
            EditorGUILayout.HelpBox("This is Current.", MessageType.Info);
        }
        else
        {
            if (targetSSO.GetCurrent() == null)
                EditorGUILayout.HelpBox("No Current is set.", MessageType.Warning);
            else
                EditorGUILayout.HelpBox(targetSSO.GetCurrent().name + " is Current.", MessageType.Warning);
            if (GUILayout.Button("Set this as Current"))
            {
                targetSSO.IsCurrent = true;
                SetDirtyAll();
            }
        }
    }

    private void SetDirtyAll()
    {
        SingleScriptableObject[] all = SingleScriptableObject.FindAll(targetSSO.GetType());
        if (all != null) foreach (SingleScriptableObject o in all) EditorUtility.SetDirty(o);
    }
}
