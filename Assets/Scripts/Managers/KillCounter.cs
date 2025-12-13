using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public static KillCounter Instance { get; private set; }

    [Header("Stats")]
    public int killedEnemies = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddKill()
    {
        killedEnemies++;
        //Debug.Log("Enemies killed: " + killedEnemies);
    }
}
