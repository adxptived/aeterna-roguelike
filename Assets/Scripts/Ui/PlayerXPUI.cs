using UnityEngine;
using UnityEngine.UI;

public class PlayerXPUI : MonoBehaviour
{
    public PlayerExperience playerXP;

    public Image xpFill;    // основная полоса
    public Image xpDelay;   // задержанная

    public float delaySpeed = 1.5f;

    private void Update()
    {
        if (playerXP == null) return;

        float target = (float)playerXP.currentXP / playerXP.xpToNextLevel;

        // Fill — сразу
        xpFill.fillAmount = target;

        // Delay — ВСЕГДА догоняет
        xpDelay.fillAmount = Mathf.MoveTowards(
            xpDelay.fillAmount,
            target,
            delaySpeed * Time.deltaTime
        );
    }
}
