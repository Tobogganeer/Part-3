using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Villager
{
    public float dashTime = 0.3f;
    public float dashSpeedMultiplier = 3f;

    [Space]
    public GameObject daggerPrefab;
    public Transform spawnPoint;
    public float spawnAngleRandomness = 10f;
    public int numDaggers = 2;

    float baseSpeed;

    protected override void Start()
    {
        // Grab our references
        base.Start();
        // Track our base speed
        baseSpeed = speed;
    }

    protected override void Attack()
    {
        StartCoroutine(Dash());

        // Spawn in the daggers
        //for (int i = 0; i < numDaggers; i++)
        //    SpawnDagger(spawnPoint.position, destination);
    }

    IEnumerator Dash()
    {
        // Set destination and wait
        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        speed = baseSpeed * dashSpeedMultiplier;

        yield return new WaitForSeconds(dashTime);

        // Reset speed
        speed = baseSpeed;
        // Play attack animation
        base.Attack();
    }

    void SpawnDagger(Vector3 position, Vector3 target)
    {
        Vector3 direction = (target - position).normalized;
        // Make a rotation towards the target (up is the 2d forward)
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        // Add in some randomness (I love quaternion math)
        rotation *= Quaternion.Euler(0, 0, Random.Range(-spawnAngleRandomness, spawnAngleRandomness));
        Instantiate(daggerPrefab, position, rotation);
    }

    public override ChestType GetChestType() => ChestType.Thief;
}
