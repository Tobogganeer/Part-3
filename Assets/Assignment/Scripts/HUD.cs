using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    private static HUD instance;
    private void Awake()
    {
        instance = this;
    }

    public Image nextUnlockImage;
    public TMP_Text nextUnlockText;
    public GoalUI[] goalUIs;

    [Space]
    public TMP_Text currentBuildingText;

    public static void SetCurrentGoal(Goal goal)
    {
        if (goal == null)
        {
            AllGoalsCompleted();
            return;
        }

        // Turn on the appropriate number of goal UIs
        for (int i = 0; i < instance.goalUIs.Length; i++)
            instance.goalUIs[i].gameObject.SetActive(i < goal.products.Length);

        Dictionary<ProductID, int> currentOutboxes = FactoryManager.GetCurrentOutboxes();

        for (int i = 0; i < goal.products.Length; i++)
        {
            int currentAmount;
            // If we have outboxed any of this product, set it
            if (!currentOutboxes.TryGetValue(goal.products[i].ID, out currentAmount))
                currentAmount = 0;
            instance.goalUIs[i].Set(goal.products[i], currentAmount);
        }

        BuildingDescriptor nextBuilding = FactoryManager.Instance.buildings[goal.unlockedBuildingType];
        instance.nextUnlockImage.sprite = nextBuilding.sprite;
        instance.nextUnlockText.text = FormatBuildingType(nextBuilding.type);
    }

    static void AllGoalsCompleted()
    {
        // Turn off all UIs
        for (int i = 0; i < instance.goalUIs.Length; i++)
            instance.goalUIs[i].gameObject.SetActive(false);

        instance.nextUnlockText.text = "Personal Satisfaction";
        instance.nextUnlockImage.sprite = FactoryManager.Instance.productSprites[ProductID.Tomato]; // idk tomato sure
    }

    static string FormatBuildingType(BuildingType type) => type switch
    {
        // When unlocking underground conveyors, let the player know it's not just the inputs
        BuildingType.UndergroundInput => "Underground Conveyors",
        _ => type.ToString()
    };

    public static void SetCurrentBuildingText(BuildingType type)
    {
        instance.currentBuildingText.text = type.ToString();
    }
}

/*

HUD.cs
- Shows the current production goals and what building it will unlock
- Has various UI components (text elements for production stats, icons for building sprites, etc)
- Pseudocode:
  - Variables: nextUnlockImage, currentGoalText[], nextUnlockText, currentGoal
  - static fn SetCurrentGoal(goal) => update currentGoal, nextUnlockText, nextUnlockImage
  - static fn SetProduct(product) => update the corresponding currentGoalText (check the currentGoal to get the index)

*/
