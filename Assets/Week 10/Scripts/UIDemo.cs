#define USE_UI_IMAGE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if USE_UI_IMAGE
using VisualElement = UnityEngine.UI.Image;
#else
using VisualElement = SpriteRenderer;
#endif

[RequireComponent(typeof(VisualElement))]

public class UIDemo : MonoBehaviour
{
    public Color startColour;
    public Color endColour;

    private VisualElement sr;

    private void Start()
    {
        sr = GetComponent<VisualElement>();
    }

    public void SetColour(float value0to60)
    {
        float t = value0to60 / 60f;
        sr.color = Color.Lerp(startColour, endColour, t);
    }
}
