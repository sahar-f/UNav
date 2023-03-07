using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 600;
    public float zoomOutMax = 22000;
    public float rate = 200;
    Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Camera>();
        myCamera.orthographicSize = 3000;
    }
   

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetMouseButton(0));

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = myCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,2000));

        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            float difference = currentMagnitude- prevMagnitude;
            zoom(difference);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - myCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2000));
            Debug.Log("Direction: " + direction);
            Debug.Log("Start" + touchStart);
            Debug.Log("Mouse" + Input.mousePosition);
            Debug.Log("Camera Mouse: " + myCamera.ScreenToWorldPoint(Input.mousePosition));

            myCamera.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
    void zoom(float increment)
    {
        myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize - (increment * rate), zoomOutMin, zoomOutMax);
        //Debug.Log("Size: " + Camera.main.orthographicSize);
    }
}
