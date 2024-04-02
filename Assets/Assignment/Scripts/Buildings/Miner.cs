using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Miner : FactoryBuilding
{
    public float mineDelay = 1f;
    public SpriteRenderer outputSpriteRenderer;

    List<WorldTile> resourceTiles;

    protected override void Start()
    {
        base.Start();
        outputSpriteRenderer.sprite = null; // Hide the placeholder circle
    }

    public override void Place()
    {
        base.Place();
        resourceTiles = World.GetWorldTiles(this).Where((tile) => tile.Product != ProductID.None).ToList();
        StartCoroutine(Mine());
    }

    IEnumerator Mine()
    {
        while (true)
        {
            // Loop through all resources (for if we are sitting on multiple different ones)
            for (int i = 0; i < resourceTiles.Count; i++)
            {
                // Set the sprite to the current product
                ProductID prod = resourceTiles[i].Product;
                outputSpriteRenderer.sprite = FactoryManager.Instance.productSprites[prod];

                // Create a new WaitForSeconds each time in case the delay changes
                yield return new WaitForSeconds(mineDelay);

                Product product = new Product(prod);

                // Wait until an output opens up and send it out
                yield return new WaitUntil(() => Outputs.Any(output => output.Output(product)));
            }
        }
    }

    public override bool CanBePlacedOn(List<WorldTile> worldTiles)
    {
        // Make sure at least one tile has a resource we can mine
        return worldTiles.Any((tile) => tile.Product != ProductID.None);
    }
}

/*

Miner.cs
- 2x2 tiles
- 1 output
- Outputs the resource it is placed on
- Has a SpriteRenderer for showing the output
- Pseudocode:
  - Variables: mineDelay, outputSpriteRenderer
  - override fn Place(position) => base + start Mine coroutine, set outputSpriteRenderer to the resource we are on
  - coroutine Mine() => infinite loop that outputs a resource and waits for mineDelay
  - override fn CanBePlacedOn(worldTiles) => return base fn + check if at least one tile has a resource

*/
