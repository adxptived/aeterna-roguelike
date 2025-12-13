using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnRadius = 8f;

    private float timer;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        Vector2 offset = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 pos = player.position + (Vector3)offset;

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}
