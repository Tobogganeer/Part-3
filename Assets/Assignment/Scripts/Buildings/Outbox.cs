using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outbox : FactoryBuilding
{

}

/*

Outbox.cs
- 3x3 tiles
- Inputs on all sides
- Adds to the FactoryManager product counts
- Pseudocode:
  - override fn OnInput(product) => FactoryManager.OnProductOutboxed(product)

*/
