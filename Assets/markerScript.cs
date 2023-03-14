
using UnityEngine;

public class markerScript : MonoBehaviour
{

    [SerializeField] private GameObject _markerPrefab;

    private void Start()
    {


        // Create a new marker object
        var marker = Instantiate(_markerPrefab);
        marker.transform.SetParent(transform);

        // Set the marker's location
        var location = new Vector2(37, -122);


        // Set the marker's rotation and scale
        marker.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        marker.transform.localScale = new Vector3(10f, 10f, 10f);
    }
}
