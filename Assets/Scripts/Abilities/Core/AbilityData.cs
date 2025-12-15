using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    public string abilityName;

    [TextArea]
    public string description;
}
