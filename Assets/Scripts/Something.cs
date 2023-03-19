using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Something : MonoBehaviour, IPointerClickHandler
{
    public GameObject panelObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        // If the click was outside of the Panel, make it invisible
        if (!RectTransformUtility.RectangleContainsScreenPoint(panelObject.GetComponent<RectTransform>(), eventData.position, eventData.enterEventCamera))
        {
            panelObject.SetActive(false);
        }
    }
}