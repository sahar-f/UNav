using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class GpsLocation : MonoBehaviour
{
    private GameObject me_marker;
    private GameObject fake_marker;
    private GameObject fake_marker1;
    private GameObject fake_marker2;
    private GameObject fake_marker3;
    private double LatRef = 52.131059;
    private double LonRef = -106.634043;
    private double MerRef_X;
    private double MerRef_Y;
    private double ScreenRefY = 522;
    private double ScreenRefX = -235;
    private double LatRef2 = 52.134830;
    private double LonRef2 = -106.622736;
    private double MerRef_X2;
    private double MerRef_Y2;
    private double ScreenRefY2 = 2470;
    private double ScreenRefX2 = 3775;
    public float nScaleRate = 1f;
    public float eScaleRate = 1f;
    public static float height=701f;

    // Start is called before the first frame update
    void Start()
    {
        me_marker = GameObject.Find("GPSMarker");
        fake_marker = GameObject.Find("GPSMarker (1)");
        fake_marker1 = GameObject.Find("GPSMarker (2)");
        fake_marker2 = GameObject.Find("GPSMarker (3)");
        fake_marker3 = GameObject.Find("GPSMarker (4)");
        Vector2 tempRef = LatLongToWebMercader(LatRef, LonRef);
        MerRef_X= tempRef.x;
        MerRef_Y= tempRef.y;
        Vector2 tempRef2 = LatLongToWebMercader(LatRef2, LonRef2);
        MerRef_X2 = tempRef2.x;
        MerRef_Y2 = tempRef2.y;
        nScaleRate = (float)((float)(ScreenRefY2 - ScreenRefY) / (MerRef_Y2 - MerRef_Y));
        eScaleRate = (float)((float)(ScreenRefX2 - ScreenRefX) / (MerRef_X2 - MerRef_X));
        Debug.Log("ME    nscale:" + nScaleRate);
        Debug.Log("ME    escale:" + eScaleRate);
        nScaleRate = 2.8180f;
        eScaleRate = 3.1870f;
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

    public static void setHeight(float y_value){
        height=y_value;
    }
    private void UpdateGPSData() 
    {
       
        if (Input.location.status == LocationServiceStatus.Running)
        {
            //access to GPS values and it has been init
            Debug.Log("GPS Should be working");
            
         
            float Current_Lat = Input.location.lastData.latitude;
            Debug.Log("ME LAT: " + Current_Lat);
            float Current_Lon = Input.location.lastData.longitude;
            Debug.Log("ME LON: " + Current_Lon);
            Vector2 map = LatLongToWebMercader((double) Current_Lat, (double) Current_Lon);
            float X_Distance = map.x - (float)MerRef_X;
            float Y_Distance =  map.y - (float)MerRef_Y;
            Debug.Log("ME X Distance : " + X_Distance);
            Debug.Log("ME Y Distance : " + Y_Distance);
            
            me_marker.transform.position = new Vector3((float)ScreenRefX + (X_Distance *eScaleRate), height, (float)ScreenRefY +(Y_Distance * nScaleRate));
            Debug.Log("ME X: "+ me_marker.transform.position.x);
            Debug.Log("ME Z: " + me_marker.transform.position.z);








            float Current_Lat1 = 52.132269f;
            float Current_Lon1 = -106.631717f;
            Vector2 map1 = LatLongToWebMercader((double)Current_Lat1, (double)Current_Lon1);
            float X_Distance1 = map1.x - (float)MerRef_X;
            float Y_Distance1 = map1.y - (float)MerRef_Y;
            fake_marker.transform.position = new Vector3((float)ScreenRefX + (X_Distance1 * eScaleRate), 200, (float)ScreenRefY + (Y_Distance1 * nScaleRate));


            float Current_Lat2 = 52.130008f;
            float Current_Lon2 = -106.638785f;
            Vector2 map2 = LatLongToWebMercader((double)Current_Lat2, (double)Current_Lon2);
            float X_Distance2 = map2.x - (float)MerRef_X;
            float Y_Distance2 = map2.y - (float)MerRef_Y;
            fake_marker1.transform.position = new Vector3((float)ScreenRefX + (X_Distance2 * eScaleRate), 200, (float)ScreenRefY + (Y_Distance2 * nScaleRate));


            float Current_Lat3 = 52.139122f;
            float Current_Lon3 = -106.630077f;
            Vector2 map3 = LatLongToWebMercader((double)Current_Lat3, (double)Current_Lon3);
            float X_Distance3 = map3.x - (float)MerRef_X;
            float Y_Distance3 = map3.y - (float)MerRef_Y;
            fake_marker2.transform.position = new Vector3((float)ScreenRefX + (X_Distance3 * eScaleRate), 200, (float)ScreenRefY + (Y_Distance3 * nScaleRate));

            float Current_Lat4 = 52.131412f;
            float Current_Lon4 = -106.633758f;
            Vector2 map4 = LatLongToWebMercader((double)Current_Lat4, (double)Current_Lon4);
            float X_Distance4 = map4.x - (float)MerRef_X;
            float Y_Distance4 = map4.y - (float)MerRef_Y;
            fake_marker3.transform.position = new Vector3((float)ScreenRefX + (X_Distance4 * eScaleRate), 200, (float)ScreenRefY + (Y_Distance4 * nScaleRate));






        }
        else
        {
            Debug.Log("GPSUpdate: Service Not Available");
            // service is stopped
        }

    } // end of UpdateDateGPSData

    public static Vector2 LatLongToWebMercader(double lat, double lon)
    {

        const double EarthRadius = 6378137.0;
        double x = lon * EarthRadius * Mathf.Deg2Rad;
        double y = Mathf.Log(Mathf.Tan((float)((90 + lat) * Mathf.Deg2Rad / 2.0))) * EarthRadius;
        return new Vector2((float)x, (float)y);
    }
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
