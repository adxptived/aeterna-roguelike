using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [Header("Tilemaps")]
    public Tilemap groundTilemap;
    public Tilemap obstacleTilemap;   // деревья сюда (непроходимые)
    public Tilemap decorTilemap;      // камни сюда (проходимые)

    [Header("Ground Tiles (Biomes)")]
    public TileBase grassTile;
    public TileBase forestTile;
    public TileBase rockyTile;
    public TileBase roadTile;

    [Header("Forest Objects")]
    public TileBase treeSmall;
    public TileBase treeMedium;
    public TileBase treeLarge;

    [Header("Rock Objects")]
    public TileBase rockTile;

    [Header("Map Settings")]
    public int mapWidth = 50;
    public int mapHeight = 50;

    [Header("Noise Settings")]
    [Tooltip("Чем больше - тем крупнее области биомов (меньше 'островков'). Рекомендуем 15-40")]
    public float biomeNoiseScale = 15f;
    public int seed = 0;

    [Header("Decoration Density")]
    public int forestTreeDensity = 400; // ~1 дерево на 20x20
    public int rockyRockDensity = 133;  // ~3 камня на 20x20

    [Header("Biome tuning (make forest larger, rocky smaller)")]
    [Range(0f, 1f)]
    public float grassThreshold = 0.25f;   // всё ниже = трава
    [Range(0f, 1f)]
    public float forestThreshold = 0.70f;  // между grassThreshold и forestThreshold = лес
    

    private void Start()
    {
        GenerateRandomSeed();
        GenerateMap();
    }

    // кнопка в меню Unity
    [ContextMenu("Generate Random Map")]
    public void GenerateRandomMap()
    {
        GenerateRandomSeed();
        GenerateMap();
    }

    void GenerateRandomSeed()
    {
        seed = Random.Range(0, 999999);
    }

    [ContextMenu("Generate Map")]
    public void GenerateMap()
    {
        ClearMap();
        GenerateBiomes();
        GenerateForestObjects();
        GenerateRockObjects();

        Debug.Log("Map generated with seed: " + seed);
    }

    [ContextMenu("Clear Map")]
    public void ClearMap()
    {
        groundTilemap.ClearAllTiles();
        obstacleTilemap.ClearAllTiles();
        decorTilemap.ClearAllTiles();
    }

    // --------------------------------------------------------------------
    // BIOMES
    // --------------------------------------------------------------------
    private void GenerateBiomes()
    {
        float offsetX = seed * 1.3f;
        float offsetY = seed * 2.1f;

        for (int x = -mapWidth / 2; x < mapWidth / 2; x++)
        {
            for (int y = -mapHeight / 2; y < mapHeight / 2; y++)
            {
                float noise = Mathf.PerlinNoise(
                    (x + offsetX) / biomeNoiseScale,
                    (y + offsetY) / biomeNoiseScale
                );

                TileBase chosenTile;

                if (noise < grassThreshold)
                    chosenTile = grassTile;
                else if (noise < forestThreshold)
                    chosenTile = forestTile;
                else
                    chosenTile = rockyTile;

                groundTilemap.SetTile(new Vector3Int(x, y, 0), chosenTile);
            }
        }
    }

    // --------------------------------------------------------------------
    // FOREST OBJECTS (Trees)
    // --------------------------------------------------------------------
    void GenerateForestObjects()
    {
        int totalTiles = mapWidth * mapHeight;
        int targetTrees = totalTiles / forestTreeDensity;

        for (int i = 0; i < targetTrees; i++)
        {
            Vector3Int pos = GetRandomGroundCellOfType(forestTile);
            if (pos == Vector3Int.zero) continue;

            TileBase chosenTree = RandomTree();
            obstacleTilemap.SetTile(pos, chosenTree); // непроходимо
        }
    }

    TileBase RandomTree()
    {
        int r = Random.Range(0, 3);
        if (r == 0) return treeSmall;
        if (r == 1) return treeMedium;
        return treeLarge;
    }

    // --------------------------------------------------------------------
    // ROCK OBJECTS (Decor, passable)
    // --------------------------------------------------------------------
    void GenerateRockObjects()
    {
        int totalTiles = mapWidth * mapHeight;
        int targetRocks = totalTiles / rockyRockDensity;

        for (int i = 0; i < targetRocks; i++)
        {
            Vector3Int pos = GetRandomGroundCellOfType(rockyTile);
            if (pos == Vector3Int.zero) continue;

            decorTilemap.SetTile(pos, rockTile); // камни – проходимые
        }
    }

    // --------------------------------------------------------------------
    // UTILS
    // --------------------------------------------------------------------
    Vector3Int GetRandomGroundCellOfType(TileBase type)
    {
        for (int attempt = 0; attempt < 20; attempt++)
        {
            int x = Random.Range(-mapWidth / 2, mapWidth / 2);
            int y = Random.Range(-mapHeight / 2, mapHeight / 2);
            Vector3Int p = new Vector3Int(x, y, 0);

            if (groundTilemap.GetTile(p) == type)
                return p;
        }
        return Vector3Int.zero;
    }

    public bool IsWalkable(Vector3 worldPos)
    {
        Vector3Int cell = obstacleTilemap.WorldToCell(worldPos);
        return obstacleTilemap.GetTile(cell) == null;
    }
}
