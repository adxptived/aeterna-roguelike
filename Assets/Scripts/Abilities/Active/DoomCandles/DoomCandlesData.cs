using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Active/Doom Candles",
    fileName = "DoomCandlesData"
)]
public class DoomCandlesData : AbilityData, IAbilityFactory
{
    public int damagePerSecond = 4;
    public float radius = 2.5f;

    public bool Grant(AbilityHolder holder)
    {
        return holder.TryAddActiveAbility<DoomCandlesAbility>(this) != null;
    }

    public bool Has(AbilityHolder holder)
    {
        return holder.GetComponent<DoomCandlesAbility>() != null;
    }
}
