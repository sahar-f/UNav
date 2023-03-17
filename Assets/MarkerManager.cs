/*
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarkerManager : MonoBehaviour
{
    public GameObject markerPrefab;
    public List<MarkerData> markerDataList;

    private List<GameObject> markers = new List<GameObject>();

    private void Start()
    {
        foreach (var markerData in markerDataList)
        {
            var markerGameObject = Instantiate(markerPrefab);
            markerGameObject.transform.SetParent(transform);

            var marker = markerGameObject.GetComponent<OnlineMapsMarker>();
            marker.position = markerData.coordinates;
            marker.label = markerData.title;

            var markerPopup = markerGameObject.GetComponent<markerPopup>();
            markerPopup.SetPopup(markerData.title, markerData.description, markerData.image);

            markerGameObject.SetActive(false);
            markers.Add(markerGameObject);
        }
    }

    public void ToggleMarkers(Button button)
    {
        var active = !markers[0].activeSelf;
        foreach (var marker in markers)
        {
            marker.SetActive(active);
        }

        var buttonText = active ? "Hide Markers" : "Show Markers";
        button.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
    }
}

[System.Serializable]
public class MarkerData
{
    public string title;
    public string description;
    public Sprite image;
    public Vector2 coordinates;
}
*/
