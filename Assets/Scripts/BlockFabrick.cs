using UnityEngine;

public class BlockFabrick : MonoBehaviour
{
    #region Singleton

    public static BlockFabrick Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    #endregion

    [Header("Prefabs")]
    [SerializeField] private GameObject playerSpawnPointPrefab;
    [SerializeField] private GameObject enemySpawnPointPrefab;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject brickTilePrefab;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject boomPrefab;

    [Header("Parent")]
    [SerializeField] private Transform parentTransform;

    public GameObject GetBlock(BlockType blockType)
    {
        GameObject prefab;
        Vector3 startPosition = Vector3.zero;
        switch (blockType)
        {
            case BlockType.Enemy: prefab = enemySpawnPointPrefab; break;
            case BlockType.Player: prefab = playerSpawnPointPrefab; break;
            case BlockType.Brick: prefab = brickPrefab; break;
            case BlockType.BrickTile: prefab = brickTilePrefab; break;
            case BlockType.Tile: prefab = tilePrefab; break;
            case BlockType.Bullet: prefab = bulletPrefab; break;
            case BlockType.Boom: prefab = boomPrefab; break;
            default: prefab = null; break;
        }

        if (prefab)
        {
            prefab = Instantiate(prefab, startPosition, Quaternion.identity, parentTransform);
        }

        return prefab;
    }
    public GameObject GetBlock(BlockType blockType, Vector3 startPosition)
    {
        GameObject prefab;
        switch (blockType)
        {
            case BlockType.Enemy: prefab = enemySpawnPointPrefab; break;
            case BlockType.Player: prefab = playerSpawnPointPrefab; break;
            case BlockType.Brick: prefab = brickPrefab; break;
            case BlockType.BrickTile: prefab = brickTilePrefab; break;
            case BlockType.Tile: prefab = tilePrefab; break;
            case BlockType.Bullet: prefab = bulletPrefab; break;
            case BlockType.Boom: prefab = boomPrefab; break;
            default: prefab = null; break;
        }

        if (prefab)
        {
            prefab = Instantiate(prefab, startPosition, Quaternion.identity, parentTransform);
        }

        return prefab;
    }
}
