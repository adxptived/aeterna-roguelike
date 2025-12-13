using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP = 30;
    public float moveSpeed = 2f;

    [Header("Combat")]
    public int contactDamage = 5;
    public float damageInterval = 0.5f;

    [Header("Rewards")]
    public int xpReward = 3;

    private int hp;
    private Transform player;
    private PlayerStats playerStats;

    private float damageTimer;

    private void Awake()
    {
        hp = maxHP;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerStats = playerObj.GetComponent<PlayerStats>();
        }
    }

    private void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;
        transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
    }

    // 👇 КОНТАКТНЫЙ УРОН
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        damageTimer -= Time.deltaTime;

        if (damageTimer <= 0f)
        {
            DealDamage();
            damageTimer = damageInterval;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            damageTimer = 0f; // сброс таймера
        }
    }

    void DealDamage()
    {
        if (playerStats != null)
        {
            playerStats.TakeDamage(contactDamage);
        }

        var movement = playerStats.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            Vector2 dir = (playerStats.transform.position - transform.position).normalized;
            movement.ApplyKnockback(dir);
        }
    }


    // -------------------------
    // УРОН ВРАГУ
    // -------------------------
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
            Die();
    }

    void Die()
    {
        var xp = Object.FindFirstObjectByType<PlayerExperience>();
        if (xp != null)
            xp.AddXP(xpReward);

        KillCounter.Instance?.AddKill();
        Destroy(gameObject);
    }
}
