using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public TMPro.TMP_Text selectedCharacterText;

    public static Villager SelectedVillager { get; private set; }
    public static void SetSelectedVillager(Villager villager)
    {
        if(SelectedVillager != null)
        {
            SelectedVillager.Selected(false);
        }
        SelectedVillager = villager;
        SelectedVillager.Selected(true);
    }

    // Might not be the most efficient to do it in update, but it's easy
    private void Update()
    {
        // Show the current character's type, if there is a current character
        selectedCharacterText.text = SelectedVillager ? SelectedVillager.GetChestType().ToString() : "No Character";
    }
}
