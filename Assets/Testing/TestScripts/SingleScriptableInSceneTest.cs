using UnityEngine;

[ExecuteAlways]
public class SingleScriptableInSceneTest : MonoBehaviour
{
    public string current;

    private void Update()
    {
        current = SingleScriptableObject.Current ? SingleScriptableObject.Current.name : "null";
    }
}
