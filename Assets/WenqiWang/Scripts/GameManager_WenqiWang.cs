using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_WenqiWang : MonoBehaviour  
{
    public GameObject player;

    //Pickup and Level Completion Logic
    public int currentPickup_WenqiWangs = 0;
    public int maxPickup_WenqiWangs = 5;
    public bool levelComplete = false;

    public Text pickup_WenqiWangText;

    //Audio Proximity Logic
    public AudioSource[] audioSources;
    public float audioProximity = 5.0f;

    // Update is called once per frame
    void Update()  
    {
        LevelCompleteCheck();
        UpdateGUI();
        PlayAudioSamples();
    }

    private void LevelCompleteCheck()
    {
        if (currentPickup_WenqiWangs >= maxPickup_WenqiWangs)
            levelComplete = true;
        else
            levelComplete = false;
    }

    private void UpdateGUI()
    {
        pickup_WenqiWangText.text = "Pickup_WenqiWangs: " + currentPickup_WenqiWangs + "/" + maxPickup_WenqiWangs;
    }

    //Loop for playing audio proximity events - AudioSource based
    private void PlayAudioSamples()
    {
        for (int i = 0; i < audioSources.Length; i++) {
            if(Vector3.Distance(player.transform.position, audioSources[i].transform.position) <= audioProximity){
                if(!audioSources[i].isPlaying){
                    audioSources[i].Play();
                }
            }
        }
    }
}