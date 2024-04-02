using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
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
