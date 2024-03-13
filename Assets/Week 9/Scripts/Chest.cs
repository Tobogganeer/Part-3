using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator animator;
    public ChestType type;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only open/close if they are the right type
        if (MatchesChestType(collision))
            animator.SetBool("IsOpened", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (MatchesChestType(collision))
            animator.SetBool("IsOpened", false);
    }

    bool MatchesChestType(Component comp)
    {
        // Make sure the object is a villager and they are the correct type or we are open to everyone
        return comp.TryGetComponent(out Villager villager) && (type == ChestType.Villager || villager.GetChestType() == type);
    }
}

public enum ChestType
{
    Villager,
    Merchant,
    Archer,
    Thief,
}
