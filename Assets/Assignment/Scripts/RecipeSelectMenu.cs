using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSelectMenu : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
}

/*

RecipeSelectMenu.cs
- Shows available recipes for the assembler
- Pops up when the assembler is clicked
- Unique instance of menu per assembler (part of prefab, makes it a bit easier)
- Will have another small script that handles clicks and sends that info to this script
- Pseudocode
  - Variables: target, menu
  - Start => get reference to target (Assembler script on same object)
  - fn Open() => turn the menu GameObject on
  - fn RecipeChosen(recipe) => set target's current recipe to the clicked recipe and close the window (call Close())
  - fn Close() => turn the menu off

*/
