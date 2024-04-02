using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
}

/*

Recipe.cs
- Stores what inputs will produce what outputs in an assembler
- ScriptableObject
- Pseudocode:
  - Variables: inputs, outputs, craftingTime
  - Not much else, maybe one util function
  - fn CanBeCraftedBy(assembler) => returns true if the assembler's inventory has all required components
  - ^^^ but that function would likely be in the assembler itself, single responsibility and all

*/
