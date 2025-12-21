using UnityEngine;

public class AbilityGrantService : MonoBehaviour
{
    public static AbilityGrantService Instance;

    private AbilityHolder playerHolder;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterPlayer(GameObject player)
    {
        playerHolder = player.GetComponent<AbilityHolder>();
    }

    public bool GrantAbility(AbilityData data)
    {
        if (data == null)
            return false;

        if (data is not IAbilityFactory factory)
        {
            Debug.LogError($"AbilityData {data.name} does not implement IAbilityFactory");
            return false;
        }

        return factory.Grant(playerHolder);
    }

    public bool HasAbility(AbilityData data)
    {
        if (data is not IAbilityFactory factory)
            return false;

        return factory.Has(playerHolder);
    }
}
