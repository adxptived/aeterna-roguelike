using UnityEngine;

[CreateAssetMenu(
    menuName = "Abilities/Active/Fire Missile",
    fileName = "FireMissileData"
)]
public class FireMissileData : AbilityData, IAbilityFactory
{
    public GameObject projectilePrefab;
    public float cooldown = 1.5f;
    public int damage = 10;
    public float projectileSpeed = 6f;
    public float range = 10f;

    public bool Grant(AbilityHolder holder)
    {
        return holder.TryAddActiveAbility<FireMissileAbility>(this) != null;
    }

    public bool Has(AbilityHolder holder)
    {
        return holder.GetComponent<FireMissileAbility>() != null;
    }
}
