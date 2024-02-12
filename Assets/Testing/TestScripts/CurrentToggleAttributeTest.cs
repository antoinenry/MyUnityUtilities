using System;
using UnityEngine;

[CreateAssetMenu(menuName = "CurrentToggleAttributeTest", fileName = "CurrentToggleAttributeTest")]
public class CurrentToggleAttributeTest : ScriptableObject
{
    [CurrentToggle] public bool isCurrent;
}
