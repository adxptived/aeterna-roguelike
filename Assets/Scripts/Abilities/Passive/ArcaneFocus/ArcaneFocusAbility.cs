using UnityEngine;

public class ArcaneFocusAbility : PassiveAbility, IAbilityWithData
{
    private ArcaneFocusData data;

    public void Init(AbilityData baseData)
    {
        data = (ArcaneFocusData)baseData;

        ActiveAbility[] actives = GetComponents<ActiveAbility>();
        foreach (var ability in actives)
        {
            if (ability is AuraAbility aura)
            {
                aura.ModifyTickInterval(data.cooldownMultiplier);
            }
        }
    }
}
