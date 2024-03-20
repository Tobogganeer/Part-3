using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIDemo : MonoBehaviour
{
    public Color startColour;
    public Color endColour;

    private Image sr;

    private void Start()
    {
        sr = GetComponent<Image>();
    }

    public void SetColour(float value0to60)
    {
        float t = value0to60 / 60f;
        sr.color = Color.Lerp(startColour, endColour, t);
    }
}
