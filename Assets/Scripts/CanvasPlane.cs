using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasPlane : MonoBehaviour
{
    public RectTransform canvas;
    public TMP_Text textPrefab;
    public float leftMargin = 0.15f;
    public float rightMargin = 0.15f;
    public float bottomMargin = 0.15f;
    public float topMargin = 0.3f;

    private RectTransform plane;
    private TMP_Text[] texts = new TMP_Text[8];

    private struct EventData
    {
        public string text1;
        public string text2;
        public string text3;
        public string text4;
        public string text5;
        public string text6;
        public string text7;
        public string text8;
    }

    private EventData Get_Event()
    {
        // Implementation of the Get_Event function is not provided, as it is dependent on your specific application
        // In this example, we will simply create a dummy EventData object with some example text
        EventData eventData;
        eventData.text1 = "Text 1";
        eventData.text2 = "Text 2";
        eventData.text3 = "Text 3";
        eventData.text4 = "Text 4";
        eventData.text5 = "Text 5";
        eventData.text6 = "Text 6";
        eventData.text7 = "Text 7";
        eventData.text8 = "Text 8";
        return eventData;
    }

    void Start()
    {
        // Get the EventData object
        EventData eventData = Get_Event();

        // Calculate the size and position of the plane
        float width = canvas.rect.width - (canvas.rect.width * (leftMargin + rightMargin));
        float height = canvas.rect.height - (canvas.rect.height * (topMargin + bottomMargin));
        float x = canvas.rect.width * leftMargin;
        float y = canvas.rect.height * bottomMargin;

        // Create the plane and set its size and position
        plane = new GameObject("Plane").AddComponent<RectTransform>();
        plane.SetParent(canvas.transform, false);
        plane.anchorMin = new Vector2(0, 0);
        plane.anchorMax = new Vector2(1, 1);
        plane.sizeDelta = new Vector2(width, height);
        plane.anchoredPosition = new Vector2(x, y);

        // Add text areas to the plane
        for (int i = 0; i < 8; i++)
        {
            texts[i] = Instantiate(textPrefab, plane.transform);
            texts[i].text = GetTextFromEventData(eventData, i);
            texts[i].rectTransform.anchorMin = new Vector2(0.5f, 1f - ((i + 1) / 9f));
            texts[i].rectTransform.anchorMax = new Vector2(0.5f, 1f - (i / 9f));
            texts[i].rectTransform.offsetMin = new Vector2(10f, 0f);
            texts[i].rectTransform.offsetMax = new Vector2(-10f, -10f);
            texts[i].alignment = TextAlignmentOptions.MidlineLeft;
        }

        // Add a button to the canvas that destroys the plane when clicked
        Button closeButton = Instantiate(Resources.Load<Button>("CloseButton"));
        closeButton.transform.SetParent(canvas.transform, false);
        closeButton.onClick.AddListener(() => Destroy(plane.gameObject));
    }

    private string GetTextFromEventData(EventData eventData, int index)
    {
        switch (index)
        {
            case 0:
                return eventData.text1;
            case 1:
                return eventData.text2;
            case 2:
                return eventData.text3;
            case 3:
                return eventData.text4;
            case 4:
                return eventData.text5;
            case 5:
                return eventData.text6;
            case 6:
                return eventData.text7;
            case 7:
                return eventData.text8;
            default:
                return "";
        }
    }


// Update is called once per frame
void Update()
    {
        // Check if user clicks outside the plane and destroy the plane if so
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            if (!RectTransformUtility.RectangleContainsScreenPoint(plane, mousePosition))
            {
                Destroy(plane.gameObject);
            }
        }
    }
}