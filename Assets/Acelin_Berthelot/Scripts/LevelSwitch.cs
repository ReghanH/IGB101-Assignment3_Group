using UnityEngine;
using UnityEngine.SceneManagement;

internal class LevelSwitch : MonoBehaviour
{
    [Header("Scene to load when requirements are met")]
    [SerializeField] private string nextLevel = "";

    [Header("Pickups needed for THIS switch")]
    [SerializeField] private int pickupsRequired = 10;

    [Header("Audio to play before switching scenes")]
    [SerializeField] private AudioSource audioSource;

    private GameManagerAce gameManagerAce;
    private bool hasTriggered = false;

    private void Awake()
    {
        gameManagerAce = Object.FindFirstObjectByType<GameManagerAce>();

#if !UNITY_2023_1_OR_NEWER
        if (gameManagerAce == null)
            gameManagerAce = FindObjectOfType<GameManagerAce>();
#endif

        if (gameManagerAce == null)
            Debug.LogError("GameManagerAce not found in the scene!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("PlayerAce") || gameManagerAce == null)
            return;

        if (gameManagerAce.currentPickups >= pickupsRequired)
        {
            hasTriggered = true;

            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
                Invoke(nameof(SwitchScene), audioSource.clip.length);
            }
            else
            {
                SwitchScene();
            }
        }
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
