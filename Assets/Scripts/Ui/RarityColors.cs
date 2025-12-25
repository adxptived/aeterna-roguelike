using UnityEngine;
using System.Collections.Generic;

public static class RarityColors
{
    private static readonly Dictionary<AbilityRarity, Color> map =
        new Dictionary<AbilityRarity, Color>
        {
            { AbilityRarity.Common,    new Color(0.75f, 0.75f, 0.75f) },
            { AbilityRarity.Rare,      new Color(0.25f, 0.6f, 1f) },
            { AbilityRarity.Epic,      new Color(0.7f, 0.3f, 1f) },
            { AbilityRarity.Mythic,    new Color(0.7f, 0.3f, 1f) },
            { AbilityRarity.Legendary, new Color(1f, 0.6f, 0.1f) }
        };

    public static Color Get(AbilityRarity rarity)
    {
        return map.TryGetValue(rarity, out var color)
            ? color
            : Color.white;
    }
}
