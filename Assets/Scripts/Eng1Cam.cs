using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eng1Cam : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 100;
    private GameObject Player_marker;
    public float rate = 5;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = 50;
        //Player_marker = GameObject.Find("Player");
        //Vector3 targetPosition = Player_marker.transform.position;

        //Camera.main.transform.position = new Vector3(targetPosition.x, 100f, targetPosition.z);
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetMouseButton(0));

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50));

        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            float difference = currentMagnitude - prevMagnitude;
            zoom(difference);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50));
            Debug.Log("Direction: " + direction);
            Debug.Log("Start" + touchStart);
            Debug.Log("Mouse" + Input.mousePosition);
            Debug.Log("Camera Mouse: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));

            Camera.main.transform.position += direction;
        }
        zoom(10 * (Input.GetAxis("Mouse ScrollWheel")));
    }
    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - (increment * rate), zoomOutMin, zoomOutMax);
        //Debug.Log("Size: " + Camera.main.orthographicSize);
    }
}
