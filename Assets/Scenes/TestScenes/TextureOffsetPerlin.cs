using UnityEngine;

public class TextureOffsetPerlin : MonoBehaviour
{
    float xoff1 = 0.0f;
    float xoff2 = 10000.0f;
    Vector2 scale;
    MeshRenderer mr;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        if (mr != null && mr.material != null)
        {
            Vector4 v = this.GetComponent<MeshRenderer>().material.GetVector("_MainTex_ST");
            scale.x = v.x;
            scale.y = v.y;
        }
        
        
    }

    void Update()
    {
        if (mr != null)
        {
            xoff1 += 0.2f * Time.deltaTime;
            xoff2 += 0.2f * Time.deltaTime;
            float x = Mathf.PerlinNoise1D(xoff1);
            float y = Mathf.PerlinNoise1D(xoff2);
            mr.material.SetVector("_MainTex_ST", new Vector4(scale.x, scale.y, x, y));
        }
    }
}
