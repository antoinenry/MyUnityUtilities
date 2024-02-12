using UnityEngine;

[CreateAssetMenu(menuName = "SingleScriptableTest2")]
public class SingleScriptableObjectTest2 : SingleScriptableObject
{
    #region SingleScriptableObject implementation
    protected override SingleScriptableObject CurrentAbstract { get => Current; set => Current = value as SingleScriptableObjectTest2; }
    static public SingleScriptableObjectTest2 Current;
    #endregion

    public string someOtherData;
}