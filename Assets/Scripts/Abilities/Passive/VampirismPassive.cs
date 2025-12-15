using UnityEngine;

public class VampirismPassive : PassiveAbility
{
    [Header("Vampirism")]
    [Range(0f, 1f)]
    public float lifeStealPercent = 0.1f; // 10%

    private PlayerStats stats;

    private void OnEnable()
    {
        stats = GetComponent<PlayerStats>();
        DamageEvents.OnDamageDealt += OnDamageDealt;
    }

    private void OnDisable()
    {
        DamageEvents.OnDamageDealt -= OnDamageDealt;
    }

    void OnDamageDealt(int damage)
    {
        if (stats == null) return;

        int healAmount = Mathf.CeilToInt(damage * lifeStealPercent);
        stats.Heal(healAmount);
    }
}
