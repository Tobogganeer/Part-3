using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler : FactoryBuilding
{
    
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
