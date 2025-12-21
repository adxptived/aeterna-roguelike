using UnityEngine;

public class AbilityGrantService : MonoBehaviour
{
    public static AbilityGrantService Instance;

    private AbilityHolder playerHolder;

    private void Start()
{
    Debug.Log("AbilityGrantService loaded");
}

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

        if (data is DoomCandlesData candles)
        {
            return playerHolder
                .TryAddActiveAbility<DoomCandlesAbility>(candles) != null;
        }

        if (data is FrostAuraData frost)
        {
            return playerHolder
                .TryAddActiveAbility<FrostAuraAbility>(frost) != null;
        }

        if (data is StoneSpikesData spikes)
        {
            return playerHolder
                .TryAddActiveAbility<StoneSpikesAbility>(spikes) != null;
        }
        if (data is AetherExpansionData expansion)
        {
            return playerHolder
                .TryAddPassiveAbility<AetherExpansionAbility>(expansion) != null;

        }

        if (data is ArcaneFocusData focus)
        {
            return playerHolder
                .TryAddPassiveAbility<ArcaneFocusAbility>(focus) != null;
        }

        if (data is VampirismData vamp)
        {
            return playerHolder
                .TryAddPassiveAbility<VampirismAbility>(vamp) != null;
        }





        Debug.LogWarning($"AbilityGrantService: unknown AbilityData {data.name}");
        return false;
    }

    public bool HasAbility(AbilityData data)
    {
        if (data is OrbitingBladesData)
            return playerHolder.GetComponent<OrbitingBladesAbility>() != null;

        if (data is FireMissileData)
            return playerHolder.GetComponent<FireMissileAbility>() != null;

        if (data is IceShardsData)
            return playerHolder.GetComponent<IceShardsAbility>() != null;
        
        if (data is DoomCandlesData)
            return playerHolder.GetComponent<DoomCandlesAbility>() != null;
        
        if (data is FrostAuraData)
            return playerHolder.GetComponent<FrostAuraAbility>() != null;
        
        if (data is StoneSpikesData)
            return playerHolder.GetComponent<StoneSpikesAbility>() != null;
            
        if (data is AetherExpansionData)
            return playerHolder.GetComponent<AetherExpansionAbility>() != null;

        if (data is ArcaneFocusData)
            return playerHolder.GetComponent<ArcaneFocusAbility>() != null;

        if (data is VampirismData)
            return playerHolder.GetComponent<VampirismAbility>() != null;

        return false;
    }


}
