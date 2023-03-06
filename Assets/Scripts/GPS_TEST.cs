using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GPS_TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("KRIS UNAV YPU YPUP YPUP");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"UNAV GPS MercadorX: {GPS_MANAGER.location_x}   MercadorY: {GPS_MANAGER.location_y}  Accuracy: {GPS_MANAGER.horizontal_accuracy}    Altitude: {GPS_MANAGER.location_z}    Accuracy {GPS_MANAGER.vertical_accuracy} ");

    }
}
