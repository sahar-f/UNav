using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Info : MonoBehaviour
{
    public float Rotate_Camera;
    public float Pan_Left_Max;
    public float Pan_Right_Max;
    public float Pan_Up_Max;
    public float Pan_Down_Max;
    public float Center_X;
    public float Center_Y;
    public float Center_Z;
    public float Zoom_Max;
    public float Zoom_Min;
    public GameObject Floor_Top;
    public GameObject Floor_1;
    public GameObject Floor_2;
    public GameObject Floor_3;
    public int floor_cur;
    public int floor_max;
    public int floor_min;
    public float insidebuilding_flick_strength;
    public bool inside_building;
    // Start is called before the first frame update
    void Start()
    {
        inside_building = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
