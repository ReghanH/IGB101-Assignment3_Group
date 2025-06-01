using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchTommy : MonoBehaviour
{
    GameManagerTommy gameManager;
    public string nextLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManagerTommy").GetComponent<GameManagerTommy>();    
    }
    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.transform.tag == "PlayerTommy")
        {
            if (gameManager.levelComplete)
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
    }

}


