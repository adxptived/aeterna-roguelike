using UnityEngine;

public abstract class ActiveAbility : AbilityBase
{
    protected AbilityModifiers modifiers;

    protected virtual void Awake()
    {
        modifiers = GetComponent<AbilityModifiers>();
    }
}
