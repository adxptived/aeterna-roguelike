using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int currentXP = 0;
    public int xpToNextLevel = 10;

    public float xpGrowthMultiplier = 1.4f;

    private PlayerStats stats;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }
    }

    void LevelUp()
    {
        stats.LevelUp();
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpGrowthMultiplier);
    }
}
