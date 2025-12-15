using UnityEngine;

public class DamageAuraAbility : ActiveAbility
{
    [Header("Aura Settings")]
    public float radius = 2.5f;
    public int damage = 5;
    public float tickInterval = 1f;

    private float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            DealDamage();
            timer = tickInterval;
        }
    }

    void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            radius
        );

        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("DAMAGE AURA IS DAMAGING");
                enemy.TakeDamage(damage);
            }
        }
    }

    // Визуализация радиуса в редакторе
    private void OnDrawGizmosSelected()
    {
        Debug.Log("DRAW");
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
