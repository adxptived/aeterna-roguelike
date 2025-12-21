using UnityEngine;
using System;

public abstract class AbilityData : ScriptableObject
{
    public string abilityName;
    [TextArea] public string description;

    [Header("Grant")]
    public AbilityType abilityType;
}

