using UnityEngine;

[CreateAssetMenu(menuName = "SingleScriptableTest1")]
public class SingleScriptableObjectTest1 : SingleScriptableObject
{
    #region SingleScriptableObject implementation
    protected override SingleScriptableObject CurrentAbstract { get => Current; set => Current = value as SingleScriptableObjectTest1; }
    static public SingleScriptableObjectTest1 Current;
    #endregion

    public string someData;
}