using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightControl_NoCoroutines : MonoBehaviour
{
    public GameObject missile;
    public float speed = 5;
    public float turningSpeedReduction = 0.75f;

    Action currentAction;
    Quaternion startHeading;
    Quaternion targetHeading;
    float interpolation;

    float legLength;
    float time;

    private void Update()
    {
        currentAction?.Invoke();
    }

    public void MakeTurn(float turn)
    {
        // Initialize the turn values
        interpolation = 0;
        startHeading = missile.transform.rotation;
        targetHeading = startHeading * Quaternion.Euler(0, 0, turn);

        // Make update tick our Turn function (could use booleans or enums or whatever but oh well)
        currentAction = Turn;
    }

    public void MoveForwards(float length)
    {
        // Init the movement
        legLength = length;
        time = 0;

        // Tick the movement update function
        currentAction = RunLeg;
    }

    void RunLeg()
    {
        if (time < legLength)
        {
            time += Time.deltaTime;
            missile.transform.Translate(transform.right * speed * Time.deltaTime);
        }
    }

    void Turn()
    {
        if (interpolation < 1)
        {
            interpolation += Time.deltaTime;
            missile.transform.rotation = Quaternion.Lerp(startHeading, targetHeading, interpolation);
            missile.transform.Translate(transform.right * (speed * turningSpeedReduction) * Time.deltaTime);
        }
    }
}
