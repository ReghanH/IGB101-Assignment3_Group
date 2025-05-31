using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_WenqiWang : MonoBehaviour  
{
    GameManager_WenqiWang gameManager_WenqiWang;
    // Start is called before the first frame update
    void Start()
    {
        gameManager_WenqiWang = GameObject.FindGameObjectWithTag("GameManager_WenqiWang").GetComponent<GameManager_WenqiWang>();
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        if(otherObject.transform.tag == "Player")
        {
            gameManager_WenqiWang.currentPickup_WenqiWangs += 1;
            Destroy(gameObject);  
        }
    }
}