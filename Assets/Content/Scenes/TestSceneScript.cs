using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TestSceneScript : MonoBehaviour
{
    PostProcessVolume volume;
    ChromaticAberration ca;

    void Start()
    {
        GameObject volumeObj = GameObject.Find("Postprocessing Volume");
        if (volumeObj != null)
        {
            volume = volumeObj.GetComponent<PostProcessVolume>();
            if (volume != null)
            {
                Debug.LogWarning("Found volume " + volume);
                /* ca = new ChromaticAberration();
                FloatParameter fp = new FloatParameter(); fp.value = 1.0f; // but why?
                ca.intensity = fp; */
            }
        }
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.C)) // C for chromatic aberration
        {
            volume.profile.AddSettings<ChromaticAberration>();

            FloatParameter fp = new FloatParameter(); fp.value = 1.0f;
            BoolParameter bp = new BoolParameter(); bp.value = true;
            volume.profile.GetSetting<ChromaticAberration>().enabled = bp;
            volume.profile.GetSetting<ChromaticAberration>().intensity = fp;

            
        }
    }
}
