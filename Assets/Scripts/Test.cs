using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
    public GameObject plane; // Reference to the plane where the marker will be placed
    public float scale = 1.75f; // Scale of the marker
    private bool isGPSEnabled = false; // Flag to check if GPS is enabled
    private Vector2 gpsLocation = Vector2.zero; // GPS location of the user

    IEnumerator Start()
    {
        // Start GPS service
        /*Input.location.Start();


        // Wait until the location service is initialized
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the location service is not enabled or failed to initialize, show an error message
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location.");
            yield break;
        }
        else*/
            plane = GameObject.Find("Player");
            isGPSEnabled = true;
            gpsLocation = new Vector2(52.13220f, -106.63023f);
        yield break;
        
    }

    void Update()
    {
        if (isGPSEnabled)
        {
            // Convert GPS location to world position
            Vector3 worldPos = LatLonToUnityCoords(gpsLocation.x, gpsLocation.y);

            // Map world position to plane position
            Vector3 planePos = plane.transform.InverseTransformPoint(worldPos);

            // Set marker position and scale
            transform.localPosition = new Vector3(planePos.x, 0f, planePos.z);
            transform.localScale = Vector3.one * scale;
        }
    }

    // Convert latitude and longitude to Unity world coordinates
    Vector3 LatLonToUnityCoords(float lat, float lon)
    {
        float y = 0f; // Set the Y position to 0 since we are mapping the marker onto a plane
        float x = (lon + 180f) / 360f; // Convert longitude to X position (0-1 range)
        float z = (90f - lat) / 180f; // Convert latitude to Z position (0-1 range)
        return new Vector3(x, y, z) * 10f; // Multiply by 10 to scale the coordinates
    }

    void OnDisable()
    {
        // Stop GPS service when the script is disabled
        Input.location.Stop();
    }
}