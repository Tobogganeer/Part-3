using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Villager
{
    public GameObject arrowPrefab;
    public Transform spawnPoint;

    protected override void Attack()
    {
        base.Attack();
        destination = transform.position; // Stop moving
        Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation); // Spawn arrow
    }

    public override ChestType GetChestType() => ChestType.Archer;
}
