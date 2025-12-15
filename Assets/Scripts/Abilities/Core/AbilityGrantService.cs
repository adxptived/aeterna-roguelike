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

        // Orbiting Blades
        if (data is OrbitingBladesData blades)
        {
            return playerHolder
                .TryAddActiveAbility<OrbitingBladesAbility>(blades) != null;
        }

        // Fire Missile
        if (data is FireMissileData fire)
        {
            return playerHolder
                .TryAddActiveAbility<FireMissileAbility>(fire) != null;
        }

        // Ice Shards
        if (data is IceShardsData ice)
        {
            return playerHolder
                .TryAddActiveAbility<IceShardsAbility>(ice) != null;
        }

        Debug.LogWarning($"AbilityGrantService: unknown AbilityData {data.name}");
        return false;
    }
}
