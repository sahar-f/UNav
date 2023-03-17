using UnityEngine;
using UnityEngine.UI;



    //public void OnButtonClick()
    //{
        // Toggle the markers on or off
       // markersVisible = !markersVisible;
     //   markerManager.enabled = markersVisible;
   // }




public class markerToggle : MonoBehaviour
{
    public OnlineMapsMarkerManager markerManager;
    public OnlineMapsMarker[] markers;

    private void Start()
    {
        markerManager = OnlineMapsMarkerManager.instance;

        markers = new OnlineMapsMarker[]
        {
            OnlineMapsMarkerManager.CreateItem(-106.630089426724, 52.1323247011608, "Structures Lab"),
            OnlineMapsMarkerManager.CreateItem(-106.631694789855, 52.1330123315228, "Ag Cafe"),
            OnlineMapsMarkerManager.CreateItem(-106.630107098177, 52.1321703660822, "SESS Office"),
            // add as many markers as needed
        };
    }

    //public void ToggleMarker(int markerIndex)
    //{
    //    markers[markerIndex].enabled = !markers[markerIndex].enabled;
    //}

    public void ToggleMarker(int index)
    {
        markers[index].enabled = !markers[index].enabled; // toggle the marker on or off

        if (markers[index].enabled)
        {
           // markers[index].GetComponent<UIBubblePopup>().Show(); // show the popup
        }

        else
        {
            //markers[index].GetComponent<UIBubblePopup>().Hide(); // hide the popup
        }
    }


    public void OnMarker1ButtonClick()
    {
        ToggleMarker(0);
    }

    public void OnMarker2ButtonClick()
    {
        ToggleMarker(1);
    }

    public void OnMarker3ButtonClick()
    {
        ToggleMarker(2);
    }

    // add additional methods for each button and marker
}
