using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupReghan : MonoBehaviour
{
    GameManagerReghan gameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManagerReghan").GetComponent<GameManagerReghan>();
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        if(otherObject.transform.tag == "PlayerReghan")
        {
            gameManager.currentPickups += 1;
            Destroy(this.gameObject);
        }
    }
}
