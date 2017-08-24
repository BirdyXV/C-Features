using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Breakout
{
    public class GameManager : MonoBehaviour
    {
        public int width = 20;
        public int height = 20;
        public Vector2 spacing = new Vector2(25f, 10f);
        public Vector2 offset = new Vector2(25f, 0f);
        public GameObject[] blockPrefab;
        [Header("Debug")]
        public bool isDebugging = false;

        private GameObject[,] spawnedBlocks;
        // Use this for initialization
        void Start()
        {
            GenerateBlocks();
            UpdateBlocks();
        }


        GameObject GetRandomBlock()
        {
            // Randomly spawn a new GameObject
            int randomIndex = Random.Range(0, blockPrefab.Length);
            GameObject randomPrefab = blockPrefab[randomIndex];
            GameObject clone = Instantiate(randomPrefab);
            return clone;
        }

        void GenerateBlocks()
        {
            spawnedBlocks = new GameObject[width, height];
            // Loop through the width 
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Create new instance of the block
                    GameObject block = GetRandomBlock();
                    // Set the new position
                    Vector3 pos = new Vector3(x * spacing.x,
                                              y * spacing.y);
                    block.transform.position = pos;
                    // Add spawned blocks to array
                    spawnedBlocks[x, y] = block;

                }
            }
        }

        void UpdateBlocks()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Update positions
                    GameObject currentBlock = spawnedBlocks[x, y];
                    // Create a new position
                    Vector2 pos = new Vector2(x * spacing.x,
                                              y * spacing.y);
                    // Add an offset to pos 
                    pos += offset;
                    // Set currentBlock's position to new pos
                    currentBlock.transform.position = pos;
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (isDebugging)
            {
                UpdateBlocks();
            }
        }
    }
}
