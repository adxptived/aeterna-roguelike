using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Active/Stone Spikes",
    fileName = "StoneSpikesData"
)]
public class StoneSpikesData : AbilityData
{
    public int damage = 8;
    public float radius = 2.2f;
    public float cooldown = 5f;

    [Header("Visual")]
    public GameObject spikePrefab;
    public int spikeCount = 6;
    public float spikeLifetime = 0.4f;
}
