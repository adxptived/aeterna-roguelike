using UnityEngine;
using System.Collections.Generic;

public class LevelUpUI : MonoBehaviour
{
    public static LevelUpUI Instance;

    public GameObject panel;
    public Transform cardsContainer;
    public AbilityCardUI cardPrefab;

    private void Awake()
    {
        Debug.Log("LevelUpUI Awake");
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(List<AbilityData> options)
    {
        Time.timeScale = 0f;
        panel.SetActive(true);

        foreach (Transform child in cardsContainer)
            Destroy(child.gameObject);

        foreach (AbilityData data in options)
        {
            AbilityCardUI card = Instantiate(cardPrefab, cardsContainer);
            card.Setup(data);
        }
    }

    public void Close()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
