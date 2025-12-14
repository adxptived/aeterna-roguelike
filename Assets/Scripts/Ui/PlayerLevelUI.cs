using UnityEngine;
using TMPro;

public class PlayerLevelUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public TMP_Text levelText;

    private int lastLevel = -1;

    private void Update()
    {
        if (playerStats == null || levelText == null) return;

        if (playerStats.level != lastLevel)
        {
            lastLevel = playerStats.level;
            levelText.text = playerStats.level.ToString();
            
        }
    }
}
