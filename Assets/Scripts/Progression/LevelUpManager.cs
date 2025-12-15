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
        foreach (AbilityData data in possibleAbilities)
        {
            bool granted = AbilityGrantService.Instance.GrantAbility(data);
            if (granted)
            {
                Debug.Log("Выдана способность: " + data.name);
                return;
            }
        }

        Debug.Log("Нечего выдавать (все способности уже есть)");
    }


}
