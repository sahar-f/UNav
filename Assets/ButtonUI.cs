using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string newEvent = "Event";
    public void NewEventButton()
    {
        SceneManager.LoadScene(newEvent);
    }
}
