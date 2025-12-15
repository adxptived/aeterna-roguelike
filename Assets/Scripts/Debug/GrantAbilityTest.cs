using UnityEngine;

public class GrantAbilityTest : MonoBehaviour
{
    public AbilityData abilityToGrant;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AbilityGrantService.Instance
                .GrantAbility(abilityToGrant);
        }
    }
}
