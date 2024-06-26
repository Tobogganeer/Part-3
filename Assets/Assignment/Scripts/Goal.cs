using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Goal")]
public class Goal : ScriptableObject
{
    public Product[] products;
    public BuildingType unlockedBuildingType;
}

/*

Goal.cs
- Represents a production goal needed to unlock the next building
- ScriptableObject
- Pseudocode:
  - Variables: products, unlockedBuildingType
  - Nothing else really

*/
