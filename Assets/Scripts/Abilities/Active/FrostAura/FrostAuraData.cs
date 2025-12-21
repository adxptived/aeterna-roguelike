using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Active/Frost Aura",
    fileName = "FrostAuraData"
)]
public class FrostAuraData : AbilityData, IAbilityFactory
{
    public float radius = 2f;
    public float slowMultiplier = 0.2f;
    public float slowDuration = 0.2f;

    public bool Grant(AbilityHolder holder)
    {
        return holder.TryAddActiveAbility<FrostAuraAbility>(this) != null;
    }

    public bool Has(AbilityHolder holder)
    {
        return holder.GetComponent<FrostAuraAbility>() != null;
    }
}
