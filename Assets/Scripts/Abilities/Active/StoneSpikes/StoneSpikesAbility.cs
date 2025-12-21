using UnityEngine;

public class StoneSpikesAbility : ActiveAbility, IAbilityWithData
{
    private StoneSpikesData data;
    private float timer;

    public void Init(AbilityData baseData)
    {
        data = (StoneSpikesData)baseData;
        timer = data.cooldown;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            TriggerSpikes();
            timer = data.cooldown;
        }
    }

    void TriggerSpikes()
    {
        // 1. Наносим урон
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            data.radius
        );

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(data.damage);
            }
        }

        // 2. ВИЗУАЛЬНЫЕ СПАЙКИ
        SpawnVisualSpikes();
    }

    void SpawnVisualSpikes()
    {
        if (data.spikePrefab == null) return;

        float angleStep = 360f / data.spikeCount;

        for (int i = 0; i < data.spikeCount; i++)
        {
            float angle = angleStep * i;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

            Vector3 pos = transform.position + (Vector3)(dir * data.radius);

            GameObject spike = Instantiate(
                data.spikePrefab,
                pos,
                Quaternion.identity
            );

            Destroy(spike, data.spikeLifetime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, data.radius);
    }
}
