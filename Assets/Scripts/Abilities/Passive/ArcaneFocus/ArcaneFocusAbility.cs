using UnityEngine;

public class ArcaneFocusAbility : PassiveAbility, IAbilityWithData
{
    private ArcaneFocusData data;

    public void Init(AbilityData baseData)
    {
        data = (ArcaneFocusData)baseData;

        var modifiers = GetComponent<AbilityModifiers>();
        modifiers.ModifyAuraTick(data.cooldownMultiplier);
    }
}
