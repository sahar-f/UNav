using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class SystemHandler : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
        Globals.EventData test1 = new();
        test1.date = "April 5 2023";
        test1.event_start = "5PM";
        test1.event_end = "7PM";
        test1.location = "The Bowl";
        test1.x_location = 10;
        test1.y_location = 50;
        test1.z_location = 30;
        test1.title = "Free Pancakes";
        test1.available = "Everyone";
        test1.name = "Event1";
        test1.event_info = "This is HOW you Get Free Pancakes.  You spend a year making an app and then trying to convince others to use your product so you can always know where them yummy golden tasty, omg, i'm so hungry while i'm writing this. ";

        Globals.Events_Add(test1);

        Globals.EventData test2 = new();
        test2.event_start = "9AM";
        test2.date = "March 30 2023";
        test2.event_end = "4PM";
        test2.location = "Athabasca Hall";
        test2.x_location = 15;
        test2.y_location = 55;
        test2.z_location = 400;
        test2.title = "BBQ";
        test2.available = "College of Engineering Students Only";
        test2.name = "event2";
        test2.event_info = "Come Eat Some Food";

        Globals.Events_Add(test2);





    }
    public void Events_Create()
    {
        if (Globals.Events_List.Count > 0)
        {
            for (int x = 0; x < Globals.Events_List.Count; x++)
            {
                Globals.EventData evt = Globals.Events_List[x];
                GameObject prefab = Resources.Load<GameObject>("Marker");
                evt.event_object = Instantiate(prefab);
                evt.event_object.name = evt.name;
                evt.event_object.tag = "Events";

                evt.event_object.transform.position = new Vector3((float)evt.x_location, (float)evt.y_location, (float)evt.z_location);
                // evt.event_object.transform.localScale = new Vector3(100,100,100);
                
                // Add the script to the GameObject
                EventHandler evthand = evt.event_object.AddComponent<EventHandler>();

            }

        }
        else
        { Debug.Log("UNAV: No Events Present in System"); }

    }

    public void Events_Destroy()
    {
        GameObject[] events = GameObject.FindGameObjectsWithTag("Events");

        foreach (GameObject ev in events)
        {
            Destroy(ev);
        }

    }


    public void Rstr_Create()
    {
        if (Globals.Events_List.Count > 0)
        {
            for (int x = 0; x < Globals.Events_List.Count; x++)
            {
                Globals.EventData evt = Globals.Events_List[x];
                GameObject prefab = Resources.Load<GameObject>("Marker");
                evt.event_object = Instantiate(prefab);
                evt.event_object.name = evt.name;
                evt.event_object.tag = "Events";

                evt.event_object.transform.position = new Vector3((float)evt.x_location, (float)evt.y_location, (float)evt.z_location);
                // evt.event_object.transform.localScale = new Vector3(100,100,100);
            }

        }
        else
        { Debug.Log("UNAV: No Events Present in System"); }

    }

    public void Rstr_Destroy()
    {
        GameObject[] events = GameObject.FindGameObjectsWithTag("Events");

        foreach (GameObject ev in events)
        {
            Destroy(ev);
        }

    }



    public void Services_Create()
    {
        if (Globals.Events_List.Count > 0)
        {
            for (int x = 0; x < Globals.Events_List.Count; x++)
            {
                Globals.EventData evt = Globals.Events_List[x];
                GameObject prefab = Resources.Load<GameObject>("Marker");
                evt.event_object = Instantiate(prefab);
                evt.event_object.name = evt.name;
                evt.event_object.tag = "Events";

                evt.event_object.transform.position = new Vector3((float)evt.x_location, (float)evt.y_location, (float)evt.z_location);
                // evt.event_object.transform.localScale = new Vector3(100,100,100);
            }

        }
        else
        { Debug.Log("UNAV: No Events Present in System"); }

    }

    public void Services_Destroy()
    {
        GameObject[] events = GameObject.FindGameObjectsWithTag("Events");

        foreach (GameObject ev in events)
        {
            Destroy(ev);
        }

    }



}
