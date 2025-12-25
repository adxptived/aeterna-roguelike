using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{

    private PlayerStats playerStats;


    [Header("Abilities Pool")]
    public AbilityData[] possibleAbilities;

    // 🔁 pity-счётчик
    private int levelsWithoutEpic = 0;

    private void OnEnable()
    {
        playerStats = FindAnyObjectByType<PlayerStats>();
        PlayerStats.OnLevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        PlayerStats.OnLevelUp -= OnLevelUp;
    }

    void OnLevelUp()
    {
        int newLevel = playerStats.level;
        // 1️⃣ формируем базовый пул
        List<AbilityData> basePool = new();

        foreach (AbilityData data in possibleAbilities)
        {
            if (!data.canDropFromLevelUp)
                continue;

            if (AbilityGrantService.Instance.HasAbility(data))
                continue;

            basePool.Add(data);
        }

        if (basePool.Count == 0)
            return;

        // 2️⃣ проверяем pity (строго с 3 уровня)
        bool forceEpic = false;

        if (newLevel >= 3)
        {
            float epicChance = GetEpicChance(levelsWithoutEpic);
            float roll = Random.value;

            if (roll <= epicChance)
            {
                forceEpic = true;
            }
        }

        // 3️⃣ выбираем 3 карточки
        List<AbilityData> result = new();
        List<AbilityData> tempPool = new(basePool);

        int forcedEpicSlot = forceEpic ? Random.Range(0, 3) : -1;

        for (int slot = 0; slot < 3; slot++)
        {
            AbilityData chosen;

            if (slot == forcedEpicSlot)
            
            {
                chosen = GetRandomByRarity(tempPool, AbilityRarity.Epic);
            }
            else
            {
                chosen = GetRandomByRarityWeighted(tempPool);
            }

            if (chosen == null)
                continue;

            result.Add(chosen);
            tempPool.Remove(chosen);
        }

        // 4️⃣ обновляем pity
        bool epicAppeared = false;

        foreach (AbilityData a in result)
        {
            if (a.rarity == AbilityRarity.Epic)
            {
                epicAppeared = true;
                break;
            }
        }

        if (epicAppeared)
            levelsWithoutEpic = 0;
        else if (newLevel >= 3)
            levelsWithoutEpic++;

        // 5️⃣ показываем UI
        LevelUpUI.Instance.Show(result);
    }

    // ===========================
    // 🎯 PITY TABLE
    // ===========================
    float GetEpicChance(int noEpicLevels)
    {
        return noEpicLevels switch
        {
            0 => 0.05f,
            1 => 0.08f,
            2 => 0.12f,
            3 => 0.18f,
            4 => 0.26f,
            5 => 0.38f,
            6 => 0.55f,
            7 => 0.75f,
            _ => 0.95f
        };
    }

    // ===========================
    // 🎲 RANDOM BY RARITY
    // ===========================
    AbilityData GetRandomByRarity(List<AbilityData> pool, AbilityRarity rarity)
    {
        List<AbilityData> filtered = pool.FindAll(a => a.rarity == rarity);

        if (filtered.Count == 0)
            return null;

        return GetWeightedRandom(filtered);
    }

    // ===========================
    // 🎲 RANDOM WITH RARITY + WEIGHT
    // ===========================
    AbilityData GetRandomByRarityWeighted(List<AbilityData> pool)
    {
        Dictionary<AbilityRarity, int> rarityWeights = new()
        {
            { AbilityRarity.Common, 55 },
            { AbilityRarity.Uncommon, 25 },
            { AbilityRarity.Rare, 12 },
            { AbilityRarity.Epic, 6 },
            { AbilityRarity.Mythic, 2 }
        };

        AbilityRarity chosenRarity = GetWeightedRandomRarity(rarityWeights);

        List<AbilityData> filtered = pool.FindAll(a => a.rarity == chosenRarity);

        if (filtered.Count == 0)
            return GetWeightedRandom(pool); // fallback

        return GetWeightedRandom(filtered);
    }

    // ===========================
    // 🎲 WEIGHT HELPERS
    // ===========================
    AbilityRarity GetWeightedRandomRarity(Dictionary<AbilityRarity, int> weights)
    {
        int total = 0;
        foreach (var w in weights.Values)
            total += w;

        int roll = Random.Range(0, total);
        int current = 0;

        foreach (var pair in weights)
        {
            current += pair.Value;
            if (roll < current)
                return pair.Key;
        }

        return AbilityRarity.Common;
    }

    AbilityData GetWeightedRandom(List<AbilityData> list)
    {
        int total = 0;
        foreach (var a in list)
            total += Mathf.Max(1, a.weight);

        int roll = Random.Range(0, total);
        int current = 0;

        foreach (var a in list)
        {
            current += Mathf.Max(1, a.weight);
            if (roll < current)
                return a;
        }

        return list[0];
    }
    
    void LogAbilityChances(List<AbilityData> abilities)
    {
        int totalWeight = 0;
        foreach (var a in abilities)
            totalWeight += a.weight;

        Debug.Log("📊 ABILITY CHANCES:");

        foreach (var a in abilities)
        {
            float chance = (float)a.weight / totalWeight * 100f;
            Debug.Log($"{a.abilityName} [{a.rarity}] → {chance:F1}%");
        }
    }

}
