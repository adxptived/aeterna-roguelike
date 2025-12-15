using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Active/Fire Missile",
    fileName = "FireMissileData"
)]
public class FireMissileData : AbilityData
{
    public GameObject projectilePrefab;
    public float cooldown = 1.5f;
    public int damage = 10;
    public float projectileSpeed = 6f;
    public float range = 10f;
}
