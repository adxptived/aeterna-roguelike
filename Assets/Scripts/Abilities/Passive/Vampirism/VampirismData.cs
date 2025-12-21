using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Passive/Vampirism",
    fileName = "VampirismData"
)]
public class VampirismData : AbilityData, IAbilityFactory
{
    [Range(0f, 1f)]
    public float lifeStealPercent = 0.1f;

    public bool Grant(AbilityHolder holder)
    {
        return holder.TryAddPassiveAbility<VampirismAbility>(this) != null;
    }

    public bool Has(AbilityHolder holder)
    {
        return holder.GetComponent<VampirismAbility>() != null;
    }
}
