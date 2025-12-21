using UnityEngine;
using TMPro;

public class AbilityCardUI : MonoBehaviour
{
    public TMP_Text titleText;

    private AbilityData data;

    public void Setup(AbilityData abilityData)
    {
        data = abilityData;
        titleText.text = abilityData.abilityName;
    }

    public void OnClick()
    {
        AbilityGrantService.Instance.GrantAbility(data);
        
        LevelUpUI.Instance.Close();
    }
}
