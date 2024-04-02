using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    // Allow these to be serialized in the inspector
    [field: SerializeField]
    public ProductID ID { get; private set; }
    [field: SerializeField]
    public int Amount { get; private set; }

    public Product(ProductID id, int amount)
    {
        ID = id;
        Amount = amount;
    }

    public Product(ProductID id) : this(id, 1) { }

    /// <summary>
    /// Spawns a GameObject to display this product.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public ProductObject SpawnObject(Vector3 position, float size = 0.9f) => ProductObject.Spawn(ID, position, size);
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
    /// <summary>
    /// Recipe: Tomato
    /// </summary>
    Sauce,
    /// <summary>
    /// Recipe: Can + Tomato
    /// </summary>
    CannedTomatoes,
    /// <summary>
    /// Recipe: Wheat
    /// </summary>
    Flour,

    // Secondary products
    /// <summary>
    /// Recipe: Flour + Water
    /// </summary>
    Dough,
    /// <summary>
    /// Recipe: Sauce + Water
    /// </summary>
    Soup,
    /// <summary>
    /// Recipe: CannedTomatoes + Water
    /// </summary>
    Juice,

    // Tertiary products
    /// <summary>
    /// Recipe: Dough + Sauce
    /// </summary>
    Pizza,
    /// <summary>
    /// Recipe: Dough + CannedTomatoes
    /// </summary>
    Pie,
    /// <summary>
    /// Recipe: Dough + Water
    /// </summary>
    Bread,

    // Legendary item
    /// <summary>
    /// Recipe: Bread + Can
    /// </summary>
    CannedBread,
}

/*

Product.cs
- Represents an item that is mined, produced, etc.
- Raw C# class, essentially just a ProductID and an amount
- Pseudocode:
  - Variables: id, amount 
  - Not much else for it to do...

*/
