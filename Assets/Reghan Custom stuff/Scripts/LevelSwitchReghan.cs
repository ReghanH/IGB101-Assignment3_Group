using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchReghan : MonoBehaviour
{
    GameManagerReghan gameManager;
    public string nextLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManagerReghan").GetComponent<GameManagerReghan>();
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        if(otherObject.transform.tag == "PlayerReghan")
        {
            if (gameManager.levelComplete)
            {
                SceneManager.LoadScene("Acelin_Main");
            }
        }
    }
}
