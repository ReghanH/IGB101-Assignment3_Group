using UnityEngine;

public class ToggleChildObject : MonoBehaviour
{
    // Reference to the child object
    public GameObject childObject;

    // Update is called once per frame
    void Update()
    {
        // Check if the "T" key is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Toggle the active state of the child object
            childObject.SetActive(!childObject.activeSelf);
        }
    }
}
