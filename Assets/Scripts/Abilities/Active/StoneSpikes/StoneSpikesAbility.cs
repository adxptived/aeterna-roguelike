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
            Trigger();
            timer = data.cooldown;
        }
    }

    void Trigger()
    {
        // УРОН
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

        // ВИЗУАЛ
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
}
