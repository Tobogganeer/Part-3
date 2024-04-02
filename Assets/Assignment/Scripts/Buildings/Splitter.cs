using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : FactoryBuilding
{
    int currentOutput;

    public override bool WillAccept(Product product, TileInput input)
    {
        // Only receive a product if we have space
        return Products.Count == 0;
    }

    protected override void Tick()
    {
        // Try to output any products we have stored
        if (Products.Count > 0 && Output(Products[0]))
            Products.RemoveAt(0);
    }

    public override void OnInput(Product product, TileInput input)
    {
        // Only store the product if we can't output it immediately
        if (!Output(product))
            base.OnInput(product, input);
    }

    bool Output(Product product)
    {
        // If the first output fails
        if (!TryCurrentOutput(product))
        {
            // Swap and try again
            SwapOutputs();
            return TryCurrentOutput(product);
        }

        return true;
    }

    bool TryCurrentOutput(Product product)
    {
        // Check if we can output the product
        if (Outputs[currentOutput].Output(product))
        {
            // Switch to the next output for next time
            SwapOutputs();
            return true;
        }

        return false;
    }

    void SwapOutputs()
    {
        currentOutput = (++currentOutput % Outputs.Length);
    }
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
