using UnityEngine;

public class TestSceneScript : MonoBehaviour
{
    GameObject waterPlane;
    float xoff1 = 0.0f;
    float xoff2 = 10000.0f;
    Vector2 scale;

    void Start()
    {
        waterPlane = GameObject.FindGameObjectWithTag("WaterLayer");
        if (waterPlane != null)
        {
            Vector4 v = waterPlane.GetComponent<MeshRenderer>().material.GetVector("_MainTex_ST");
            scale.x = v.x;
            scale.y = v.y;
            Debug.LogWarning("Found water layer");
        }
    }

    void Update() {
        if (waterPlane != null)
        {
            xoff1 += 0.2f * Time.deltaTime;
            xoff2 += 0.2f * Time.deltaTime;
            float x = Mathf.PerlinNoise1D(xoff1);
            float y = Mathf.PerlinNoise1D(xoff2);
            waterPlane.GetComponent<MeshRenderer>().material.SetVector("_MainTex_ST", new Vector4(scale.x, scale.y, x, y));

        }

    }
}
