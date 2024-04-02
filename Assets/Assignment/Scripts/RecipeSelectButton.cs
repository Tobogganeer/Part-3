using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSelectButton : MonoBehaviour
{
    // Store a recipe and tell our recipe menu when we are pressed
    public Recipe recipe;
    public SpriteRenderer resultSprite;

    RecipeSelectMenu menu;

    private void Start()
    {
        menu = GetComponentInParent<RecipeSelectMenu>();
        resultSprite.sprite = recipe.outputs[0].Sprite;
    }

    private void OnMouseDown()
    {
        menu.RecipeChosen(recipe);
    }

}
