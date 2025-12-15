using UnityEngine;
using System.Collections.Generic;

public class AbilityHolder : MonoBehaviour
{
    [Header("Limits")]
    public int maxActiveAbilities = 6;
    public int maxPassiveAbilities = 5;

    public readonly List<ActiveAbility> activeAbilities = new();
    public readonly List<PassiveAbility> passiveAbilities = new();

    // -------------------------
    // ACTIVE
    // -------------------------
    public bool TryAddActiveAbility<T>() where T : ActiveAbility
    {
        if (activeAbilities.Count >= maxActiveAbilities)
            return false;

        if (HasActiveAbility<T>())
            return false;

        T ability = gameObject.AddComponent<T>();
        activeAbilities.Add(ability);

        return true;
    }

    public bool HasActiveAbility<T>() where T : ActiveAbility
    {
        return GetComponent<T>() != null;
    }

    // -------------------------
    // PASSIVE
    // -------------------------
    public bool TryAddPassiveAbility<T>() where T : PassiveAbility
    {
        if (passiveAbilities.Count >= maxPassiveAbilities)
            return false;

        if (HasPassiveAbility<T>())
            return false;

        T ability = gameObject.AddComponent<T>();
        passiveAbilities.Add(ability);

        return true;
    }

    public bool HasPassiveAbility<T>() where T : PassiveAbility
    {
        return GetComponent<T>() != null;
    }

    // -------------------------
    // REMOVE (на будущее)
    // -------------------------
    public void RemoveAbility(AbilityBase ability)
    {
        ActiveAbility active = ability as ActiveAbility;
        if (active != null)
        {
            activeAbilities.Remove(active);
        }

        PassiveAbility passive = ability as PassiveAbility;
        if (passive != null)
        {
            passiveAbilities.Remove(passive);
        }

        Destroy(ability);
    }

}
