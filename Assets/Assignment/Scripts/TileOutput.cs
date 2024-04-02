using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOutput : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
}

/*

TileOutput
- Represents a place where an item can leave a building
- No components - raw class
- Pseudocode:
  - Variables: direction, tile, building
  - fn Init(tile) => set reference to tile and building (direction set in inspector)
  - fn CanOutput(product) => returns true if there is a place to output the item (checks buildings from World for inputs)
  - fn Output(product) => puts the product into the attached input

*/
