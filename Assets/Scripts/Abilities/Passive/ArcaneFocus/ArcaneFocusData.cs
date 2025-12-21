using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Passive/Arcane Focus",
    fileName = "ArcaneFocusData"
)]
public class ArcaneFocusData : AbilityData, IAbilityFactory
{
    public float cooldownMultiplier = 0.85f;

    public bool Grant(AbilityHolder holder)
    {
        return holder.TryAddPassiveAbility<ArcaneFocusAbility>(this) != null;
    }

    public bool Has(AbilityHolder holder)
    {
        return holder.GetComponent<ArcaneFocusAbility>() != null;
    }
}
