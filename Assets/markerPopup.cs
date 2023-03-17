/*
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InfinityCode.OnlineMapsExamples;
using System.Collections.Generic; // Added using statement for Dictionary

public class markerPopup : MonoBehaviour
{
    public Text title;
    public Text description;

    void Start()
    {
        OnlineMaps.instance.OnMarkerClick += OnMarkerClick;
    }

    void OnMarkerClick(OnlineMapsMarkerBase marker)
    {
        title.text = marker.label;

        // Get the marker's custom data dictionary
        var data = marker.customData as Dictionary<string, object>;
        if (data != null && data.ContainsKey("description"))
        {
            // Get the description from the custom data dictionary
            description.text = data["description"] as string;
        }
        else
        {
            description.text = "";
        }

        // Show the pop-up
        gameObject.SetActive(true);
    }
}
*/