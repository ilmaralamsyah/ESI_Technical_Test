using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    [SerializeField] private GameObject evenTilePrefab;
    [SerializeField] private GameObject oddTilePrefab;
    [SerializeField] private int gridSize = 50;

    private Vector2Int currentGridPosition;
    private List<GameObject> tilePool = new List<GameObject>();

    void Start()
    {
        InitializeTilePool();
        UpdateGridAroundPlayer();
    }

    void Update()
    {
        Vector2Int playerGridPosition = GetGridPosition(Player.Instance.transform.position);

        if (playerGridPosition != currentGridPosition)
        {
            currentGridPosition = playerGridPosition;
            UpdateGridAroundPlayer();
        }
    }

    private void InitializeTilePool()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject tile = Instantiate((i % 2 == 0) ? evenTilePrefab : oddTilePrefab, transform);
            tile.SetActive(false); // Mulai dengan tile yang tidak aktif
            tilePool.Add(tile);
        }
    }

    private Vector2Int GetGridPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt((position.x + gridSize / 2) / gridSize);
        int y = Mathf.FloorToInt((position.y + gridSize / 2) / gridSize);
        return new Vector2Int(x, y);
    }

    private void UpdateGridAroundPlayer()
    {
        int tileIndex = 0;

        // Loop untuk membuat 3x3 grid di sekitar player
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int gridPosition = currentGridPosition + new Vector2Int(x, y);
                Vector3 spawnPosition = new Vector3(gridPosition.x * gridSize, gridPosition.y * gridSize, 0);

                GameObject tile = tilePool[tileIndex];
                tile.SetActive(true);  // Aktifkan tile dari pool

                // Tentukan apakah tile genap atau ganjil
                GameObject tilePrefab = (gridPosition.x + gridPosition.y) % 2 == 0 ? evenTilePrefab : oddTilePrefab;
                if (tilePrefab == evenTilePrefab && tile.name != evenTilePrefab.name)
                {
                    tile.GetComponent<SpriteRenderer>().sprite = evenTilePrefab.GetComponent<SpriteRenderer>().sprite;
                    tile.name = evenTilePrefab.name;
                }
                else if (tilePrefab == oddTilePrefab && tile.name != oddTilePrefab.name)
                {
                    tile.GetComponent<SpriteRenderer>().sprite = oddTilePrefab.GetComponent<SpriteRenderer>().sprite;
                    tile.name = oddTilePrefab.name;
                }

                // Pindahkan tile ke posisi yang tepat
                tile.transform.position = spawnPosition;

                tileIndex++;
            }
        }
    }
}
