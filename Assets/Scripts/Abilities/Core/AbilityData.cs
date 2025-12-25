using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    [Header("Info")]
    public string abilityName;
    [TextArea]
    public string description;

    [Header("Grant")]
    public AbilityType abilityType;

    [Header("Drop Settings")]
    public AbilityRarity rarity;
    
    [Tooltip("Weight inside the same rarity")]
    public int weight = 1;

    [Tooltip("Can this ability appear on level up?")]
    public bool canDropFromLevelUp = true;
}
