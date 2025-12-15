using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public AbilityData[] possibleAbilities;

    private void OnEnable()
    {
        PlayerStats.OnLevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        PlayerStats.OnLevelUp -= OnLevelUp;
    }

    void OnLevelUp()
    {
        Debug.Log("СПОСОБНОСТЬ ВЫДАНА");

        if (possibleAbilities == null || possibleAbilities.Length == 0)
        {
            Debug.LogError("possibleAbilities пуст!");
            return;
        }

        AbilityGrantService.Instance
            .GrantAbility(possibleAbilities[0]);
    }

}
