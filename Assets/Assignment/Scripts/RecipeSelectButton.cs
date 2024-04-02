using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RecipeSelectButton : MonoBehaviour
{
    // Store a recipe and tell our recipe menu when we are pressed
    public Recipe recipe;

    SpriteRenderer spriteRenderer;
    RecipeSelectMenu menu;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        menu = GetComponentInParent<RecipeSelectMenu>();

        spriteRenderer.sprite = recipe.outputs[0].Sprite;
    }

    private void OnMouseDown()
    {
        menu.RecipeChosen(recipe);
    }

}
