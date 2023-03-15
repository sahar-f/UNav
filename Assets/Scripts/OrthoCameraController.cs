using UnityEngine;

public class OrthoCameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float zoomSpeed = 20f;
    public float minZoom = 5f;
    public float maxZoom = 50f;
    public float zoomLerpSpeed = 5f;

    private Camera mainCamera;
    private Vector3 lastPanPosition;
    private float lastZoomDistance;
    private bool fingersDown = false;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
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
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0f)
                {
                    float newOrthoSize = Mathf.Clamp(mainCamera.orthographicSize - scroll * zoomSpeed * 10f, minZoom, maxZoom);
                    mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newOrthoSize, zoomLerpSpeed * Time.deltaTime);
                }
            }
        }

        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                lastPanPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - lastPanPosition;
                transform.Translate(-delta.x * panSpeed * Time.deltaTime, 0, -delta.y * panSpeed * Time.deltaTime, Space.World);
                lastPanPosition = Input.mousePosition;
            }
        }

        
    }

    public void PanToGameObject(Vector3 targetPos)
    {
        targetPos.y = transform.position.y; // keep the y position constant
        Vector3 cameraPos = new Vector3(targetPos.x, transform.position.y, targetPos.z); // create a new camera position with the x and z positions of the target

        transform.position = cameraPos;
    }
}


