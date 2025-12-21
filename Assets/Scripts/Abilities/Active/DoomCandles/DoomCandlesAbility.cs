using UnityEngine;

public class DoomCandlesAbility : AuraAbility, IAbilityWithData
{
    private DoomCandlesData data;

    public void Init(AbilityData baseData)
    {
        data = (DoomCandlesData)baseData;

        baseRadius = data.radius;
        baseTickInterval = 1f;

        ApplyModifiers();
    }

    protected override void ApplyEffect(Enemy enemy)
    {
        enemy.TakeDamage(data.damagePerSecond);
    }
}
