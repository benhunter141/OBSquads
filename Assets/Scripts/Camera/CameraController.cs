using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraView cameraView;
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (cam.orthographic == false && cameraView == CameraView.TopDown)
        {
            SetCameraToTopDown();
        }
        else if (cam.orthographic == true && cameraView == CameraView.IsoMetric)
        {
            SetCameraToIsoMetric();
        }
    }

    void SetCameraToTopDown()
    {
        Vector3 position = new Vector3(0, 15, 0);
        Quaternion orientation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
        transform.position = position;
        transform.rotation = orientation;
        cam.orthographic = true;
    }

    void SetCameraToIsoMetric()
    {
        Vector3 position = new Vector3(9, 4, 0);
        Quaternion orientation = Quaternion.Euler(new Vector3(20, -90, 0));
        transform.position = position;
        transform.rotation = orientation;
        cam.orthographic = false;
    }
}

public enum CameraView
{   TopDown,
    IsoMetric}

