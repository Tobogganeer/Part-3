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
    //List<Product> outputBuffer = new List<Product>();

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
        {
            if (!productSums.ContainsKey(p.ID))
                productSums.Add(p.ID, p.Amount);
            else
                productSums[p.ID] += p.Amount;
        }

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


    public override bool WillAccept(Product product, TileInput input)
    {
        // Don't take input if we have no recipe
        if (currentRecipe == null)
            return false;

        int required = GetIngredientCount(product.ID, currentRecipe);
        // Accept it if the recipe needs it and we don't have too much
        return required > 0 && CurrentIngredientCount(product.ID) < required * MaxIngredientMultiplier;
    }

    static int GetIngredientCount(ProductID ingredient, Recipe recipe)
    {
        Product correctInput = recipe.inputs.FirstOrDefault(input => input.ID == ingredient);
        return correctInput == null ? 0 : correctInput.Amount;
    }

    int CurrentIngredientCount(ProductID ingredient)
    {
        return Products.Where(p => p.ID == ingredient).Select(p => p.Amount).Sum();
    }

    protected override void Tick()
    {
        // If we have a recipe and have the necessary requirements
        if (currentRecipe != null && currentRecipe.inputs.All(input => CurrentIngredientCount(input.ID) >= input.Amount))
        {
            // Make sure we aren't already crafting
            if (craftCoroutine == null)
                craftCoroutine = StartCoroutine(Craft());
        }
    }

    public override void OnInput(Product product, TileInput input)
    {
        base.OnInput(product, input);
        ClampIngredients();
    }

    IEnumerator Craft()
    {
        Product outputProduct = new Product(currentRecipe.outputs[0].ID, currentRecipe.outputs[0].Amount);

        // Wait until there's an open spot to put our item
        while (!TryGetOpenOutput(outputProduct, out _))
            yield return null;

        // Consume the items
        foreach (Product product in Products)
            product.Amount -= GetIngredientCount(product.ID, currentRecipe);

        // Wait for the crafting time 
        yield return new WaitForSeconds(currentRecipe.craftingTime);

        // Wait for an open spot again (just in case)
        // TODO: Output buffer (store items instead of waiting)?
        TileOutput output;
        while (!TryGetOpenOutput(outputProduct, out output))
            yield return null;

        output.Output(outputProduct);
        craftCoroutine = null;
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
