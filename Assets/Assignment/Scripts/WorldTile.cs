using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [HideInInspector] public 
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
