using UnityEngine;

public class DoomCandlesAbility : AuraAbility, IAbilityWithData
{
    private DoomCandlesData data;

    public void Init(AbilityData baseData)
    {
        data = (DoomCandlesData)baseData;

        radius = data.radius;
        tickInterval = 1f; // 1 тик = 1 секунда
    }

    protected override void ApplyEffect(Enemy enemy)
    {
        Debug.Log("AURA");
        enemy.TakeDamage(data.damagePerSecond);
    }
}
