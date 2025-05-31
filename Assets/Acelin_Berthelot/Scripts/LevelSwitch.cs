using UnityEngine;
using UnityEngine.SceneManagement;

internal class LevelSwitch : MonoBehaviour
{
    [Header("Scene to load when requirements are met")]
    [SerializeField] private string nextLevel = "";

    [Header("Pickups needed for THIS switch")]
    [SerializeField] private int pickupsRequired = 3;

    private GameManagerAce gameManagerAce;

    private void Awake()
    {
        // New API (Unity 2023+)
        gameManagerAce = Object.FindFirstObjectByType<GameManagerAce>();

        // Fallback for older Unity versions (optional)
#if !UNITY_2023_1_OR_NEWER
        if (gameManagerAce == null)
            gameManagerAce = FindObjectOfType<GameManagerAce>();
#endif

        if (gameManagerAce == null)
            Debug.LogError("GameManagerAce not found in the scene!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerAce") || gameManagerAce == null) return;

        if (gameManagerAce.currentPickups >= pickupsRequired)
            SceneManager.LoadScene(nextLevel);
    }
}
