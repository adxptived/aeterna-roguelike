using UnityEngine;

public class IceShardsAbility : ActiveAbility, IAbilityWithData
{
    private IceShardsData data;
    private float timer;

    public void Init(AbilityData abilityData)
    {
        data = abilityData as IceShardsData;
        timer = data.cooldown;
    }

    private void Update()
    {
        if (data == null) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Cast();
            timer = data.cooldown;
        }
    }

    void Cast()
    {
        float startAngle = -data.spreadAngle / 2f;
        float angleStep = data.spreadAngle / (data.shardCount - 1);

        for (int i = 0; i < data.shardCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

            GameObject shard = Instantiate(
                data.shardPrefab,
                transform.position,
                Quaternion.identity
            );

            shard.GetComponent<IceShardProjectile>()
                .Init(dir, data.damage, data.speed);
        }
    }
}
