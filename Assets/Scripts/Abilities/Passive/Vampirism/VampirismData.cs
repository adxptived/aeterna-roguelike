using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Passive/Vampirism",
    fileName = "VampirismData"
)]
public class VampirismData : AbilityData
{
    [Range(0f, 1f)]
    public float lifeStealPercent = 0.05f; // 5%
}
