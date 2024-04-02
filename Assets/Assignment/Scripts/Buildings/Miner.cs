using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : FactoryBuilding
{

}

/*

Miner.cs
- 2x2 tiles
- 1 output
- Outputs the resource it is placed on
- Has a SpriteRenderer for showing the output
- Pseudocode:
  - Variables: mineDelay, outputSpriteRenderer
  - override fn Place(position) => base + start Mine coroutine, set outputSpriteRenderer to the resource we are on
  - coroutine Mine() => infinite loop that outputs a resource and waits for mineDelay
  - override fn CanBePlacedOn(worldTiles) => return base fn + check if at least one tile has a resource

*/
