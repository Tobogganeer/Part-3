using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : FactoryBuilding
{

}

/*

Splitter.cs
- 2x1 tiles
- 2 inputs at the back, 2 outputs at the front
- Swaps which output it uses each time
- Pseudocode:
  - Variables: currentOutput
  - override fn WillAccept(product) => returns true if there are no current products
  - override fn Tick() => if we currently have a product stored, call Output(), removing the product from the products list if successfully output
  - override fn OnInput(product) => calls Output(), calling the base function to stores the product if it returns false
  - bool Output() => calls TryOutput(), and if output fails it SwapOutputs() and tries again. Returns whether or not the item was output successfully
  - bool TryCurrentOutput(product) => checks if the current output can output, outputting the product, swapping the output and returning true, if output is possible. Returns false otherwise
  - fn SwapOutputs() => changes the currentOutputIndex from 0 to 1 and vice versa

*/
