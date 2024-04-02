using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
}

/*

HUD.cs
- Shows the current production goals and what building it will unlock
- Has various UI components (text elements for production stats, icons for building sprites, etc)
- Pseudocode:
  - Variables: nextUnlockImage, currentGoalText[], nextUnlockText, currentGoal
  - static fn SetCurrentGoal(goal) => update currentGoal, nextUnlockText, nextUnlockImage
  - static fn SetProduct(product) => update the corresponding currentGoalText (check the currentGoal to get the index)

*/
