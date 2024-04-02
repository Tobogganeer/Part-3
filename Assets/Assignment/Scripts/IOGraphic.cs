using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOGraphic : MonoBehaviour
{
    static List<IOGraphic> all = new List<IOGraphic>();
    static bool visible = true;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = visible;
    }

    private void OnEnable()
    {
        all.Add(this);
    }

    private void OnDisable()
    {
        all.Remove(this);
    }

    /// <summary>
    /// Sets the global visibility of all IO graphics
    /// </summary>
    /// <param name="visible"></param>
    public static void SetVisibility(bool visible)
    {
        IOGraphic.visible = visible;

        foreach (IOGraphic graphic in all)
            graphic.spriteRenderer.enabled = visible;
    }
}
