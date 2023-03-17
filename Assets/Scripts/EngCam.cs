using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraZoom : MonoBehaviour
{
    public float minZoom = 1.0f;
    public float maxZoom = 75.0f;
    public float zoomSpeed = 0.2f;

    private Camera cam;
    private float currentZoom;

    private void Start()
    {
        cam = GetComponent<Camera>();
        currentZoom = cam.orthographicSize;
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            currentZoom += deltaMagnitudeDiff * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            cam.orthographicSize = currentZoom;
        }

        // Keep camera on the plane
        Vector3 position = transform.position;
        position.z = 0;
        transform.position = position;
    }
}