using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class GpsLocation : MonoBehaviour
{
    private GameObject me_marker;
    private double UTMRefPointN = 5776874.3;
    private double UTMRefPointE = 388153.75;
    private double ScreenRefN =24;
    private double ScreenRefE = -238;
    public float nScaleRate = 4.7f;
    public float eScaleRate = 22.93756f;

    // Start is called before the first frame update
    void Start()
    {
        me_marker = GameObject.Find("Cylinder123");
        StartCoroutine(GPSLoc());
    }


    IEnumerator GPSLoc()
    {
        // Check if location services are enabled for the app
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

    } // end of GPSLoc
    private void UpdateGPSData() 
    {
       
        if (Input.location.status == LocationServiceStatus.Running)
        {
            //access to GPS values and it has been init

            
         
            float Current_Lat = Input.location.lastData.latitude;
            float Current_Lon = Input.location.lastData.longitude;
            (double UTMNorth, double UTMEast) = ConvertToUTM(Current_Lat, Current_Lon);

            float North_Distance = (float)UTMNorth - (float)UTMRefPointN;
            float East_Distance = (float)UTMEast - (float)UTMRefPointE;
            Debug.Log("North Distance : " + North_Distance);
            Debug.Log("East Distance : " + East_Distance);
            
            me_marker.transform.position = new Vector3(East_Distance *5.93f, 200, North_Distance * nScaleRate);
            Debug.Log("ME X: "+ me_marker.transform.position.x);
            Debug.Log("ME Z: " + me_marker.transform.position.z);
        }
        else
        {
            Debug.Log("GPSUpdate: Service Not Available");
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
        
    }
}