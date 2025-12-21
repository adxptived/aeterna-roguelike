using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Active/Frost Aura",
    fileName = "FrostAuraData"
)]
public class FrostAuraData : AbilityData
{
    public float radius = 1f;
    public float slowMultiplier = 0.2f; // -50% скорости
    public float slowDuration = 0.1f;
}
