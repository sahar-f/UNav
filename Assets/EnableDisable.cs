using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using OpenMapsV3;

public class EnableDisable : MonoBehaviour{
    public GameObject eventFrame;
    public bool isEnabled = true;

    public void ButtonClicked()
    {
        isEnabled = !isEnabled;
        eventFrame.SetActive(isEnabled);
    }
    //public void ToggleMarkers()
    //{
     //   OpenMapsMarkerManager markerManager = GetComponent<OpenMapsMarkerManager>();
      //  markerManager.ToggleAllMarkers();
    //}
}
