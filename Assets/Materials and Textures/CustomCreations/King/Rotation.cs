using UnityEngine;

public class CubeRotation : MonoBehaviour
{

        public float rotationSpeed = 100f; // Adjust rotation speed as desired

        void Update()
        {
            // Rotate the cube around the Y-axis
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    
}
