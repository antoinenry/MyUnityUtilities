using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleScriptableObject : ScriptableObject
{
    #region STATIC
    //static private SingleScriptableObject _current;
    protected abstract SingleScriptableObject CurrentAbstract { get; set; }

    static private Dictionary<Type, SingleScriptableObject> _currentBase;

    static public T GetCurrent<T>() where T : SingleScriptableObject
    {
        Type objectType = typeof(T);
        if (_currentBase == null) _currentBase = new Dictionary<Type, SingleScriptableObject>();
        if (_currentBase.ContainsKey(objectType) == false) _currentBase.Add(objectType, FindCurrent(objectType));
        return _currentBase[objectType] as T;
    }

    static public void SetCurrent<T>(T value) where T : SingleScriptableObject
    {
        if (_currentBase == null) _currentBase = new Dictionary<Type, SingleScriptableObject>();
        Type objectType = typeof(T);
        if (_currentBase.ContainsKey(objectType) == false) _currentBase.Add(objectType, value);
        else _currentBase[objectType] = value;
    }

    static public SingleScriptableObject[] FindAll(Type objectType)
    {
        Resources.LoadAll("", objectType);
        SingleScriptableObject[] all = Resources.FindObjectsOfTypeAll(objectType) as SingleScriptableObject[];
        Resources.UnloadUnusedAssets();
        return all;
    }

    static public SingleScriptableObject FindCurrent(Type objectType)
    {
        SingleScriptableObject[] all = FindAll(objectType);
        if (all == null) return null;
        SingleScriptableObject[] allCurrents = Array.FindAll(all, o => o.isCurrent);
        if (allCurrents == null || allCurrents.Length == 0) return null;
        if (allCurrents.Length > 1) Debug.LogWarning("Several current " + allCurrents[0].GetType() + " found.");
        return allCurrents[0];
    }

    static public void ClearCurrent(Type objectType)
    {
        SingleScriptableObject[] all = FindAll(objectType);
        if (all == null || all.Length == 0) return;
        foreach (SingleScriptableObject o in all)
        {
            o.isCurrent = false;
            o.CurrentAbstract = null;
        }
    }
    #endregion

    #region INSTANCE
    [SerializeField] [HideInInspector] private bool isCurrent;

    public bool IsCurrent
    {
        get => isCurrent && CurrentAbstract == this;
        set
        {
            if (value == true) SetCurrent(this);
            else if (GetCurrent() == this) SetCurrent(null);
        }
    }

    public SingleScriptableObject GetCurrent()
    {
        Type objectType = GetType();
        if (CurrentAbstract == null) CurrentAbstract = FindCurrent(objectType);
        return CurrentAbstract;
    }

    public void SetCurrent(SingleScriptableObject value)
    {
        Type objectType = GetType();
        ClearCurrent(objectType);
        CurrentAbstract = value;
        if (CurrentAbstract) CurrentAbstract.isCurrent = true;
    }

    //public SingleScriptableObject GetCurrent() => Current;
    #endregion
}
