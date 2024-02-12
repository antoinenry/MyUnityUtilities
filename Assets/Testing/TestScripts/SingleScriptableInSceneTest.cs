using UnityEngine;

[ExecuteAlways]
public class SingleScriptableInSceneTest : MonoBehaviour
{
    public string currentData1;
    public string currentData2;

    private void Update()
    {
        SingleScriptableObjectTest1 current1 = SingleScriptableObject.GetCurrent<SingleScriptableObjectTest1>();
        currentData1 = current1 != null ? current1.someData : "NO CURRENT";
    }
}
