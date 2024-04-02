using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ProductObject : MonoBehaviour
{
    public ProductID id;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = FactoryManager.Instance.productSprites.dict[id];
    }
}

/*

ProductObject.cs
- Used to display a Product in the world
- Has a SpriteRenderer
- Pseudocode:
  - Variables: productID
  - Start => get SpriteRenderer, set its sprite using the list in FactoryManager

*/
