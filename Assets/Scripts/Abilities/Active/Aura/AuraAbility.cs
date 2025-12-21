using UnityEngine;

public abstract class AuraAbility : ActiveAbility
{
    [Header("Aura Settings")]
    protected float radius;
    protected float tickInterval = 1f;

    private float timer;

    protected virtual void Start()
    {
        timer = 0f;
    }

    protected virtual void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ApplyAura();
            timer = tickInterval;
        }
    }

    public void ModifyRadius(float multiplier)
    {
        radius *= multiplier;
    }

    public void ModifyTickInterval(float multiplier)
    {
        tickInterval *= multiplier;
    }



    void ApplyAura()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            radius
        );

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                ApplyEffect(enemy);
            }
        }
    }

    /// <summary>
    /// Что аура делает с конкретным врагом
    /// </summary>
    protected abstract void ApplyEffect(Enemy enemy);

    // 🧪 Для удобства отладки
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
