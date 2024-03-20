using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private static CharacterControl instance;
    private void Awake()
    {
        instance = this;
    }


    public TMPro.TMP_Text selectedCharacterText;
    public UnityEngine.UI.Slider scaleSlider;
    public Villager[] villagers;

    public static Villager SelectedVillager { get; private set; }
    public static void SetSelectedVillager(Villager villager)
    {
        if (SelectedVillager != null)
            SelectedVillager.Selected(false);

        SelectedVillager = villager;
        if (SelectedVillager != null)
        {
            SelectedVillager.Selected(true);
            // Set the slider to match our current villager
            instance.scaleSlider.value = SelectedVillager.scale;
        }

        // Show the current character's type (if we have one selected)
        instance.selectedCharacterText.text = SelectedVillager == null ? "No Character" : SelectedVillager.ToString();
    }

    private void Start()
    {
        // Initial text
        selectedCharacterText.text = "No Character";
    }


    public void SelectFromDropdown(int index)
    {
        // Check if the index is valid
        if (index < 0 || index >= villagers.Length)
            SetSelectedVillager(null);
        else
            SetSelectedVillager(villagers[index]);
    }

    public void SetCurrentVillagerScale(float scale)
    {
        // Should be clamped from the slider but we'll make sure it is
        scale = Mathf.Clamp(scale, 0.5f, 2f);
        if (SelectedVillager != null)
            SelectedVillager.scale = scale;
    }
}
