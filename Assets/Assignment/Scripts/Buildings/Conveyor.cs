using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Conveyor : FactoryBuilding
{
    public float transportDelay = 1f;

    //protected State state;

    public override bool WillAccept(Product product, TileInput input)
    {
        // Only receive a product if we have space
        return Products.Count == 0;
    }

    public override void OnInput(Product product, TileInput input)
    {
        base.OnInput(product, input);
    }

    protected virtual IEnumerator TransportProduct(Product product, TileInput input)
    {
        // sets itemSpriteRenderer using FactoryManager dict,
        // waits for GetTransportTime(),
        // waits for valid output,
        // outputs product,
        // remove product from products list,
        // sets itemSpriteRenderer to nothing
    }

    protected virtual IEnumerator MoveProductObject(ProductObject obj, Vector2 from, Vector2 to, float time)
    {
        float timer = 0f;
        while (timer < time)
        {
            // Just move it from point A to B
            timer += Time.deltaTime;
            obj.transform.position = Vector2.Lerp(from, to, timer / time);
            yield return null;
        }
    }

    protected virtual float GetTransportTime() => transportDelay;
    protected virtual bool HasValidOutput(Product product) => Outputs.Any(output => output.CanOutput(product));

    /*
    protected enum State
    {
        Empty,
        ReceivingItem,
        HoldingItem,
        SendingItem,
    }
    */
}

/*

Conveyor.cs
- 1 tile
- 1 output, 3 inputs
- Just transports an item forward
- References a SpriteRenderer to display the item
- Pseudocode:
  - Variables: transportDelay, itemSpriteRenderer
  - override fn WillAccept(product) => returns true if there are no current products
  - override fn OnInput(product) => base + starts TransportProduct coroutine
  - virtual coroutine TransportProduct(product) => sets itemSpriteRenderer using FactoryManager dict, waits for GetTransportTime(), waits for valid output, outputs product, remove product from products list, sets itemSpriteRenderer to nothing
  - virtual fn GetTransportTime() => returns transportDelay by default
  - virtual fn HasValidOutput() => returns our output's CanOutput() by default

*/
