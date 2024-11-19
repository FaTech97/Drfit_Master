using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float speedRotateX = 5;
    public float speedRotateY = 5;
    
    private void Update()
    {
        PCCameraControl();
        TouchCameraControll();
    }

    private void TouchCameraControll()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Quaternion rotation = Quaternion.Euler(0f, touch.deltaPosition.x * 0.25f, 0f);
                transform.rotation = rotation * transform.rotation;
            }
        }
    }

    private void PCCameraControl()
    {
        if (!Input.GetMouseButton(0))
            return;

        float rotX = Input.GetAxis("Mouse X") * speedRotateX * Mathf.Deg2Rad;
        // float rotY = Input.GetAxis("Mouse ScrollWheel") * speedRotateY;

        if (Mathf.Abs(rotX) > 0)
            transform.RotateAroundLocal(transform.up, rotX);
        // if (Mathf.Abs(rotY) > 0 && Camera.main.orthographicSize > 50 && Camera.main.orthographicSize < 130)
        // if (Mathf.Abs(rotY) > 0 )
        //     Camera.main.orthographicSize += 1 * rotY;
    }
}
