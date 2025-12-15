using UnityEngine;

public class IceShardProjectile : MonoBehaviour
{
    private Vector2 direction;
    private int damage;
    private float speed;

    public void Init(Vector2 dir, int damage, float speed)
    {
        direction = dir.normalized;
        this.damage = damage;
        this.speed = speed;
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakeDamage(damage);

        // позже добавим замедление
        Destroy(gameObject);
    }
}
