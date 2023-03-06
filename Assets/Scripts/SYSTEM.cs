using System.Collections;
using System.Collections.Generic;


using UnityEngine;




public class SYSTEM : MonoBehaviour

{
    public struct Mapstruct
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
    public struct Camerastruct
    {
        public Camera objectbase;
        public double location_x, location_y, location_z;
        public double rotation_x, rotation_y, rotation_z;
        public bool orthographic;
        public double orthographicSize;
        public string tag;

    }



    public static int Number_of_Maps = 3;
    public static int Number_of_Cameras = 1;
    public static Mapstruct[] maps;
    public static Camerastruct[] cams;



    
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
        List<Mapstruct> mapslist = new List<Mapstruct>();
        for (int currentmapindex = 0; currentmapindex < Number_of_Maps; currentmapindex++)
        {
            Mapstruct map = new Mapstruct();
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
        List<Camerastruct> camslist = new List<Camerastruct>();
        for (int currentcamindex = 0; currentcamindex < Number_of_Cameras; currentcamindex++)
        {
            Camerastruct cam = new Camerastruct();
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
    
}
