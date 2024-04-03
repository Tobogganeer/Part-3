using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outbox : FactoryBuilding
{
    public override void OnInput(Product product, TileInput input)
    {
        base.OnInput(product, input);
        FactoryManager.OnProductOutboxed(product);
    }
}

/*

Outbox.cs
- 3x3 tiles
- Inputs on all sides
- Adds to the FactoryManager product counts
- Pseudocode:
  - override fn OnInput(product) => FactoryManager.OnProductOutboxed(product)

*/
