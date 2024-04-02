using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToolbarButton : MonoBehaviour
{
    // Basically store a ref to the toolbar, store a building type, tell it when we are pressed
    // Simple life...

    public Toolbar toolbar;
    public Image buildingIcon;
    public BuildingType buildingType;

    bool unlocked;

    private void Start()
    {
        // Register the click in code to save a few clicks in editor
        GetComponent<Button>().onClick.AddListener(OnClicked);
        // Set the button's icon
        buildingIcon.sprite = FactoryManager.Instance.buildings.dict[buildingType].sprite;
    }

    void OnClicked()
    {
        if (unlocked)
            toolbar.BuildingButtonPressed(buildingType);
    }

    public void SetUnlockState(bool unlocked)
    {
        this.unlocked = unlocked;
        // Set icon to be black if we are locked
        buildingIcon.color = unlocked ? Color.white : Color.black;
    }
}
