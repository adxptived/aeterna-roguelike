using UnityEngine;

public class Blade : MonoBehaviour
{
    public int damage = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("blade strikes");
            enemy.TakeDamage(damage);
        }
    }
}
