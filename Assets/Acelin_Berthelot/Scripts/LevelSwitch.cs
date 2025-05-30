using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

internal class LevelSwitch : MonoBehaviour
{
    private GameManagerAce gameManagerAce;  

    [SerializeField] internal string nextLevel;  

    private void Start()
    {
        gameManagerAce = GameObject.FindGameObjectWithTag("GameManagerAce").GetComponent<GameManagerAce>();
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.CompareTag("PlayerAce"))
        {
            if (gameManagerAce.currentPickups >= gameManagerAce.maxPickups && gameManagerAce.levelComplete)
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}
