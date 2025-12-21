using UnityEngine;

public abstract class AuraAbility : ActiveAbility
{
    protected float baseRadius;
    protected float baseTickInterval;

    protected float radius;
    protected float tickInterval;

    private float timer;

    protected override void Awake()
    {
        base.Awake();

        ApplyModifiers();

        if (modifiers != null)
            modifiers.OnModifiersChanged += ApplyModifiers;
    }

    protected virtual void OnDestroy()
    {
        if (modifiers != null)
            modifiers.OnModifiersChanged -= ApplyModifiers;
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

    protected abstract void ApplyEffect(Enemy enemy);

    protected void ApplyModifiers()
    {
        if (modifiers == null)
        {
            radius = baseRadius;
            tickInterval = baseTickInterval;
            return;
        }

        radius = baseRadius * modifiers.auraRadiusMultiplier;
        tickInterval = baseTickInterval * modifiers.auraTickMultiplier;
    }
}
