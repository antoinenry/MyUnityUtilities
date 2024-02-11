using UnityEngine;

[ExecuteAlways]
public class SingleScriptableObjectSceneTest : MonoBehaviour
{
    public string current;

    private void Update()
    {
        current = SingleScriptableObjectTest.Current ? SingleScriptableObjectTest.Current.name : "(null)";
    }
}
