﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        // Store x and y coordinate in array for later use
        public int x = 0;
        public int y = 0;
        public bool isMine = false; // Is the current tile of a mine?
        public bool isRevealed = false; // Has the tile already been revealed?
        [Header("References")]
        public Sprite[] emptySprites; // List of empty sprites i.e, empty, 1, 2, 3, 4, etc...
        public Sprite[] mineSprites; // The mine sprites

        private SpriteRenderer rend; // Reference to sprite renderer

        void Awake()
        {
            // Grabbing the sprite renderer reference
            rend = GetComponent<SpriteRenderer>();
        }
        void Start()
        {
            // Randomly decide if it's a mine or not
            isMine = Random.value < 0.5f;
        }

        // Destroys tile when LMB is pressed on it
        void OnMouseDown()
        {
            Destroy(gameObject);
        }

        public void Reveal(int adjacentMines, int mineState = 0)
        {
            // Flags the tile as being revealed
            isRevealed = true;
            // Check if tile is a mine
            if (isMine)
            {
                // Sets sprite to mine sprite
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                // Sets sprite to appropriate texture based on adjacent mines
                rend.sprite = emptySprites[adjacentMines];
            }
        }
    }
}