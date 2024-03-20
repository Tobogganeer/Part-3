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
    public Vector2 daggerDelays = new Vector2(0.1f, 0.2f);

    float baseSpeed;
    Coroutine currentDash;

    protected override void Start()
    {
        // Grab our references
        base.Start();
        // Track our base speed
        baseSpeed = speed;
    }

    protected override void Attack()
    {
        // Stop the current dash (if applicable)
        if (currentDash != null)
            StopCoroutine(currentDash);

        // Start our dash
        currentDash = StartCoroutine(Dash());   
    }

    IEnumerator Dash()
    {
        // Set destination and wait
        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        speed = baseSpeed * dashSpeedMultiplier;

        // Store our move direction to spawn daggers going that way
        Vector3 direction = destination - (Vector2)spawnPoint.position;

        yield return new WaitForSeconds(dashTime);

        // Reset speed
        speed = baseSpeed;
        // Play attack animation
        base.Attack();

        // Spawn in the daggers
        Vector3 daggerSpawnPos = spawnPoint.position;
        Vector3 daggerTarget = spawnPoint.position + direction;

        yield return new WaitForSeconds(daggerDelays.x);
        SpawnDagger(daggerSpawnPos, daggerTarget);

        yield return new WaitForSeconds(daggerDelays.y);
        SpawnDagger(daggerSpawnPos, daggerTarget);
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

    public override string ToString()
    {
        return "Sneaky Thief";
    }
}
