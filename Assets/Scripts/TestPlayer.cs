using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestPlayer : MonoBehaviour
{   
    
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Object touched player");
        }
    }
    // Update is called once per frame
    void Update()
    {
        // if (marker.transform.position.y == target.transform.position.y){
        //     cam.transform.position = new Vector3(0, 49f, 0);
        // }
        
    }
}
