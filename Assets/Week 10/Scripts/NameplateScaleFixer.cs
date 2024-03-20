using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When the character flips their X scale we don't want the text to flip as well
public class NameplateScaleFixer : MonoBehaviour
{
    Transform parent;

    private void Start()
    {
        parent = transform.parent;
    }

    private void Update()
    {
        // Match our X flip to our parent's X flip (will cancel each other out)
        transform.localScale = new Vector3(Mathf.Sign(parent.localScale.x), 1f, 1f);
    }
}
