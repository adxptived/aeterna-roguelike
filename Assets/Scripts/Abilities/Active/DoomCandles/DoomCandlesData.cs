using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Active/Doom Candles",
    fileName = "DoomCandlesData"
)]
public class DoomCandlesData : AbilityData
{
    public int damagePerSecond = 4;
    public float radius = 2.5f;
}
