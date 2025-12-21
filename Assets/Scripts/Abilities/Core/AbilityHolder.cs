using UnityEngine;
using System.Collections.Generic;

public class AbilityHolder : MonoBehaviour
{
    [Header("Limits")]
    public int maxActiveAbilities = 6;
    public int maxPassiveAbilities = 5;

    private readonly List<ActiveAbility> activeAbilities = new();
    private readonly List<PassiveAbility> passiveAbilities = new();


    // =========================
    // ACTIVE ABILITIES (NEW API)
    // =========================
    public T TryAddActiveAbility<T>(AbilityData data)
        where T : ActiveAbility
    {
        if (activeAbilities.Count >= maxActiveAbilities)
            return null;

        if (HasActiveAbility<T>())
            return null;

        T ability = gameObject.AddComponent<T>();

        if (ability is IAbilityWithData withData)
        {
            withData.Init(data);
        }
        else
        {
            Debug.LogError($"{typeof(T).Name} does not implement IAbilityWithData");
            Destroy(ability);
            return null;
        }

        activeAbilities.Add(ability);
        return ability;
    }

    public T TryAddPassiveAbility<T>(AbilityData data)
        where T : PassiveAbility
    {
        if (passiveAbilities.Count >= maxPassiveAbilities)
            return null;

        if (GetComponent<T>() != null)
            return null;

        T ability = gameObject.AddComponent<T>();

        if (ability is IAbilityWithData withData)
        {
            withData.Init(data);
        }
        else
        {
            Debug.LogError($"{typeof(T).Name} does not implement IAbilityWithData");
            Destroy(ability);
            return null;
        }

        passiveAbilities.Add(ability);
        return ability;
    }



    public bool HasActiveAbility<T>() where T : ActiveAbility
    {
        return GetComponent<T>() != null;
    }

    // =========================
    // PASSIVE (пока не трогаем)
    // =========================
}
