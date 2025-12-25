using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityCardUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text titleText;
    public Image frameImage; // ← Image рамки

    private AbilityData data;

    public void Setup(AbilityData abilityData)
    {
        data = abilityData;

        titleText.text = abilityData.abilityName;

        // 🎨 цвет рамки по редкости
        if (frameImage != null)
            frameImage.color = RarityColors.Get(abilityData.rarity);
    }

    public void OnClick()
    {
        AbilityGrantService.Instance.GrantAbility(data);
        LevelUpUI.Instance.Close();
    }
}
