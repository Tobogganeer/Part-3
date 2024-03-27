using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Growing : MonoBehaviour
{
    public GameObject square;
    public GameObject triangle;
    public GameObject circle;
    public TextMeshProUGUI squareTMP;
    public TextMeshProUGUI triangleTMP;
    public TextMeshProUGUI circleTMP;
    public TextMeshProUGUI crTMP;
    public int running;
    Coroutine coroutine;

    void Start()
    {
        StartCoroutine(GrowShapes());
    }

    IEnumerator GrowShapes()
    {
        running++;

        yield return StartCoroutine(Square());
        // Wait for a second to start the triangle
        yield return new WaitForSeconds(1f);
        StartCoroutine(Triangle());
        StartCoroutine(Circle());

        running--;
    }

    void Update()
    {
        crTMP.text = "Coroutines: " + running;
    }

    IEnumerator Square()
    {
        running++;

        float size = 0;
        while (size < 5)
        {
            size += Time.deltaTime;
            Vector3 scale = new Vector3(size, size, size);
            square.transform.localScale = scale;
            squareTMP.text = "Square: " + scale;
            yield return null;
        }

        running--;
    }
    IEnumerator Triangle()
    {
        running++;

        float size = 0;
        while (size < 5)
        {
            size += Time.deltaTime;
            Vector3 scale = new Vector3(size, size, size);
            triangle.transform.localScale = scale;
            triangleTMP.text = "Triangle: " + scale;
            yield return null;
        }

        running--;
    }
    IEnumerator Circle()
    {
        running++;

        float size = 0;
        // Loop forever (I know it said to use a while loop but I <3 goto)
        LoopStart:

        while (size < 5)
        {
            size += Time.deltaTime;
            Vector3 scale = new Vector3(size, size, size);
            circle.transform.localScale = scale;
            circleTMP.text = "Cirlce: " + scale;
            yield return null;
        }
        while (size > 0)
        {
            size -= Time.deltaTime;
            Vector3 scale = new Vector3(size, size, size);
            circle.transform.localScale = scale;
            circleTMP.text = "Cirlce: " + scale;
            yield return null;
        }

        goto LoopStart;

        //running--;
    }
}
