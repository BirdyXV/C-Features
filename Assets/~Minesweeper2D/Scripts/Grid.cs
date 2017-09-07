using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{
    public class Grid : MonoBehaviour
    {
        public GameObject tilePrefab;
        public int width = 10;
        public int height = 10;
        public float spacing = .155f;

        private Tile[,] tiles;

        void Start()
        {
            GenerateTiles();
        }
        // Functionality for spawning tiles
        Tile SpawnTile(Vector3 pos)
        {
            // Clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            clone.transform.position = pos; // Position tile
            Tile currentTile = clone.GetComponent<Tile>(); // Get Tile component
            return currentTile; // Return it
        }

        void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
                if (hit.collider != null)
                {
                    // LET tile = hit collider's Tile component
                    Tile tile = hit.collider.GetComponent<Tile>();
                    // IF tile != null
                    if (tile != null)
                    {
                        // LET adjacentMines = GetAdjacentMineCountAt(tile)
                        int adjacentMines = GetAdjacentMineCountAt(tile);
                        // CALL tile.Reveal(adjacentMines)
                        tile.Reveal(adjacentMines);
                    }

                }
            }
        }



        void GenerateTiles()
        {
            // Create new 2D array of size width x height
            tiles = new Tile[width, height];

            // Loop through the entire tile list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Store half size for later use
                    // Made it centre with a little math
                    Vector2 halfSize = new Vector2(width / 2.25f, height / 2.25f);

                    // Pivot tiles around Grid
                    Vector2 pos = new Vector2(x - halfSize.x,
                                              y - halfSize.y);

                    // Apply spacing
                    pos *= spacing;

                    // Spawn the tile
                    Tile tile = SpawnTile(pos);
                    // Attach newly spawned tile to 
                    tile.transform.SetParent(transform);
                    // Store it's arrary coordinates within itself for future reference
                    tile.x = x;
                    tile.y = y;
                    // Store tile in array at those coordinates
                    tiles[x, y] = tile;
                }
            }
        }

        public int GetAdjacentMineCountAt(Tile t)
        {
            int count = 0;
            // Loop through all elements and have each axis go between -1 to 1
            for (int x = -1; x <= 1; x++)
            {
                // Loop through all elements and have each axis go between -1 to 1
                for (int y = -1; y <= 1; y++)
                {
                    // Calculate desired coordinates from ones attained
                    int desiredX = t.x + x;
                    int desiredY = t.y + y;

                    // Check if desiredX and desiredY are greater than or equal to
                    if (desiredX >= 0 && desiredY >= 0 &&
                        desiredX < width && desiredY < height)
                    {
                        Tile tile = tiles[desiredX, desiredY];
                        if (tile.isMine)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
    }
}