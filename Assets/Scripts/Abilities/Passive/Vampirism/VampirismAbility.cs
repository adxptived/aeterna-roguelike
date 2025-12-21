using UnityEngine;

public class VampirismAbility : PassiveAbility, IAbilityWithData
{
    private VampirismData data;
    private PlayerStats player;

    public void Init(AbilityData baseData)
    {
        data = (VampirismData)baseData;
        player = GetComponent<PlayerStats>();

        DamageEvents.OnDamageDealt += OnDamageDealt;
    }

    private void OnDestroy()
    {
        DamageEvents.OnDamageDealt -= OnDamageDealt;
    }

    void OnDamageDealt(int damage)
    {
        if (player == null) return;

        int heal = Mathf.RoundToInt(damage * data.lifeStealPercent);
        if (heal > 0)
        {
            player.Heal(heal);
        }
    }
}
