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
        if (data is OrbitingBladesData blades)
        {
            return playerHolder
                .TryAddActiveAbility<OrbitingBladesAbility>(blades) != null;
        }

        return false;
        
    }
}
