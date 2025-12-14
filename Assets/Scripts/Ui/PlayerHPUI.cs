using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHPUI : MonoBehaviour
{
    [Header("References")]
    public PlayerStats playerStats;

    public Image hpFill;     // зелёная
    public Image hpDelay;    // задержанная
    public TMP_Text hpText;

    [Header("Delay Settings")]
    public float delaySpeed = 0.5f;

    private void Update()
    {
        if (playerStats == null) return;

        float targetFill = (float)playerStats.CurrentHP / playerStats.maxHP;

        // Мгновенное HP
        hpFill.fillAmount = targetFill;

        // Delay HP — догоняет
        if (hpDelay.fillAmount > targetFill)
        {
            hpDelay.fillAmount = Mathf.MoveTowards(
                hpDelay.fillAmount,
                targetFill,
                delaySpeed * Time.deltaTime
            );
        }
        else
        {
            hpDelay.fillAmount = targetFill;
        }

        if (hpText != null)
            hpText.text = $"{playerStats.CurrentHP} / {playerStats.maxHP}";
    }
}
