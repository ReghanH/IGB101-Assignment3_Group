using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAce : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public int currentPickups = 0;
    [SerializeField] public int maxPickups = 5;
    [SerializeField] public bool levelComplete = false;
    [SerializeField] public Text pickupText;
    [SerializeField] public AudioSource[] audiosources;
    [SerializeField] public float audioproximity = 5f;


    void Update()
    {
        LevelCompleteCheck();
        UpdateGUI();
        PlayAudioSamples();
    }

    private void LevelCompleteCheck()
    {
        if(currentPickups >= maxPickups)
            levelComplete = true;
        else 
            levelComplete = false;
    }

    private void UpdateGUI()
    {
        pickupText.text = "Pickups: " + currentPickups + "/" + maxPickups;
    }

    private void PlayAudioSamples()
    {
        for(int i = 0; i < audiosources.Length; i++) 
        {
            if (Vector3.Distance(player.transform.position, audiosources[i].transform.position) < audioproximity)
            {
                if(!audiosources[i].isPlaying)
                {
                    audiosources[i].Play();
                }
                
            }
        }
    }
    

}
