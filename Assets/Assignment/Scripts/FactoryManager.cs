using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public static FactoryManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        Init();
    }

    public GameObject tileInputPrefab;
    public GameObject tileOutputPrefab;
    public GameObject productObjectPrefab;

    [Space]
    public SerializableDictionary<ProductID, Sprite> tileSprites;
    public SerializableDictionary<ProductID, Sprite> productSprites;
    public SerializableDictionary<BuildingType, BuildingDescriptor> buildings;
    public SerializableDictionary<BuildingType, GameObject> buildingPrefabs;
    [SerializeField] private List<Recipe> _recipes;

    public Dictionary<ProductID, Recipe> recipes;

    void Init()
    {
        recipes = new Dictionary<ProductID, Recipe>();
        foreach (Recipe recipe in _recipes)
        {
            // Each recipe only has one output item type
            recipes.Add(recipe.outputs[0].ID, recipe);
        }
    }

    public static HashSet<BuildingType> GetCurrentlyUnlockedBuildings()
    {
        // TODO: Actually implement

        // For testing purposes just have all buildings unlocked
        HashSet<BuildingType> unlocked =
            new HashSet<BuildingType>((BuildingType[])System.Enum.GetValues(typeof(BuildingType)));

        return unlocked;
    }
}

/*

FactoryManager.cs
- Manages lists and data for other scripts (sprites, ScriptableObjects, goals, etc)
- Pseudocode:
  - Variables: goals, tileSprites, buildings, productSprites, recipes, instance, [product counts]
  - Awake => set singleton instance
  - static fn OnProductOutboxed(product) => increment appropriate counts
  - Needed functions tbd as development progresses

*/
