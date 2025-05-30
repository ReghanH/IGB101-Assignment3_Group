using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] internal GameObject[] cameraNodes;
    [SerializeField] internal int cameraIndex = 0;

    [SerializeField] internal GameObject[] objects;

    [SerializeField] internal float moveSpeed = 1.0f;  // Base speed for movement (Inspector-controlled)
    [SerializeField] internal float rotSpeed = 1.0f;   // Base speed for rotation (Inspector-controlled)

    [SerializeField] internal float initialDelay = 5.0f;  // Initial delay before the camera starts moving
    [SerializeField] internal float transitionDelay = 0f; // Delay between node transitions (set to 0 for no pause)
    private float delayTimer = 0f;  // Timer to track the delay before moving or between nodes

    private bool isTransitioning = false;  // Flag to check if the camera is transitioning

    [SerializeField] internal Quaternion targetRotation;

    // Fog density control
    [SerializeField] internal float fogDensityAtNode = 0.05f;  // Example fog density for a node

    // Animation curve for smooth movement (controls the acceleration and deceleration)
    [SerializeField] internal AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);  // Default easing curve (Ease In/Out)

    private float transitionTime = 0f;  // Timer to control how much time the camera has been moving

    // Start is called before the first frame update
    internal void Awake()
    {
        Cursor.visible = false;
    }

    void Start()
    {
        // Initialize the timer with the initial delay
        delayTimer = initialDelay;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Movement();
    }

    internal void Movement()
    {
        // Ensure cameraNodes and objects arrays are not empty
        if (cameraNodes.Length == 0 || objects.Length == 0) return;

        // If we're in a delay state, count down the timer
        if (delayTimer > 0f)
        {
            delayTimer -= Time.deltaTime;
            return;  // Don't move yet, just wait for the delay to end
        }

        // Once the delay timer is done, start transitioning to the current node if not already transitioning
        if (!isTransitioning)
        {
            isTransitioning = true;
            transitionTime = 0f;  // Reset the transition time to start the new transition

            // Move smoothly towards the current node's position
            StartCoroutine(TransitionToNode());
        }
    }

    private IEnumerator TransitionToNode()
    {
        Vector3 targetPosition = cameraNodes[cameraIndex].transform.position;
        Quaternion targetRotation = cameraNodes[cameraIndex].transform.rotation;

        // Smoothly move the camera towards the position of the current node using an animation curve
        while (transitionTime < 1f)
        {
            // Use the AnimationCurve to modify the "Lerp" based on the curve's value
            float curveValue = movementCurve.Evaluate(transitionTime);  // Get value from the curve based on the time

            // Apply the base speed, adjusting by the curve value (this slows it down without speeding it up)
            float adjustedMoveSpeed = moveSpeed * curveValue;
            float adjustedRotSpeed = rotSpeed * curveValue;

            transform.position = Vector3.Lerp(transform.position, targetPosition, adjustedMoveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, adjustedRotSpeed * Time.deltaTime);

            transitionTime += Time.deltaTime / moveSpeed;  // Normalize the time and use moveSpeed for consistency
            yield return null;  // Wait until the next frame, but continue moving
        }

        // Ensure the camera reaches the exact target position and rotation
        transform.position = targetPosition;
        transform.rotation = targetRotation;

        // Wait for a delay before transitioning to the next node
        if (transitionDelay > 0f)
        {
            yield return new WaitForSeconds(transitionDelay); // Delay between nodes
        }

        // Move to the next node
        cameraIndex = (cameraIndex + 1) % cameraNodes.Length;
        isTransitioning = false;

        // Reset the delay timer for the next transition (set to 0 for immediate transition)
        delayTimer = transitionDelay;
    }
    
}
