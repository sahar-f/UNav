using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{


    public string theName;
    public string theName1;
    public GameObject inputField;
    public GameObject inputField1;
    


    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;
        theName1 = inputField1.GetComponent<Text>().text;
        Debug.Log("NAV   East: " + theName);
        Debug.Log("NAV   North: " + theName1);
    }
   
   
}
