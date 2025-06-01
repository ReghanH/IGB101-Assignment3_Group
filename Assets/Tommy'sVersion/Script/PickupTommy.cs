using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupTommy : MonoBehaviour
{
    GameManagerTommy gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManagerTommy").GetComponent<GameManagerTommy>();
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
