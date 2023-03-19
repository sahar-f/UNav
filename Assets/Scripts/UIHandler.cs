using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHandler : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_InputField searchField;
    private OrthoCameraController cameraController;
    public bool Events_Visible = false;
    public bool Restaurants_Visible = false;
    public bool Services_Visible = false;
    private SystemHandler syshand;

    private List<string> originalOptions = new List<string>();
    

    private void Start()
    {
        // Get the original options list from the dropdown
        dropdown.gameObject.SetActive(false);
        dropdown.ClearOptions();
        Globals.Create_Building_List();
        originalOptions = Globals.Building_Names;
        // dropdown.AddOptions(Globals.Building_Names);
        dropdown.value = -1;
        // Add a listener to the input field's value changed event
        searchField.onValueChanged.AddListener(OnSearchValueChanged);
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        cameraController = FindObjectOfType<OrthoCameraController>();
        syshand = FindObjectOfType<SystemHandler>();



    }

    private void OnSearchValueChanged(string searchValue)
    {
        if (searchValue.Length == 0)
        {
            dropdown.Hide();
            dropdown.gameObject.SetActive(false);
        }
        else 
        {
            dropdown.gameObject.SetActive(true);
        }
                
        List<string> filteredOptions = originalOptions.Where(option => option.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        filteredOptions.Insert(0, "---------------");
        // Clear the dropdown options list and add the filtered options



        // Update the search field's text after the dropdown menu has been updated
        searchField.text = searchValue;
        dropdown.Hide();
        dropdown.ClearOptions();
       
        dropdown.AddOptions(filteredOptions);
        dropdown.Show();

        searchField.ActivateInputField();
        searchField.selectionAnchorPosition = searchField.selectionFocusPosition = searchField.text.Length;
        
    }
    private void OnDropdownValueChanged(int index)
    {
        Debug.Log("INDEX : " + index);
        // Close the dropdown when an option is selected
        dropdown.Hide();
       
        searchField.text = dropdown.options[index].text;
        dropdown.gameObject.SetActive(false);

       GameObject target = GameObject.Find(searchField.text);
      if (target != null) { }
       cameraController.PanToGameObject(target.transform.position);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // Close the dropdown when the user clicks outside of it
        dropdown.Hide();
       
        dropdown.gameObject.SetActive(false);
    }
   
    public void Events_ClickHandler()
    {
        if (Events_Visible)
        {
            syshand.Events_Destroy();
            Events_Visible= false;
        }
        else
        {
            syshand.Events_Create();
            Events_Visible= true;
        }
    }
    public void Rstr_ClickHandler()
    {
        if (Restaurants_Visible)
        {
            syshand.Rstr_Destroy();
            Restaurants_Visible = false;
        }
        else
        {
            syshand.Rstr_Create();
            Restaurants_Visible = true;
        }
    }
    public void Services_ClickHandler()
    {
        if (Services_Visible)
        {
            syshand.Services_Destroy();
            Services_Visible = false;
        }
        else
        {
            syshand.Services_Create();
            Services_Visible = true;
        }
    }
}


