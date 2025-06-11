using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TestSceneScript : MonoBehaviour
{
    PostProcessVolume volume;
    
    void Start()
    {
        GameObject volumeObj = GameObject.Find("Postprocessing Volume");
        if (volumeObj != null) {
            volume = volumeObj.GetComponent<PostProcessVolume>();
            if (volume != null) {
                Debug.LogWarning("Found volume " + volume);
            }
        }
        ca_enabled = false;
    }

    bool ca_enabled;
    void Update() {
        if (Input.GetKeyDown(KeyCode.C)) { // C for chromatic aberration
            ChromaticAberration ca;
            volume.profile.TryGetSettings(out ca);
            ca.intensity.value = ca_enabled ? 1.0f : 0.0f;
            ca_enabled = !ca_enabled;
        }

        /* Later, try to figure how to do to this
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C)) {
            ChromaticAberration ca;
            volume.profile.TryGetSettings(out ca);
            ca.intensity.value = 0.0f;
        } else if (Input.GetKeyDown(KeyCode.C)) {
            ChromaticAberration ca;
            volume.profile.TryGetSettings(out ca);
            ca.intensity.value = 1.0f;
        }
        */
    }
}
