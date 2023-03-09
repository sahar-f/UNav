using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Camera_Controller : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 600;
    public float zoomOutMax = 2200;
    public float rate = 5;
    public TMP_InputField inputField;
    public float speed = 5f;
    public GameObject nullTargetMessage;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = 600;
        inputField.onSubmit.AddListener(SetTarget);
    }
   

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetMouseButton(0));

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,2000));

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
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2000));
            Debug.Log("Direction: " + direction);
            Debug.Log("Start" + touchStart);
            Debug.Log("Mouse" + Input.mousePosition);
            Debug.Log("Camera Mouse: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));

            Camera.main.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - (increment * rate), zoomOutMin, zoomOutMax);
        //Debug.Log("Size: " + Camera.main.orthographicSize);
    }

    private void SetTarget(string input)
    {
        GameObject gameObject = GameObject.Find(input);
        print(input);
        if (gameObject != null)
        {
            target = gameObject.transform;
            nullTargetMessage.SetActive(false);
        }
        else
        {
            target = null;
            nullTargetMessage.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
