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

    [Header("Knockback")]
    public float knockbackForce = 4f;
    public float knockbackDuration = 0.15f;

    private Rigidbody2D rb;
    private bool isKnockedBack;
    private float knockbackTimer;

    [Header("Drops")]
    public GameObject xpOrbPrefab;



    private void Awake()
    {
        hp = maxHP;
        rb = GetComponent<Rigidbody2D>();


        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerStats = playerObj.GetComponent<PlayerStats>();
        }
    }

    private void Update()
    {
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
            }
            return;
        }

        MoveToPlayer();
    }


    void MoveToPlayer()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
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

    //KNOCKBACK
    public void ApplyKnockback(Vector2 direction)
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
    }


    void Die()
    {
        if (xpOrbPrefab != null)
        {
            for (int i = 0; i < xpReward; i++)
            {
                Vector2 offset = Random.insideUnitCircle * 0.5f;
                Instantiate(xpOrbPrefab, transform.position + (Vector3)offset, Quaternion.identity);
            }
        }

        KillCounter.Instance?.AddKill();
        Destroy(gameObject);
    }

}
