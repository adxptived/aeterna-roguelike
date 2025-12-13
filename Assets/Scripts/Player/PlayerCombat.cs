using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack")]
    public float attackCooldown = 1f;
    public float attackRange = 5f;
    public int damage = 10;

    private float timer;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Attack();
            timer = attackCooldown;
        }
    }

    void Attack()
    {
        Enemy target = FindClosestEnemy();
        if (target == null) return;

        target.TakeDamage(damage);
    }

    Enemy FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy closest = null;
        float minDist = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist && dist <= attackRange)
            {
                minDist = dist;
                closest = enemy;
            }
        }

        return closest;
    }
}

