using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    [SerializeField] internal float playDistance = 10f; // Distance within which the audio will play
    [SerializeField] internal AudioSource audioSource;   // Reference to the AudioSource component

    private Camera mainCamera; // Reference to the camera

    void Start()
    {
        // Get the main camera if not already assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // If the audio source is not set, try to get it from the current GameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Check the distance between the camera and the audio source
        float distanceToCamera = Vector3.Distance(mainCamera.transform.position, transform.position);

        // Play audio if within range, stop otherwise
        if (distanceToCamera <= playDistance)
        {
            if (!audioSource.isPlaying) // Only play if the audio isn't already playing
            {
                audioSource.Play();
                Debug.Log("Audio is playing, within camera bounds.");
            }
        }
        else
        {
            if (audioSource.isPlaying) // Stop audio if outside the range
            {
                audioSource.Stop();
                Debug.Log("Audio is stopped, out of camera bounds.");
            }
        }
    }
}
