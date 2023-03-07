using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class SYSTEM : MonoBehaviour

{
    public struct Map_struct
    {
        public string tag;
        public double location_x, location_y, location_z;
        public double scale_x, scale_y, scale_z;
        public double rotation_x, rotation_y, rotation_z;
        public double refpoint1_x, refpoint1_y, refpoint1_z;
        public double refpoint1_lat, refpoint1_lon, refpoint1_alt;
        public double refpoint2_x, refpoint2_y, refpoint2_z;
        public double refpoint2_lat, refpoint2_lon, refpoint2_alt;
        public string texture;
        public GameObject objectbase;

    }
    public struct Camera_struct
    {
        public Camera objectbase;
        public double location_x, location_y, location_z;
        public double rotation_x, rotation_y, rotation_z;
        public bool orthographic;
        public double orthographicSize;
        public string tag;

    }
    public struct Partner_struct
    {
        public int ID;
        public string name;
        public int Building_ID;
        public int Room_ID;
        public string Phone;
        public string Email;
        public string Website;
        public string Hours_Open_Monday;
        public string Hours_Close_Monday;
        public string Hours_Open_Tuesday;
        public string Hours_Close_Tuesday;
        public string Hours_Open_Wednesday;
        public string Hours_Close_Wednesday;
        public string Hours_Open_Thursday;
        public string Hours_Close_Thursday;
        public string Hours_Open_Friday;
        public string Hours_Close_Friday;
        public string Hours_Open_Saturday;
        public string Hours_Close_Saturday;
        public string Hours_Open_Sunday;
        public string Hours_Close_Sunday;
        public string Hours_Open_Holiday;
        public string Hours_Close_Holiday;
        public string Info1;
        public string Info2;
        public string Info3;
        public string Info4;
        public string Info5;
        

    }
    public struct Restaurant_struct
    {
        public int ID;
        public string name;
        public int Building_ID;
        public int Room_ID;
        public string Phone;
        public string Email;
        public string Website;
        public string Hours_Open_Monday;
        public string Hours_Close_Monday;
        public string Hours_Open_Tuesday;
        public string Hours_Close_Tuesday;
        public string Hours_Open_Wednesday;
        public string Hours_Close_Wednesday;
        public string Hours_Open_Thursday;
        public string Hours_Close_Thursday;
        public string Hours_Open_Friday;
        public string Hours_Close_Friday;
        public string Hours_Open_Saturday;
        public string Hours_Close_Saturday;
        public string Hours_Open_Sunday;
        public string Hours_Close_Sunday;
        public string Hours_Open_Holiday;
        public string Hours_Close_Holiday;
        public string Info1;
        public string Info2;
        public string Info3;
        public string Info4;
        public string Info5;

    }
    public struct Services_struct
    {
        public int ID;
        public string name;
        public int Building_ID;
        public int Room_ID;
        public string Phone;
        public string Email;
        public string Website;
        public string Hours_Open_Monday;
        public string Hours_Close_Monday;
        public string Hours_Open_Tuesday;
        public string Hours_Close_Tuesday;
        public string Hours_Open_Wednesday;
        public string Hours_Close_Wednesday;
        public string Hours_Open_Thursday;
        public string Hours_Close_Thursday;
        public string Hours_Open_Friday;
        public string Hours_Close_Friday;
        public string Hours_Open_Saturday;
        public string Hours_Close_Saturday;
        public string Hours_Open_Sunday;
        public string Hours_Close_Sunday;
        public string Hours_Open_Holiday;
        public string Hours_Close_Holiday;
        public string Info1;
        public string Info2;
        public string Info3;
        public string Info4;
        public string Info5;

    }
    public struct Events_struct
    {
        public int ID;
        public int OWNER_ID;
        public string OWNER_TABLE;
        public string DATE_START;
        public string DATE_END;
        public string TIME_START;
        public string TIME_END;
        public string OPEN_TO;
        public string TITLE;
        public string INFO1;
        public string INFO2;
        public string INFO3;
        public string WEBSITE;
        public int EventType;
        public double Location_Lat;
        public double Location_Lon;
        public double Location_Alt;
        public int Location_Building;
        public int Location_Room;
        public int PUBLISHED;

    }



    public static int Number_of_Maps = 3;
    public static int Number_of_Cameras = 1;
    public static Map_struct[] maps;
    public static Camera_struct[] cams;
    public static Restaurant_struct[] restaurants;
    public static Partner_struct[] partners;
    public static Services_struct[] services;
    public static Events_struct[] events;


    
    // Start is called before the first frame update
    void Start()
    {
        system_Update_Db();  // this should be the first thing that is called.  It will connect to the webapi
                             // and update the internal SQLLITE db with the remote one
        system_Load_Scripts(); // this is the 2nd thing that is called.  It will attach all the needed scripts
                               // to the main gameObject.  THIS script is already attached (via editor not script) to the main
                               // gameObject.
        system_Load_Maps();     // From the LocalDatabase, load and create all the maps
        system_Load_Cameras();  // From the localDatabase, load and create all the cameras
        system_GPS_FAKEPOINTS();

    }

    


    private void system_Load_Scripts()
    {
        GameObject currentObject = gameObject;    // current object will reference the parent object of the script
        GPS_MANAGER gps_manager = currentObject.AddComponent<GPS_MANAGER>(); // load the GPS manager script
        DATABASE_MANAGER database_manager = currentObject.AddComponent<DATABASE_MANAGER>(); // load the database mananger script
        
    }

    private void system_Update_Db()
    {
        
    }
    private void system_create_MAPS() 
    {
    
    }
 private void Load_Map_Data()
    {
        


    }
    private void system_Load_Maps()
    {
        DATABASE_MANAGER db = GetComponent<DATABASE_MANAGER>();   // get Access to the DB script
        List<Map_struct> mapslist = new List<Map_struct>();
        for (int currentmapindex = 0; currentmapindex < Number_of_Maps; currentmapindex++)
        {
            Map_struct map = new Map_struct();
            db.Retreive_Map(ref map, currentmapindex +1);        // Pass the MainMap struct and get the DB to fill it in for us, we only want the 
            mapslist.Add(map);

        }
        Debug.Log(mapslist.Count);

        maps = mapslist.ToArray();

        for (int idx = 0; idx < Number_of_Maps; idx++)
        {
            Debug.Log(maps[idx].tag);
            maps[idx].objectbase = GameObject.CreatePrimitive(PrimitiveType.Plane);
            maps[idx].objectbase.name = maps[idx].tag;
            Texture2D texture = Resources.Load<Texture2D>(maps[idx].texture);
            // Android BUGFIX incase the Shader.Find("Standard") fails  
            Shader standardShader = Shader.Find("Standard");
            Shader fallbackShader = Shader.Find("Diffuse");

            Material material = new Material(standardShader != null ? standardShader : fallbackShader);
            material.mainTexture = texture;
            material.color = Color.white;

            // Set the plane's material to the new material
            Renderer renderer = maps[idx].objectbase.GetComponent<Renderer>();
            renderer.material = material;
            maps[idx].objectbase.transform.position = new Vector3((float)maps[idx].location_x, (float)maps[idx].location_y, (float)maps[idx].location_z);
            maps[idx].objectbase.transform.rotation = Quaternion.identity;
            maps[idx].objectbase.transform.Rotate((float)maps[idx].rotation_x, (float)maps[idx].rotation_y, (float)maps[idx].rotation_z);
            maps[idx].objectbase.transform.localScale = new Vector3((float)maps[idx].scale_x, (float)maps[idx].scale_y, (float)maps[idx].scale_z);
        }
    }
   
    private void system_Load_Cameras() 
    {
        DATABASE_MANAGER db = GetComponent<DATABASE_MANAGER>();   // get Access to the DB script
        List<Camera_struct> camslist = new List<Camera_struct>();
        for (int currentcamindex = 0; currentcamindex < Number_of_Cameras; currentcamindex++)
        {
            Camera_struct cam = new Camera_struct();
            db.Retreive_Camera(ref cam, currentcamindex + 1);        // Pass the MainMap struct and get the DB to fill it in for us, we only want the 
            camslist.Add(cam);

        }
        Debug.Log(camslist.Count);

        cams = camslist.ToArray();

        for (int idx = 0; idx < Number_of_Cameras; idx++)
        {
            Debug.Log(cams[idx].tag);
            cams[idx].objectbase = new GameObject(cams[idx].tag).AddComponent<Camera>();
            cams[idx].objectbase.name = cams[idx].tag;
           
            cams[idx].objectbase.transform.position = new Vector3((float)cams[idx].location_x, (float)cams[idx].location_y, (float)cams[idx].location_z);
            cams[idx].objectbase.transform.rotation = Quaternion.identity;
            cams[idx].objectbase.transform.Rotate((float)cams[idx].rotation_x, (float)cams[idx].rotation_y, (float)cams[idx].rotation_z);
            cams[idx].objectbase.orthographic = cams[idx].orthographic;
            cams[idx].objectbase.orthographicSize = (float)cams[idx].orthographicSize;
            cams[idx].objectbase.AddComponent<Camera_Controller>();
        }

    }
    private void system_Gps_Start() {
        
       

    }

    private void system_GPS_FAKEPOINTS()
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(-221f,0f , 369f);
        cylinder.transform.localScale = new Vector3(100, 200, 100);
        cylinder.transform.rotation = Quaternion.identity;
        
    }
    
    private void EVENTS_VISIBLE()
    { }
    private void EVENTS_HIDE()
    { }
    private void EVENTS_GetINFO(int idx)
    { }
}
