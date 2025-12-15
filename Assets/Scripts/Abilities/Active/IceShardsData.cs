using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Active/Ice Shards",
    fileName = "IceShardsData"
)]
public class IceShardsData : AbilityData
{
    public GameObject shardPrefab;
    public float cooldown = 2f;
    public int damage = 6;
    public int shardCount = 3;
    public float spreadAngle = 30f;
    public float speed = 7f;
}
