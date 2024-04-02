using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public static FactoryManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public GameObject tileInputPrefab;
    public GameObject tileOutputPrefab;
    public GameObject productObjectPrefab;

    [Space]
    public SerializableDictionary<ProductID, Sprite> tileSprites;
    public SerializableDictionary<ProductID, Sprite> productSprites;
    public SerializableDictionary<BuildingType, BuildingDescriptor> buildings;
    public SerializableDictionary<BuildingType, GameObject> buildingPrefabs;
    public List<Goal> goals;

    [Space]
    public Toolbar toolbar;

    int currentGoal = 0;
    Dictionary<ProductID, int> currentOutboxes = new Dictionary<ProductID, int>();

    public static HashSet<BuildingType> GetCurrentlyUnlockedBuildings()
    {
        // Start with miner and outbox by default
        HashSet<BuildingType> unlocked = new HashSet<BuildingType>
        {
            BuildingType.Miner,
            BuildingType.Outbox
        };

        // Go up to our currently unlocked goal
        for (int i = 0; i < Mathf.Min(Instance.currentGoal, Instance.goals.Count); i++)
            unlocked.Add(Instance.goals[i].unlockedBuildingType);

        // Give both undergrounds at the same time
        if (unlocked.Contains(BuildingType.UndergroundInput))
            unlocked.Add(BuildingType.UndergroundOutput);

        return unlocked;
    }

    public static void OnProductOutboxed(Product product)
    {
        if (Instance.currentOutboxes.ContainsKey(product.ID))
            Instance.currentOutboxes[product.ID] += product.Amount;
        else
            Instance.currentOutboxes.Add(product.ID, product.Amount);

        Instance.CheckForGoalCompletion();
        Instance.UpdateGoalUI();
    }

    void CheckForGoalCompletion()
    {
        // We've reached the end
        if (currentGoal >= goals.Count)
            // TODO: Add a display on the UI/generate random goals?
            return;

        // Check if the goal is complete
        if (goals[currentGoal].products.All(goalProd => currentOutboxes[goalProd.ID] >= goalProd.Amount))
        {
            currentGoal++;
            toolbar.EnableCurrentlyUnlockedBuildings();
            currentOutboxes.Clear();
        }
    }

    void UpdateGoalUI()
    {
        // We've reached the end
        if (currentGoal >= goals.Count)
        {
            // Special stuff here
        }


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
