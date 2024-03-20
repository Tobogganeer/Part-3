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

    public static Villager SelectedVillager { get; private set; }
    public static void SetSelectedVillager(Villager villager)
    {
        if (SelectedVillager != null)
        {
            SelectedVillager.Selected(false);
        }
        SelectedVillager = villager;
        SelectedVillager.Selected(true);

        // Show the current character's type (seems we assume it will never be null)
        instance.selectedCharacterText.text = SelectedVillager.ToString();
    }

    private void Start()
    {
        // Initial text
        selectedCharacterText.text = "No Character";
    }
}
