using UnityEngine;
using System.Collections.Generic;

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

    // =========================
    // STATUS EFFECTS
    // =========================

    // ---- SLOW ----
    private float slowMultiplier = 1f;
    private float slowTimer = 0f;

    // ---- DAMAGE OVER TIME ----
    [System.Serializable]
    private class DotEffect
    {
        public int damagePerTick;
        public float tickInterval;
        public float duration;

        public float timer;
        public float tickTimer;
    }

    private readonly List<DotEffect> poisonEffects = new();
    private readonly List<DotEffect> burnEffects = new();
    private readonly List<DotEffect> bleedEffects = new();

    // =========================

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
        // -------------------------
        // STATUS EFFECTS UPDATE
        // -------------------------

        UpdateSlow();
        UpdateDots(poisonEffects);
        UpdateDots(burnEffects);
        UpdateDots(bleedEffects);

        // -------------------------
        // KNOCKBACK
        // -------------------------

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

    // =========================
    // MOVEMENT
    // =========================

    void MoveToPlayer()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed * slowMultiplier;
    }

    // =========================
    // CONTACT DAMAGE
    // =========================

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
            damageTimer = 0f;
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

    // =========================
    // DAMAGE
    // =========================

    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        DamageEvents.RaiseDamageDealt(dmg);

        if (hp <= 0)
            Die();
    }

    // =========================
    // KNOCKBACK
    // =========================

    public void ApplyKnockback(Vector2 direction)
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
    }

    // =========================
    // STATUS EFFECTS API
    // =========================

    // ---- SLOW ----
    void UpdateSlow()
    {
        if (slowTimer > 0f)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0f)
                slowMultiplier = 1f;
        }
    }

    public void ApplySlow(float multiplier, float duration)
    {
        if (multiplier < slowMultiplier)
            slowMultiplier = multiplier;

        slowTimer = Mathf.Max(slowTimer, duration);
    }

    // ---- DOTS (POISON / BURN / BLEED) ----
    void UpdateDots(List<DotEffect> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            DotEffect dot = list[i];

            dot.timer -= Time.deltaTime;
            dot.tickTimer -= Time.deltaTime;

            if (dot.tickTimer <= 0f)
            {
                TakeDamage(dot.damagePerTick);
                dot.tickTimer = dot.tickInterval;
            }

            if (dot.timer <= 0f)
            {
                list.RemoveAt(i);
            }
        }
    }

    public void ApplyPoison(int damagePerTick, float duration, float tickInterval)
    {
        AddDot(poisonEffects, damagePerTick, duration, tickInterval);
    }

    public void ApplyBurn(int damagePerTick, float duration, float tickInterval)
    {
        AddDot(burnEffects, damagePerTick, duration, tickInterval);
    }

    public void ApplyBleed(int damagePerTick, float duration, float tickInterval)
    {
        AddDot(bleedEffects, damagePerTick, duration, tickInterval);
    }

    void AddDot(List<DotEffect> list, int damage, float duration, float interval)
    {
        DotEffect dot = new DotEffect
        {
            damagePerTick = damage,
            duration = duration,
            tickInterval = interval,
            timer = duration,
            tickTimer = interval
        };

        list.Add(dot);
    }

    // =========================
    // DEATH
    // =========================

    void Die()
    {
        if (xpOrbPrefab != null)
        {
            for (int i = 0; i < xpReward; i++)
            {
                Vector2 offset = Random.insideUnitCircle * 0.5f;
                Instantiate(
                    xpOrbPrefab,
                    transform.position + (Vector3)offset,
                    Quaternion.identity
                );
            }
        }

        KillCounter.Instance?.AddKill();
        Destroy(gameObject);
    }
}
