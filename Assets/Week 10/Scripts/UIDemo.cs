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

    [Space]
    public TMPro.TMP_Dropdown selectionDropdown;

    private VisualElement visuals;

    private void Start()
    {
        visuals = GetComponent<VisualElement>();
    }

    public void SetColour(float value0to60)
    {
        float t = value0to60 / 60f;
        visuals.color = Color.Lerp(startColour, endColour, t);
    }

    public void SetImageFromDropdown(int index)
    {
        visuals.sprite = selectionDropdown.options[index].image;
    }
}
