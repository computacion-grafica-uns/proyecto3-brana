using UnityEngine;

public class RotateScript : MonoBehaviour {
    public float degreesPerSecond = 45.0f;
    void Start() { }

    void Update() {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(
            currentRotation.x,
            currentRotation.y + degreesPerSecond * Time.deltaTime,
            currentRotation.z
        );
    }
}
