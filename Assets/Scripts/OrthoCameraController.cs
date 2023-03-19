using UnityEngine;

public class OrthoCameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    public float zoomSpeed = 20f;
    public float minZoom = 5f;
    public float maxZoom = 50f;
    public float zoomLerpSpeed = 5f;

    private Camera mainCamera;
    private Vector3 lastPanPosition;
    private float lastZoomDistance;
    private bool fingersDown = false;
    public bool inside_building = false;
    private Building_Info building_info;
    public float inside_building_flick_strength;
    public Building_Controller building;
    public float Time_Old;
    public float Time_Delay;
    public float Time_Now;


    public void Enter_Building(Building_Info info)
    {
        building_info= info;
        building_info.inside_building = true;
        inside_building= true;
        Vector3 objectPosition = new Vector3(building_info.Center_X, building_info.Center_Y, building_info.Center_Z);
        PanToGameObject(objectPosition);
        RotateToGameObject(building_info.Rotate_Camera);
        ZoomTo(building_info.Zoom_Max);


    }
    public float GetZoom()
    { 
        if (mainCamera == null)
        {
            Debug.Log("MAIN CAMERA FUCKED");
        }
        return mainCamera.orthographicSize; 
    }


    public void Exit_Building() 
    {
        inside_building= false;
        building_info.inside_building = false;
        Renderer myRenderer = building_info.Floor_1.GetComponent<Renderer>();
        myRenderer.enabled = false;
        myRenderer = building_info.Floor_2.GetComponent<Renderer>();
        myRenderer.enabled = false;
        myRenderer = building_info.Floor_3.GetComponent<Renderer>();
        myRenderer.enabled = false;
        myRenderer = building_info.Floor_Top.GetComponent<Renderer>();
        myRenderer.enabled = true;
        Vector3 objectPosition = new Vector3(building_info.Center_X, building_info.Center_Y, building_info.Center_Z);
        PanToGameObject(objectPosition);
        RotateToGameObject(-building_info.Rotate_Camera);
        ZoomTo(2500f);
    }

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        Time_Now = Time.time;
        if (!inside_building)
        {
            // Handle panning
            if (!fingersDown && Input.touchCount == 2)
            {
                fingersDown = true;
            }
            else if (fingersDown && Input.touchCount < 1)
            {
                fingersDown = false;
            }

            if (fingersDown)
            {
                // Handle zooming
                if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                    {
                        lastZoomDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    }
                    else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
                    {
                        float zoomDistance = Vector2.Distance(touchZero.position, touchOne.position);
                        float deltaZoom = lastZoomDistance - zoomDistance;
                        float newOrthoSize = Mathf.Clamp(mainCamera.orthographicSize + deltaZoom * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
                        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newOrthoSize, zoomLerpSpeed * Time.deltaTime);
                        lastZoomDistance = zoomDistance;
                    }
                }
                else
                {

                }
            }

            else
            {
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0f)
                {
                    float newOrthoSize = Mathf.Clamp(mainCamera.orthographicSize - scroll * zoomSpeed * 400f, minZoom, maxZoom);
                    mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newOrthoSize, zoomLerpSpeed * Time.deltaTime);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    lastPanPosition = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0))
                {

                    Vector3 delta = Input.mousePosition - lastPanPosition;
                    transform.Translate(-delta.x * panSpeed * Time.deltaTime, -delta.y * panSpeed * Time.deltaTime, 0f, Space.Self);
                    lastPanPosition = Input.mousePosition;
                    if (delta.x > 0 || delta.y > 0 || delta.z > 0)
                    {
                        Debug.Log("Screen: " + delta);
                    }

                }
            }
        }
        else
        {

            // Inside a Building
            //    Pan to the Extents of building_info.Pan_Left_Max
            //                          building_info.Pan_Right_Max
            //                          building_info.Pan_Up_Max
            //                          building_info.Pan_Down_Max
            //   Pinch to Zoom          Max: building_info.Zoom_Max
            //                          Min: building_info.Zoom_Min
            //   Flick (This will switch the Floors. Up goes Up)
            //                          inside_building_flick_strength will set the difference between pan and flick

            // Handle panning
            if (!fingersDown && Input.touchCount == 2)
            {
                fingersDown = true;
            }
            else if (fingersDown && Input.touchCount < 1)
            {
                fingersDown = false;
            }

            if (fingersDown)
            {
                // Handle zooming
                if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                    {
                        lastZoomDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    }
                    else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
                    {
                        float zoomDistance = Vector2.Distance(touchZero.position, touchOne.position);
                        float deltaZoom = lastZoomDistance - zoomDistance;
                        float newOrthoSize = Mathf.Clamp(mainCamera.orthographicSize + deltaZoom * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
                        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newOrthoSize, zoomLerpSpeed * Time.deltaTime);
                        lastZoomDistance = zoomDistance;
                        if (newOrthoSize > 2500)
                        { Exit_Building(); }
                    }
                }
                else
                {

                }
            }

            else
            {
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0f)
                {
                    float newOrthoSize = Mathf.Clamp(mainCamera.orthographicSize - scroll * zoomSpeed * 400f, minZoom, maxZoom);
                    mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newOrthoSize, zoomLerpSpeed * Time.deltaTime);
                    if (newOrthoSize > 2500)
                    { Exit_Building(); }
                }
                // Handle Pan/Flick
                if (Input.GetMouseButtonDown(0))
                {
                    lastPanPosition = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0))
                {
                    Vector3 delta = Input.mousePosition - lastPanPosition;
                    if (delta.y > 0f)
                    {
                        Debug.Log("DELTA:" + delta.y);
                        Debug.Log("Delta Time: " + (Time.time - Time_Old));
                        
                    }
                    
                    // This Is not a Pan but a Flick Up
                    if (delta.y > building_info.insidebuilding_flick_strength)
                    {
                        if (Time.time - Time_Old > Time_Delay)
                        {
                            
                            building = building_info.Floor_Top.GetComponentInParent<Building_Controller>();
                            building.Flick_Up();
                        }
                        Time_Old = Time.time;

                    }
                    // This is Not a Pan it's a Flick Down
                    else if (delta.y < -building_info.insidebuilding_flick_strength)
                    {
                        if (Time.time - Time_Old > Time_Delay)
                        {
                            building = building_info.Floor_Top.GetComponentInParent<Building_Controller>();
                            building.Flick_Down();
                        }
                        Time_Old = Time.time;
                    }
                    // This is a Pan
                    else
                    {
                        
                        transform.Translate(-delta.x * panSpeed * Time.deltaTime, -delta.y * panSpeed * Time.deltaTime, 0f, Space.Self);
                        lastPanPosition = Input.mousePosition;
                    }

                }
            }
        }
       // FindClosestGameObjectPerpendicularToCamera();

        
    }

    public void PanToGameObject(Vector3 targetPos)
    {
        targetPos.y = transform.position.y; // keep the y position constant
        Vector3 cameraPos = new Vector3(targetPos.x, transform.position.y, targetPos.z); // create a new camera position with the x and z positions of the target

        transform.position = cameraPos;
    }
    public void RotateToGameObject(float z_amount)
    {
        transform.Rotate(0f, 0f, z_amount);
    }
    public void ZoomTo(float zoom_amount)
    {
        Debug.Log("CameraSize Actual  : " + mainCamera.orthographicSize);
        Debug.Log("CameraSize Should  : " + zoom_amount);
        mainCamera.orthographicSize = zoom_amount;
    }
    void FindClosestGameObjectPerpendicularToCamera()
    {
        // Get the position and view direction of the camera
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraDirection = Camera.main.transform.forward;

        RaycastHit[] hits = Physics.RaycastAll(cameraPosition, cameraDirection);
        float closestDistance = float.MaxValue;
        GameObject closestObject = null;

        // Iterate through the collided GameObjects to find the closest one perpendicular to the camera view
        foreach (RaycastHit hit in hits)
        {
            // Calculate the distance between the GameObject and the camera along the camera view direction
            float distance = Vector3.Dot(hit.transform.position - cameraPosition, cameraDirection);

            // Check if the GameObject is closer than the current closest GameObject
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = hit.collider.gameObject;
            }
        }

        if (closestObject != null)
        {
            Debug.Log("Closest GameObject perpendicular to camera: " + closestObject.name);
        }
    }
}


