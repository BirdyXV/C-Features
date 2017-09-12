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

        public void FFuncover(int x, int y, bool[,] visited)
        {
            // IF x >= 0 AND y >= 0 AND x < width AND y < height
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                // IF visited[x, y]
                if (visited[x, y])
                {
                    // RETURN
                    return;
                }

                // LET tile = tiles [x, y]
                Tile tile = tiles[x, y];
                // LET adjacentMines = GetAdjacentMineCountAt(tile)
                int adjacentMines = GetAdjacentMineCountAt(tile);
                // CALL tile.Reveal(adjacentMines)
                tile.Reveal(adjacentMines);
                // IF adjacentMines > 0
                if (adjacentMines > 0)
                {
                    // RETURN
                    return;
                }

                // SET visited [x, y] = true
                visited[x, y] = true;

                // CALL FFuncover(x - 1, y, visited)
                FFuncover(x - 1, y, visited);
                // CALL FFuncover(x + 1, y, visited)
                FFuncover(x + 1, y, visited);
                // CALL FFuncover(x, y - 1, visited)
                FFuncover(x, y - 1, visited);
                // CALL FFuncover(x, y + 1, visited)
                FFuncover(x, y + 1, visited);
            }


        }

        public void UncoverMines(int mineState)
        {
            // FOR x = 0 to x < width
            for (int x = 0; x < width; x++)
            {
                // FOR y = 0 to y < height
                for (int y = 0; y < height; y++)
                {
                    // LET currentTile = tiles[x, y]
                    Tile currentTile = tiles[x, y];
                    // IF currentTile isMine
                    if (currentTile.isMine)
                    {
                        // LET adjacentMines = GetAdjacentMineCountAt(currentTile)
                        int adjacentMines = GetAdjacentMineCountAt(currentTile);
                        // CALL currentTile.Reveal(adjacentMines, mineState)
                        currentTile.Reveal(adjacentMines, mineState);
                    }
                }
            }
        }

        // Detects if there are no more empty tiles in the game
        bool NoMoreEmptyTiles()
        {
            // LET emptyTileCount = 0
            int emptyTileCount = 0;
            // FOR x = 0 to x < width
            for (int x = 0; x < width; x++)
            {
                // FOR y = 0 to y < height
                for (int y = 0; y < height; y++)
                {
                    // LET currentTile = tiles[x, y]
                    Tile currentTile = tiles[x, y];
                    // IF !currentTile.isRevealed AND !currentTile.isMine
                    if (!currentTile.isRevealed && !currentTile.isMine)
                    {
                        // SET emptyTileCount = emptyTileCount + 1
                        emptyTileCount = emptyTileCount + 1;
                    }
                }
            }
            return emptyTileCount == 0;
        }

        public void SelectTile(Tile selectedTile)
        {
            // LET adjacentMines = GetAdjacentMineCountAt(selectedTile)
            int adjacentMines = GetAdjacentMineCountAt(selectedTile);
            // CALL selectedTile.Reveal(adjacentMines)
            selectedTile.Reveal(adjacentMines);
            // IF selectedTile isMine
            if (selectedTile.isMine)
            {
                // CALL UncoverMines(0)
                UncoverMines(0);
                // [EXTRA] Perform Game over logic                
            }
            // ELSEIF adjacentMines == 0
            else if (adjacentMines == 0)
            {
                // LET x = selectedTile.x
                int x = selectedTile.x;
                // LET x = selectedTile.y
                int y = selectedTile.y;
                // CALL FFuncover(x, y, new bool[width, height])
                FFuncover(x, y, new bool[width, height]);
            }
            // IF NoMoreEmptyTiles()
            if (NoMoreEmptyTiles())
            {
                // CALL UncoverMines(1)
                // [EXTRA] Perform Win Logic
            }
        }

        void Update()
        {
            // IF Mouse Button 0 is Down
            if (Input.GetMouseButtonDown(0))
            {
                // LET ray = Ray from camera using Input.mousePosition
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // LET hit = Physics2D RayCast (ray.origin, ray.direction)
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                // IF hit's collider != null
                if (hit.collider != null)
                {
                    // LET hitTile = hit collider's Tile component
                    Tile hitTile = hit.collider.GetComponent<Tile>();
                    // IF hitTile != null
                    if (hitTile != null)
                    {
                        // CALL SelectedTile(hitTile)
                        SelectTile(hitTile);
                    }
                }
            }
        }
    }
}