using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Stage[] stages;

    IEnumerator Start()
    {
        foreach (Stage stage in stages)
        {
            // Start all objects at 0 scale
            stage.SetScale(0f);
        }

        for (int i = 0; i < stages.Length; i++)
        {
            float timer = 0;
            // Wait for each stage to build
            while (timer < stages[i].timeToBuild)
            {
                // Increment our timer
                timer += Time.deltaTime;

                // Set the scale from 0 to 1
                float fac = timer / stages[i].timeToBuild;
                stages[i].SetScale(fac);

                // Wait a frame
                yield return null;
            }
        }
    }

    [System.Serializable]
    public class Stage
    {
        public float timeToBuild = 0.5f;
        public GameObject[] gameObjects;

        public void SetScale(float scale)
        {
            foreach (GameObject go in gameObjects)
            {
                go.transform.localScale = Vector3.one * scale;
            }
        }
    }
}
