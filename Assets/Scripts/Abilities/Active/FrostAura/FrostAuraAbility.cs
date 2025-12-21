using UnityEngine;

public class FrostAuraAbility : AuraAbility, IAbilityWithData
{
    private FrostAuraData data;

    public void Init(AbilityData baseData)
    {
        data = (FrostAuraData)baseData;

        baseRadius = data.radius;
        baseTickInterval = 1f;

        ApplyModifiers();
    }

    protected override void ApplyEffect(Enemy enemy)
    {
        enemy.ApplySlow(data.slowMultiplier, data.slowDuration);
    }
}
