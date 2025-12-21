using UnityEngine;

public class FireMissileAbility : ActiveAbility, IAbilityWithData
{
    private FireMissileData data;
    private float timer;

    public void Init(AbilityData abilityData)
    {
        data = abilityData as FireMissileData;
        timer = data.cooldown;
    }

    private void Update()
    {
        if (data == null) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Shoot();
            timer = data.cooldown;
        }
    }

    void Shoot()
    {
        Enemy target = FindClosestEnemy();
        if (target == null) return;

        GameObject proj = Instantiate(
            data.projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        proj.GetComponent<FireMissileProjectile>()
            .Init(target.transform, data.damage, data.projectileSpeed);
    }

    Enemy FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float minDist = data.range;
        Enemy closest = null;

        foreach (Enemy e in enemies)
        {
            float d = Vector2.Distance(transform.position, e.transform.position);
            if (d < minDist)
            {
                minDist = d;
                closest = e;
            }
        }

        return closest;
    }
}
