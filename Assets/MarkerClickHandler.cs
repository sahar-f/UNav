//using UnityEngine;
//using UnityEngine.UI;

//public class MarkerClickHandler : MonoBehaviour
//{
    //public GameObject popupPanelPrefab;

    //private void OnMouseUpAsButton()
    //{
        // Create the pop-up panel
        //GameObject popupPanel = Instantiate(popupPanelPrefab, transform.position, Quaternion.identity);

        // Set the content of the panel
      //  Text popupText = popupPanel.GetComponentInChildren<Text>();
    //    popupText.text = "Marker Information";

        // Enable the panel
  //      popupPanel.SetActive(true);
//    }
//}


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MarkerClickHandler : MonoBehaviour, IPointerClickHandler
{

    public GameObject popupWindow; // reference to the pop-up window object

    public void OnPointerClick(PointerEventData eventData)
    {
        // display the pop-up window when the user clicks on the marker
        popupWindow.SetActive(true);
    }
}
