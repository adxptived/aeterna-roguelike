using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        AbilityGrantService.Instance.RegisterPlayer(player);
    }
}
