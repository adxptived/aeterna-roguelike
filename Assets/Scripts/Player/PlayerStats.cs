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

    public event Action OnDeath;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Took damage");
        int finalDamage = Mathf.Max(damage - armor, 1);
        currentHP -= finalDamage;

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
        maxHP += 10;
        currentHP = maxHP;
        Debug.Log("LEVEL UP! Level: " + level);
    }
}
