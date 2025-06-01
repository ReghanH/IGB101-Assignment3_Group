using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch_WenqiWang : MonoBehaviour  
{
    GameManager_WenqiWang gameManager_WenqiWang;
    public string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        gameManager_WenqiWang = GameObject.FindGameObjectWithTag("GameManager_WenqiWang").GetComponent<GameManager_WenqiWang>();
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        if(otherObject.transform.tag == "Player")
        {
            if(gameManager_WenqiWang.levelComplete)
            {
                SceneManager.LoadScene(nextLevel);  
            }
        }
    }
}