using UnityEngine;

public class AetherExpansionAbility : PassiveAbility, IAbilityWithData
{
    private AetherExpansionData data;

    public void Init(AbilityData baseData)
    {
        data = (AetherExpansionData)baseData;

        var modifiers = GetComponent<AbilityModifiers>();
        modifiers.ModifyAuraRadius(data.radiusMultiplier);
    }
}
