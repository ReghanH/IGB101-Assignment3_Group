using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    [SerializeField] private float triggerDistance = 5f;
    [SerializeField] private string playerTag = "PlayerAce";

    private Transform player;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= triggerDistance && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}

