using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pickup : MonoBehaviour
{
    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.transform.tag == "Player"){
            gameManager.currentPickups += 1;
            Destroy(this.gameObject);
        }
    }

void Update()
{
    
}
}
