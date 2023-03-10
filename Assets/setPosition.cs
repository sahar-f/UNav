using UnityEngine;

public class TopRightCorner : MonoBehaviour
{
    public GameObject myObjectPrefab; // Prefab for the game object to create

    private void Start()
    {
        // Create the game object from the prefab
        GameObject myObject = Instantiate(myObjectPrefab);

        // Get the screen dimensions in pixels
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;

        // Convert the screen dimensions to world coordinates
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, 0f));

        // Set the game object's position to the top-right corner of the screen
        myObject.transform.position = new Vector3(screenTopRight.x, screenTopRight.y, 0f);
    }
}

