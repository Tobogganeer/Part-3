using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    public ProductID Product { get; set; }
    public Vector2Int GridPosition { get; set; }

    // Called by the World when it spawns this tile
    public void Init(ProductID product, Vector2Int gridPosition)
    {
        Product = product;
        GridPosition = gridPosition;
    }

    private void Start()
    {
        // Set our sprite
        GetComponent<SpriteRenderer>().sprite = FactoryManager.Instance.tileSprites.dict[Product];
    }
}

/*

WorldTile.cs
- Displays the state of one tile in the world (really just if any resources are there)
- Updates its graphics to match the state when it is created
- Has a SpriteRenderer attached
- Pseudocode:
  - Variables: spriteRenderer, tileType
  - Start => set sprite to match the tileType (get the sprites from FactoryManager)

*/
