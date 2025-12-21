using UnityEngine;
using System.Collections.Generic;

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
        List<AbilityData> available = new();

        // 1. Убираем уже полученные способности
        foreach (AbilityData data in possibleAbilities)
        {
            if (!AbilityGrantService.Instance.HasAbility(data))
            {
                available.Add(data);
            }
        }

        // ❌ Нечего показывать
        if (available.Count == 0)
        {
            Debug.Log("Нет доступных способностей");
            return;
        }

        // 2. Перемешиваем список
        Shuffle(available);

        // 3. Берём первые 3 (или меньше)
        List<AbilityData> options = new();
        int count = Mathf.Min(3, available.Count);

        for (int i = 0; i < count; i++)
        {
            options.Add(available[i]);
        }

        LevelUpUI.Instance.Show(options);
    }

    // Fisher–Yates shuffle
    void Shuffle(List<AbilityData> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
