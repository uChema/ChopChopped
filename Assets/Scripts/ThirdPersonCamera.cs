using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2 (-40, 85);
    public float scrollSpeed = 2.0f; 
    public float minDstFromTarget = 1.0f; 
    public float maxDstFromTarget = 5.0f; 

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    void Start() {
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate () 
    {
        yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);
        
        dstFromTarget -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        dstFromTarget = Mathf.Clamp(dstFromTarget, minDstFromTarget, maxDstFromTarget);

        currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;

    }

}

