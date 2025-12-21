using UnityEngine;

public class FrostAuraAbility : AuraAbility, IAbilityWithData
{
    private FrostAuraData data;

    public void Init(AbilityData baseData)
    {
        data = (FrostAuraData)baseData;
        radius = data.radius;
        tickInterval = 1f;
    }

    protected override void ApplyEffect(Enemy enemy)
    {
        enemy.ApplySlow(data.slowMultiplier, data.slowDuration);
    }
}
