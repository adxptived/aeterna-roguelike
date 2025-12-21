using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Passive/Arcane Focus",
    fileName = "ArcaneFocusData"
)]
public class ArcaneFocusData : AbilityData
{
    public float cooldownMultiplier = 0.85f; // -15%
}
