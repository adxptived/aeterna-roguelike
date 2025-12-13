using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int xpAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerExperience xp = other.GetComponent<PlayerExperience>();
        if (xp != null)
        {
            xp.AddXP(xpAmount);
        }

        Destroy(gameObject);
    }
}
