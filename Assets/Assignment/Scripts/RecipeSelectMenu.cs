using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Assembler))]
public class RecipeSelectMenu : MonoBehaviour
{
    public GameObject menu;

    Assembler target;
    bool justOpened;

    private void Start()
    {
        target = GetComponent<Assembler>();
    }

    public void Open()
    {
        justOpened = true;
        menu.SetActive(true);
    }

    public void RecipeChosen(Recipe recipe)
    {
        target.UpdateRecipe(recipe);
        Close();
    }

    public void Close()
    {
        menu.SetActive(false);
    }

    private void OnMouseDown()
    {
        Open();
    }

    private void Update()
    {
        // If the user pressed the mouse and we've been open for 1 frame
        if (!justOpened && Input.GetKeyDown(KeyCode.Mouse0))
            Close();

        justOpened = false;
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
