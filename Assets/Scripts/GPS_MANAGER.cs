using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.Device;
/* The GPSLocation Script Will Do The Following:
 * It will check if the GPS is enabled by the User, If not it will fail
 * If it is Enabled, it will try and start the service with a timeout of maxWait ( in seconds)
 * if the service doesn't start it will fail
 * if the service does start, it will use the InvokeRepeating function
 * to call the UpdateGPS function every 0.5seconds
 * so every 0.5 seconds, it will do the following:
 * get the lat/lon coordinates from the GPS module
 * convert those coordinates to WEB-Mercador
 */

public class GPS_MANAGER : MonoBehaviour
{
    
    // These Variables will be visible to everyone
    public static double location_x = 0;  // X Coordinate
    public static double location_y = 0;  // Y Coordinate
    public static double location_z = 0;  // Z Coordinate
    public static double vertical_accuracy = 0;   // the current vertical accuracy
    public static double horizontal_accuracy = 0; // the current horizontal accuracy
    public static double scalefactor_x = 1; // Map Scaling Factor Along X
    public static double scalefactor_y = 1; // Map Scaling Factor Along Y
    public static double scalefactor_z = 1; // Map Scaling Factor Along Z
    public static int maxWait = 20;         // The Timeout (in seconds) for the GPS to initialize
    public static float initial_delay = 1.0f; // The Initial Delay before Calling the UpdateGPSData Function
    public static float repeat_delay = 1.0f; // The Delay between subsequent calls to the UpdateGPSData Function

     
    // Start is called before the first frame update
    void Start()
    {
     
        StartCoroutine(GPS_Initialize());
    }


    IEnumerator GPS_Initialize()
    {
        // Check if location services are enabled for the app
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("UNAV GPS FAIL: Not Enabled By User");
            yield break;   
        }
        //Start the Service
        Input.location.Start();
        
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait -= 1;
        }
        // Service start up Timed Out
        if (maxWait < 1)
        {
            Debug.Log("UNAV GPS FAIL: Timed Out");
            yield break;
        }

        // connection failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("UNAV GPS FAIL: Starting Service Failed");
            yield break;

        }
        else
        {
            Debug.Log("UNAV GPS SUCCESS:  GPS Initialized And Ready To Receive");
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }

    } // end of GPSLoc

    public static void LatLon2WebMecador( double lat, double lon, out double mercadorx, out double mercadory)
    {
        double earthRadius = 6378137.0; // radius of the earth in meters
        double originShift = 2 * Mathf.PI * earthRadius / 2.0f;

        // Convert latitude and longitude to radians
        double latRad = lat * Mathf.PI / 180.0f;
        double lonRad = lon * Mathf.PI / 180.0f;

        // Calculate x and y in Web Mercator projection
        mercadorx = lonRad * originShift / 180.0;
        mercadory = Mathf.Log(Mathf.Tan((90 + (float)latRad) * Mathf.PI / 360.0f)) / (Mathf.PI / 180.0f);
        mercadory = mercadory * originShift / 180.0;
       
    }
   

    private void UpdateGPSData() 
    {
       
        if (Input.location.status == LocationServiceStatus.Running)
        {
            

            double Current_Lat = Input.location.lastData.latitude;
            double Current_Lon = Input.location.lastData.longitude;
            LatLon2WebMecador(Current_Lat, Current_Lon,out location_x,out location_y);
            location_z = Input.location.lastData.altitude;
            vertical_accuracy = Input.location.lastData.verticalAccuracy;
            horizontal_accuracy= Input.location.lastData.horizontalAccuracy;

            

        }
        else
        {
            Debug.Log("UNAV GPS FAIL: Service Not Available in UpdateGPSData");
            // sevice is stopped
        }

    } // end of UpdateDateGPSData
   
    


    // Update is called once per 
    void Update()
    {
        
    }
}
