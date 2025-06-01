using System.Collections.Generic;
using UnityEngine;

public class ToggleChildObject : MonoBehaviour
{
    [Header("List of child objects to toggle")]
    [SerializeField] private List<GameObject> childObjects;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (GameObject obj in childObjects)
            {
                if (obj != null)
                    obj.SetActive(!obj.activeSelf);
            }
        }
    }
}
