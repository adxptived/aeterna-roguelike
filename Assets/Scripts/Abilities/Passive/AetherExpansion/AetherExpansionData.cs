using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Passive/Aether Expansion",
    fileName = "AetherExpansionData"
)]

public class AetherExpansionData : AbilityData, IAbilityFactory
{
    public float radiusMultiplier = 1.25f;

    public bool Grant(AbilityHolder holder)
    {
        return holder.TryAddPassiveAbility<AetherExpansionAbility>(this) != null;
    }

    public bool Has(AbilityHolder holder)
    {
        return holder.GetComponent<AetherExpansionAbility>() != null;
    }
}