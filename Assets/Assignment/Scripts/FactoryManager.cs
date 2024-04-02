using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public static FactoryManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public SerializableDictionary<ProductID, Sprite> tileSprites;
}

/*

FactoryManager.cs
- Manages lists and data for other scripts (sprites, ScriptableObjects, goals, etc)
- Pseudocode:
  - Variables: goals, tileSprites, buildings, productSprites, recipes, instance, [product counts]
  - Awake => set singleton instance
  - static fn OnProductOutboxed(product) => increment appropriate counts
  - Needed functions tbd as development progresses

*/
