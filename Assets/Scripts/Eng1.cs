using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Eng1 : MonoBehaviour
{
    private GameObject player;
  
    //mid
    private double UTMRefPointN = 5777007.97;
    private double UTMRefPointE = 388499.26; 
    private Vector3 userPosition;
    private bool location_enabled = true;
    private bool flag=false;
    private float distance;
    private float moveSpeed = 5f;
    public float smoothing = 10f;

    //door
    //private double UTMRefPointN = 5776987.77;
    //private double UTMRefPointE = 388407.94;
    //private float nScaleRate = -0.046f;
    //private float eScaleRate = -6.72f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");  
        StartCoroutine(GPSLoc());
    }

   
    IEnumerator GPSLoc()
    {
        // Check if location services are enabled for the app
        Input.location.Start(1f);
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("GPS: Not Enabled By User");
            yield break;   
        }
        //Start the Service
        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait -= 1;
        }
        // Service start up Timed Out
        if (maxWait < 1)
        {
            Debug.Log("GPS: Timed Out");
            yield break;
        }

        // connection failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("GPS: Starting Service Failed");
            yield break;

        }
        else
        {
            //Access Granted
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
            
        }

    } // end of GPSLocz
    private void UpdateGPSData() 
    {

        if (Input.location.status == LocationServiceStatus.Running)
        {
           location_enabled = true;

        }
        else
        {
            Debug.Log("GPSUpdate: Service Not Available");
            location_enabled = false;
            // service is stopped
        }


        
    } // end of UpdateDateGPSData
    private (double NORTH, double EAST) ConvertToUTM(float lat, float lon)
    {
        double[] ret = new double[2];
        double WGS84_Semi_Major = 6378137;
        double WGS84_Semi_Minor = 6356752.314;
        double WGS84_Eccentricity = Math.Sqrt(Math.Pow(WGS84_Semi_Major,2) - Math.Pow(WGS84_Semi_Minor,2)) / WGS84_Semi_Major;
        Debug.Log("Eccentricity : "+ WGS84_Eccentricity);
        double WGS84_Eccentricity_Prime = Math.Sqrt(Math.Pow(WGS84_Semi_Major, 2) - Math.Pow(WGS84_Semi_Minor, 2)) / WGS84_Semi_Minor;
        Debug.Log("Eccentricity Prime : " + WGS84_Eccentricity_Prime);
        double WGS84_Eccentricity_Prime_Squared = Math.Pow(WGS84_Eccentricity_Prime, 2);
        Debug.Log("Eccentricity Prime Squared : " + WGS84_Eccentricity_Prime_Squared);
        double WGS84_PolarRadius = Math.Pow(WGS84_Semi_Major, 2) / WGS84_Semi_Minor;
        double g = lon * Mathf.PI / 180;
        Debug.Log("g :" + g);
        double h = lat * Mathf.PI / 180;
        Debug.Log("h :" + h);
        double i = Math.Truncate((lon / 6) + 31);
        Debug.Log("i :" + i);
        double j = i * 6 - 183;
        Debug.Log("j :" + j);
        double k = g - (j * Math.PI / 180);
        Debug.Log("k :" + k);
        double l = Math.Cos(h) * Math.Sin(k);
        Debug.Log("l :" + l);
        double m = 0.5 * Math.Log((1 +l) / (1-l));
        Debug.Log("m :" + m);
        double n = Math.Atan((Math.Tan(h)) / (Math.Cos(k)))-h;
        Debug.Log("n :" + n);
        double o = (WGS84_PolarRadius / Math.Pow((1 + (WGS84_Eccentricity_Prime_Squared * Math.Pow(Math.Cos(h), 2))), 0.5))*0.9996;
        Debug.Log("o :" + o);
        double p = (WGS84_Eccentricity_Prime_Squared/2)*(Math.Pow(m,2))*(Math.Pow(Math.Cos(h), 2));
        Debug.Log("p :" + p);
        double q = Math.Sin(2 * h);
        Debug.Log("q :" + q);
        double r = q * (Math.Pow(Math.Cos(h),2));
        Debug.Log("r :" + r);
        double s = h + (q / 2);
        Debug.Log("s :" + s);
        double t =( (3 * s) + r) / 4;
        Debug.Log("t :" + t);
        double u = (5 * t + r * Math.Pow(Math.Cos(h), 2)) / 3;
        Debug.Log("u :" + u);
        double v = WGS84_Eccentricity_Prime_Squared * 0.75;
        Debug.Log("v :" + v);
        double w = Math.Pow(v, 2) * 5 / 3;
        Debug.Log("w :" + w);
        double x = Math.Pow(v, 3) * 35 / 27;
        Debug.Log("x :" + x);
        double y = 0.9996 * WGS84_PolarRadius * (h - (v * s) + (w * t) - (x * u));
        Debug.Log("y :" + y);
        double EASTING = m * o * (1 + p / 3) + 500000;
        Debug.Log("EASTING :" + EASTING);
        double NORTHING = n * o * (1 + p) + y;
        Debug.Log("NORTHING :" + NORTHING);
        return (NORTHING, EASTING);

    }
    


    // Update is called once per 
    void Update()
    {
        UpdateGPSData();
        if (location_enabled)
        {
            //access to GPS values and it has been init


            //2c40
            //float Current_Lat = 52.13207f;
            //float Current_Lon = -106.62965f;


            //center
            //float Current_Lat = 52.13233f;
            //float Current_Lon = -106.62904f;


            //A-Wing Hallway
            //float Current_Lat = 52.13300f;
            //float Current_Lon = -106.62866f;


            //A-Wing
            //float Current_Lat= 52.13232f;
            //float Current_Lon = -106.62904f;

            //1B71
            //float Current_Lat = 52.13253f;
            //float Current_Lon = -106.62949f;

            //lounge
            //float Current_Lat = 52.13223f;
            //float Current_Lon = -106.63020f;

            //1b12
            //float Current_Lat = 52.13212f;
            //float Current_Lon = -106.62972f;

            float Current_Lat = Input.location.lastData.latitude;
            float Current_Lon = Input.location.lastData.longitude;
            (double UTMNorth, double UTMEast) = ConvertToUTM(Current_Lat, Current_Lon);
           
            float North_Distance = (float)UTMNorth - (float)UTMRefPointN;
            float East_Distance = (float)UTMEast - (float)UTMRefPointE;

            //Debug.Log("North Distance : " + North_Distance);
            //Debug.Log("East Distance : " + East_Distance);

            userPosition = new Vector3(East_Distance, 0f, North_Distance);
         


            if (!flag)
            {
                //player.setActive(true);
                player.transform.position = userPosition;
                flag = true;
                
            }
            distance = Vector3.Distance(userPosition, player.transform.position);

            if (distance > 0.1f)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, userPosition, Time.deltaTime * moveSpeed);
                transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * smoothing);
            }
            

            //Debug.Log("marker postion: " + player.transform.position);
        }
    }

    void OnApplicationQuit()
    {
        //location service
        Input.location.Stop();
    }
}
