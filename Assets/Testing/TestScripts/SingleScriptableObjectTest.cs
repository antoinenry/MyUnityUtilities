using UnityEngine;

[CreateAssetMenu(fileName ="SSO", menuName ="UtilityTest/Single Scriptable Object")]
public class SingleScriptableObjectTest : SingleScriptableObject
{
    protected override SingleScriptableObject CurrentObject { get => Current; set => Current = value as SingleScriptableObjectTest; }
    static public SingleScriptableObjectTest Current;
}
