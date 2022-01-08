using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookController : MonoBehaviour
{
    
    public float sensitivityX = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    float rotationX = 0F;
    Quaternion originalRotation;
    private float originalZoom;
    private Camera _camera;
    
    void Start ()
    {
        originalRotation = transform.localRotation;
        _camera = GetComponent<Camera>();
        originalZoom = _camera.fieldOfView;
    }
    
    public static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp (angle, min, max);
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationX = ClampAngle (rotationX, minimumX, maximumX);
        Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
        transform.localRotation = originalRotation * xQuaternion;
        if (Input.GetMouseButtonDown(1))
        {
            _camera.fieldOfView = originalZoom * 0.5f;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _camera.fieldOfView = originalZoom;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
