using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SingleScriptable")]
public class SingleScriptableObject : ScriptableObject
{
    #region STATIC
    static private SingleScriptableObject _current;

    static public SingleScriptableObject Current 
    { 
        get
        {
            if (_current == null) _current = FindCurrent();
            return _current;
        }
        set
        {
            ClearCurrent();
            _current = value;
            if (_current) _current.isCurrent = true;
        }
    }

    static public SingleScriptableObject[] FindAll()
    {
        Resources.LoadAll("", typeof(SingleScriptableObject));
        SingleScriptableObject[] all = Resources.FindObjectsOfTypeAll<SingleScriptableObject>();
        Resources.UnloadUnusedAssets();
        return all;
    } 

    static private SingleScriptableObject FindCurrent()
    {
        SingleScriptableObject[] all = FindAll();
        if (all == null) return null;
        SingleScriptableObject[] allCurrents = Array.FindAll(all, o => o.isCurrent);
        if (allCurrents == null || allCurrents.Length == 0) return null;
        if (allCurrents.Length > 1) Debug.LogWarning("Several current " + allCurrents[0].GetType() + " found.");
        return allCurrents[0];
    }

    static private void ClearCurrent()
    {
        Debug.Log("ClearCurrent:");
        SingleScriptableObject[] all = FindAll();
        if (all == null || all.Length == 0) return;
        foreach (SingleScriptableObject o in all)
        {
            Debug.Log(" - " + o);
            o.isCurrent = false;
        }
        _current = null;
    }
    #endregion

    #region INSTANCE
    [SerializeField] [HideInInspector] private bool isCurrent;

    public bool IsCurrent
    {
        get => isCurrent && _current == this;
        set
        {
            if (value == true) Current = this;
            else if (Current == this) Current = null;
        }
    }

    public SingleScriptableObject GetCurrent() => Current;
    #endregion
}
