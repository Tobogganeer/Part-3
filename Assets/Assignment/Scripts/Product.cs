using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product
{
    
}

public enum ProductID
{
    None,
    // Base resources
    Water,
    Wheat,
    Tomato,
    Can,

    // Primary products (all amounts are tbd)
    CannedTomatoes, // Can + Tomato
    Sauce, // Tomato
    Flour, // Wheat

    // Secondary products
    Dough, // Flour + Water
    Soup, // Sauce + Water
    Juice, // CannedTomatoes + Water

    // Tertiary products
    Pizza, // Dough + Sauce
    Pie, // Dough + CannedTomatoes
    Bread, // Dough + Water
    CannedJuice, // Juice + Can

    // Legendary item
    CannedBread, // Bread + Can
}

/*

Product.cs
- Represents an item that is mined, produced, etc.
- Raw C# class, essentially just a ProductID and an amount
- Pseudocode:
  - Variables: id, amount 
  - Not much else for it to do...

*/
