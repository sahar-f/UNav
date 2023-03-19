using UnityEngine;
using TMPro;



public class EventHandler : MonoBehaviour
{
    private float lastTapTime;
    private const float tapThreshold = 0.2f;
    public GameObject EventV;
    private GameObject tempObject;
    private Globals.EventData dta;

    private void Start()
    {
        Debug.Log("EVENT HANDLER STARTING");
    

    }

    void OnMouseDown()
    {
        if (Time.time - lastTapTime < tapThreshold)
        {
            
            for (int i=0;i < Globals.Events_List.Count;i++) {
                if (Globals.Events_List[i].name == gameObject.name)
                { 
                    dta = Globals.Events_List[i]; 
                    break; }
            }
            
            
            // Double tap detected, do something
            
            Globals.EventViewer.SetActive(true);
            Globals.EventViewer_Visible = true;



            TextMeshProUGUI m_Title = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
            m_Title.text = dta.title;
            TextMeshProUGUI m_Date = GameObject.Find("Text Date").GetComponent<TextMeshProUGUI>();
            m_Date.text = dta.date;
            TextMeshProUGUI m_Time = GameObject.Find("Text Time").GetComponent<TextMeshProUGUI>();
            m_Time.text = dta.event_start + " - " + dta.event_end; 
            TextMeshProUGUI m_Location = GameObject.Find("Text Location").GetComponent<TextMeshProUGUI>();
            m_Location.text = dta.location;
            TextMeshProUGUI m_Available = GameObject.Find("Text Available").GetComponent<TextMeshProUGUI>();
            m_Available.text = dta.available;
            TextMeshProUGUI m_info = GameObject.Find("Text Info").GetComponent<TextMeshProUGUI>();
            m_info.text = dta.event_info;



            Debug.Log("Double tap detected on " + gameObject.name);

        }

        // Record the time of this tap
        lastTapTime = Time.time;

    }
    

    


}