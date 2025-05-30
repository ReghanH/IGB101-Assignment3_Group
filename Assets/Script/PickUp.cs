using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.transform.CompareTag("Player"))
        {
            gameManager.currentPickups += 1;
            Destroy(this.gameObject);
        }
    }
}
    // Update is called once per frame

