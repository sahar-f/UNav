using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.FlowStateWidget;

public static class Globals
{

    public static bool Events_Visible = false;
    public static bool Services_Visible = false;
    public static bool Restaurants_Visible = false;
    public static GameObject Buildings;
    public static List<EventData> Events_List= new List<EventData>();
    public static List<RestaurantData> Restaurant_List= new List<RestaurantData>();
    public static List<ServicesData> Services_List= new List<ServicesData>();
    public static List<string>Building_Names= new List<string>();

    public struct EventData
    {
        public string name;
        public double x_location;
        public double y_location;
        public double z_location;
        public DateTime event_start;
        public DateTime event_end;
        public string event_info;
        public string event_type;
        public GameObject event_object;

    }
    public struct RestaurantData
    {
        public string name;
        public double x_location;
        public double y_location;
        public double z_location;
        public DateTime event_start;
        public DateTime event_end;
        public string event_info;
        public string event_type;
        public GameObject event_object;

    }
    public struct ServicesData
    {
        public string name;
        public double x_location;
        public double y_location;
        public double z_location;
        public DateTime event_start;
        public DateTime event_end;
        public string event_info;
        public string event_type;
        public GameObject event_object;

    }

    public static void Events_Add(EventData evt)
    {
        // Needs Error Checking
        Events_List.Add(evt);

    }
    public static void Resturant_Add(RestaurantData rst)
    {
        // needs error checking
        Restaurant_List.Add(rst);
    }
    public static void Services_Add(ServicesData svt)
    {
        //needs error checking
        Services_List.Add(svt);
    }
    public static void Create_Building_List()
    {
        if (Building_Names.Count == 0)
        {
            GameObject[] myGameObjects = GameObject.FindGameObjectsWithTag("Buildings");
            foreach (GameObject go in myGameObjects)
            {
                Building_Names.Add(go.name);
                Debug.Log(go.name);
            }
        }
    }

    


}
        


