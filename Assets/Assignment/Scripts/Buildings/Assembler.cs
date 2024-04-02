using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Assembler : FactoryBuilding
{
    public SpriteRenderer[] inputSprites;
    public SpriteRenderer outputSprite;

    Recipe currentRecipe;
    Coroutine craftCoroutine;
    List<Product> outputBuffer = new List<Product>();

    // Used to limit how many ingredients we can store
    const int MaxIngredientMultiplier = 3;

    protected override void Start()
    {
        base.Start();
        // Remove placeholder sprites
        ClearSprites();
    }

    public void UpdateRecipe(Recipe newRecipe)
    {
        // Do nothing if it's the same recipe
        if (newRecipe == currentRecipe)
            return;

        currentRecipe = newRecipe;
        // Stop any current crafting
        if (craftCoroutine != null)
            StopCoroutine(craftCoroutine);

        ClampIngredients();
        UpdateSprites();
    }

    void ClearSprites()
    {
        outputSprite.sprite = null;
        foreach (SpriteRenderer inputSprite in inputSprites)
            inputSprite.sprite = null;
    }

    void UpdateSprites()
    {
        ClearSprites();

        if (currentRecipe == null)
            return;

        outputSprite.sprite = currentRecipe.outputs[0].Sprite;
        for (int i = 0; i < currentRecipe.inputs.Count; i++)
            inputSprites[i].sprite = currentRecipe.inputs[i].Sprite;
    }

    void ClampIngredients()
    {
        // Don't check or remove anything if we have no recipe
        if (currentRecipe == null)
            return;

        // Count how much of each product we have
        Dictionary<ProductID, int> productSums = new Dictionary<ProductID, int>();
        foreach (Product p in Products)
            productSums[p.ID] += p.Amount;

        // Wipe them all out
        Products.Clear();

        // Loop through each possible ingredient
        for (int i = 0; i < currentRecipe.inputs.Count; i++)
        {
            Product input = currentRecipe.inputs[i];
            // Check how much we have of that ingredient, if any
            if (productSums.TryGetValue(input.ID, out int currentStoredAmount))
            {
                // Store our product back, making sure we don't have too much
                Products.Add(new Product(input.ID, Mathf.Min(currentStoredAmount, input.Amount * MaxIngredientMultiplier)));
            }
        }
    }

}

/*

Assembler.cs
- 2x2 tiles
- 1 output, 4 inputs
- Recipe can be set with a GUI
- Takes in items and creates a new item according to the recipe
- Has SpriteRenderers for the current recipe's inputs and output
- Has an attached RecipeSelectMenu
- Pseudocode:
  - Variables: currentRecipe, inputSprites, outputSprite, const_maxIngredientMultiplier, craftCoroutine
  - fn UpdateRecipe(newRecipe) => set currentRecipe to newRecipe, update recipe sprites (input and output), destroy current products that dont match the new recipe (wrong type or too many), stop the craftCoroutine if it is running
  - override fn WillAccept(product) => returns true if the currentRecipe uses that product and there isnt too much of that product (multiply how much the current recipe uses by const_maxIngredientMultiplier)
  - override fn OnInput(product) => base + check if we have enough products to start crafting - if so start the coroutine
  - coroutine Craft() => waits for the currentRecipe's craft time, waits for valid output, outputs product, removes used products from products list

*/
