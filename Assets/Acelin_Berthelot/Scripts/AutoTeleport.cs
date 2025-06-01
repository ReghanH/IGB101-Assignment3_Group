using UnityEngine;

public class AutoTeleport : MonoBehaviour
{
    [SerializeField] private Transform player;          
    [SerializeField] private Transform teleportTarget;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player && teleportTarget != null)
        {
            player.position = teleportTarget.position;
        }
    }
}
