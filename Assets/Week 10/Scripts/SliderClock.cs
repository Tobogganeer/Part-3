using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderClock : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        // Just tick the slider each second (wrap around when it maxes out)
        slider.value = (slider.value + Time.deltaTime) % slider.maxValue;
    }
}
