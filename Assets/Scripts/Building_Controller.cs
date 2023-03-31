using UnityEngine;

public class Building_Controller : MonoBehaviour
{
    private float lastTapTime;
    private const float tapThreshold = 0.2f;
    private OrthoCameraController cameraController;
    private Building_Info building_info;
   


    private void Start()
    {
        cameraController = FindObjectOfType<OrthoCameraController>();
        building_info = GetComponent<Building_Info>();

        // Set the Top of The Building as the Default
        MeshRenderer floor1_mesh = GameObject.Find("Floor1").GetComponent<MeshRenderer>();
        floor1_mesh.enabled = false;
        Renderer myRenderer = building_info.Floor_1.GetComponent<Renderer>();
        myRenderer.enabled = false;
        myRenderer = building_info.Floor_2.GetComponent<Renderer>();
        myRenderer.enabled = false;
        myRenderer = building_info.Floor_3.GetComponent<Renderer>();
        myRenderer.enabled = false;
        myRenderer = building_info.Floor_Top.GetComponent<Renderer>();
        myRenderer.enabled = true;
        GpsLocation.setHeight(701f);
        building_info.floor_cur = 1;

    }

    void OnMouseDown()
    {
        if (Time.time - lastTapTime < tapThreshold)
            {
            // Double tap detected, do something
           
            GameObject myObject = GameObject.Find("Cylinder");
           // myObject.transform.position = new Vector3(gameObject.transform.position.x,60,gameObject.transform.position.z);
           
            Debug.Log("Double tap detected on " + gameObject.name);

            if (building_info.inside_building == false)
            {
                cameraController.Enter_Building(building_info);
                Renderer myRenderer = building_info.Floor_1.GetComponent<Renderer>();
                MeshRenderer floor1_mesh = GameObject.Find("Floor1").GetComponent<MeshRenderer>();

                floor1_mesh.enabled = true;
                myRenderer.enabled = true;
                GpsLocation.setHeight(726f);
                myRenderer = building_info.Floor_2.GetComponent<Renderer>();
                myRenderer.enabled = false;
                myRenderer = building_info.Floor_3.GetComponent<Renderer>();
                myRenderer.enabled = false;
                myRenderer = building_info.Floor_Top.GetComponent<Renderer>();
                myRenderer.enabled = false;
                building_info.floor_cur = 1;
            }
        }

            // Record the time of this tap
            lastTapTime = Time.time;

    }
    public void Set_Top()
    {
        Renderer myRenderer = building_info.Floor_1.GetComponent<Renderer>();
        myRenderer.enabled = false;
        myRenderer = building_info.Floor_2.GetComponent<Renderer>();
        myRenderer.enabled = false;
        myRenderer = building_info.Floor_3.GetComponent<Renderer>();
        myRenderer.enabled = false;
        myRenderer = building_info.Floor_Top.GetComponent<Renderer>();
        myRenderer.enabled = true;
        GpsLocation.setHeight(701f);


    }
    public void Flick_Up()
        // This is poorly writen, it should do it automatically, not hard coded like it is.
    {
        if (building_info.floor_cur == 1)
        {
            Renderer myRenderer = building_info.Floor_1.GetComponent<Renderer>();
           
            MeshRenderer floor1_mesh = GameObject.Find("Floor1").GetComponent<MeshRenderer>();
          
            floor1_mesh.enabled = false;
            myRenderer.enabled = false;
            myRenderer = building_info.Floor_2.GetComponent<Renderer>();
            myRenderer.enabled = true;
            GpsLocation.setHeight(751f);
            myRenderer = building_info.Floor_3.GetComponent<Renderer>();
            myRenderer.enabled = false;
            myRenderer = building_info.Floor_Top.GetComponent<Renderer>();
            myRenderer.enabled = false;
            building_info.floor_cur = 2;
            return;
        }
        else if (building_info.floor_cur == 2)
        {
            Renderer myRenderer = building_info.Floor_1.GetComponent<Renderer>();
            myRenderer.enabled = false;
            myRenderer = building_info.Floor_2.GetComponent<Renderer>();
            myRenderer.enabled = false;
            myRenderer = building_info.Floor_3.GetComponent<Renderer>();
            myRenderer.enabled = true;
            myRenderer = building_info.Floor_Top.GetComponent<Renderer>();
            myRenderer.enabled = false;
            building_info.floor_cur = 3;
            return;

        }
        
    }

    public void Flick_Down()
    // This is poorly writen, it should do it automatically, not hard coded like it is.
    {
        if (building_info.floor_cur == 3)
        {
            Renderer myRenderer = building_info.Floor_1.GetComponent<Renderer>();
            myRenderer.enabled = false;
            myRenderer = building_info.Floor_2.GetComponent<Renderer>();
            myRenderer.enabled = true;
            GpsLocation.setHeight(751f);
            myRenderer = building_info.Floor_3.GetComponent<Renderer>();
            myRenderer.enabled = false;
            myRenderer = building_info.Floor_Top.GetComponent<Renderer>();
            myRenderer.enabled = false;
            building_info.floor_cur = 2;
            return;
        }
        else if (building_info.floor_cur == 2)
        {
            Renderer myRenderer = building_info.Floor_1.GetComponent<Renderer>();
            myRenderer.enabled = true;
            GpsLocation.setHeight(726f);
            myRenderer = building_info.Floor_2.GetComponent<Renderer>();
            myRenderer.enabled = false;
            myRenderer = building_info.Floor_3.GetComponent<Renderer>();
            myRenderer.enabled = false;
            myRenderer = building_info.Floor_Top.GetComponent<Renderer>();
            myRenderer.enabled = false;
            building_info.floor_cur = 1;
            return;
        }

    }
   

}



