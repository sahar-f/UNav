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
        test1.event_start = DateTime.Now;
        test1.event_end = DateTime.Now;
        test1.x_location = 10;
        test1.y_location = 50;
        test1.z_location = 30;

        test1.name = "Test Event";
        test1.event_info = "This is a Test of The Event System";

        Globals.Events_Add(test1);
      



       
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




}
