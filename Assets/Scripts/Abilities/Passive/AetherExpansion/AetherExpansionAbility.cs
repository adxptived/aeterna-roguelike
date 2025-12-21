using UnityEngine;

public class AetherExpansionAbility : PassiveAbility, IAbilityWithData
{
    private AetherExpansionData data;

    public void Init(AbilityData baseData)
    {
        data = (AetherExpansionData)baseData;

        // Усиливаем ВСЕ AuraAbility на игроке
        AuraAbility[] auras = GetComponents<AuraAbility>();
        foreach (var aura in auras)
        {
            aura.ModifyRadius(data.radiusMultiplier);

        }
    }
}
