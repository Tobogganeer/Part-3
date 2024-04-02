using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Building Descriptor")]
public class BuildingDescriptor : ScriptableObject
{
    public Vector2Int size;
    public TileDescriptor[] tiles;
    public BuildingType type;
    public Sprite sprite;
}

/*

BuildingDescriptor.cs
- Stores the basic information of a building (size and tiles)
- ScriptableObject
- Pseudocode:
  - Variables: size, tiles (list of TileDescriptors), buildingType, sprite
  - No functions or anything else: basic data container

*/
