using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : FactoryBuilding
{
    
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
