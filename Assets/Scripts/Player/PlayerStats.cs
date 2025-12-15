using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public int maxHP = 100;
    public int armor = 0;
    public float moveSpeed = 5f;

    [Header("Level")]
    public int level = 1;

    [Header("Current HP")]
    [SerializeField] private int currentHP;

    public int CurrentHP => currentHP;

    [Header("Invulnerability")]
    public float invulnerabilityDuration = 0.3f;

    private bool isInvulnerable;
    private float invulnerabilityTimer;


    public event Action OnDeath;

    //LEVEL UP upgrade
    public static event Action OnLevelUp;

    //Мигание легкое
    private SpriteRenderer spriteRenderer;

    ///------------------------------------------------------------------------------

    private void Start()
    {


    }


    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isInvulnerable) return;

        invulnerabilityTimer -= Time.deltaTime;
        if (invulnerabilityTimer <= 0f)
        {
            isInvulnerable = false;
        }
        
        if (isInvulnerable && spriteRenderer != null)
        {
            float alpha = Mathf.PingPong(Time.time * 10f, 1f);
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
        }
        else if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
            }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        int finalDamage = Mathf.Max(damage - armor, 1);
        currentHP -= finalDamage;
        Debug.Log("Took damage");
        StartInvulnerability();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(int value)
    {
        currentHP = Mathf.Min(currentHP + value, maxHP);
    }

    void Die()
    {
        Debug.Log("Player died");
        OnDeath?.Invoke();
    }

    public void LevelUp()
    {
        level++;
        OnLevelUp?.Invoke();
        maxHP += 10;
        currentHP = maxHP;
        Debug.Log("LEVEL UP! Level: " + level);
    }

    void StartInvulnerability()
    {
        isInvulnerable = true;
        invulnerabilityTimer = invulnerabilityDuration;
    }


}
