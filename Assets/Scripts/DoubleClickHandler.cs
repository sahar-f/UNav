using UnityEngine;

public class DoubleClickHandler : MonoBehaviour
{
    private float lastTapTime;
    private const float tapThreshold = 0.2f;
    private OrthoCameraController cameraController;

    private void Start()
    {
        cameraController = FindObjectOfType<OrthoCameraController>();
    }

    void OnMouseDown()
    {
        if (Time.time - lastTapTime < tapThreshold)
            {
            // Double tap detected, do something
            Debug.Log("GameObjectbbbbb: " + gameObject.transform.position);
            GameObject myObject = GameObject.Find("Cylinder");
            myObject.transform.position = new Vector3(gameObject.transform.position.x,60,gameObject.transform.position.z);
           
            Debug.Log("Double tap detected on " + gameObject.name);
            
            Vector3 objectPosition = gameObject.transform.position;
            cameraController.PanToGameObject(objectPosition);
               
            }

            // Record the time of this tap
            lastTapTime = Time.time;

        }
}



