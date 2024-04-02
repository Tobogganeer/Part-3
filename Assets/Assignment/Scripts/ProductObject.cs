using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ProductObject : MonoBehaviour
{
    public ProductID id;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = FactoryManager.Instance.productSprites[id];
    }

    /// <summary>
    /// Spawns a GameObject to display this the <paramref name="product"/>.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="position"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static ProductObject Spawn(ProductID product, Vector3 position, float size = 0.9f)
    {
        GameObject prefab = FactoryManager.Instance.productObjectPrefab;
        ProductObject productObj = Instantiate(prefab, position, Quaternion.identity).GetComponent<ProductObject>();
        productObj.transform.localScale = Vector3.one * size;
        productObj.id = product;
        return productObj;

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
