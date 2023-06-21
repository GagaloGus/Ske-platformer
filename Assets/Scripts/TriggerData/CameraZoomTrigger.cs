using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomTrigger : MonoBehaviour
{
    public float cameraZoom, cameraZoomDefault;
    public bool enterRight;
    private void Start()
    {
        cameraZoomDefault = Camera.main.GetComponent<Camera>().orthographicSize;
    }
}
