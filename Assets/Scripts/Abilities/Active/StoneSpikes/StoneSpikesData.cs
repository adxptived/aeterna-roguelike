using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Active/Stone Spikes",
    fileName = "StoneSpikesData"
)]
public class StoneSpikesData : AbilityData, IAbilityFactory
{
    [Header("Damage")]
    public int damage = 8;
    public float radius = 2.2f;
    public float cooldown = 3f;

    [Header("Visual")]
    public GameObject spikePrefab;
    public int spikeCount = 6;
    public float spikeLifetime = 0.4f;

    public bool Grant(AbilityHolder holder)
    {
        return holder.TryAddActiveAbility<StoneSpikesAbility>(this) != null;
    }

    public bool Has(AbilityHolder holder)
    {
        return holder.GetComponent<StoneSpikesAbility>() != null;
    }
}
