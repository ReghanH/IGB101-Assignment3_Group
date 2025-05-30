using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupAce  : MonoBehaviour
{
    GameManagerAce gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManagerAce").GetComponent<GameManagerAce>();
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        if(otherObject.transform.tag == "PlayerAce")
        {
            gameManager.currentPickups += 1;
            Destroy(this.gameObject);
        }
    }



}
