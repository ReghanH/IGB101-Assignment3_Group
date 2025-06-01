using UnityEngine;
using UnityEngine.UI;

public class DooropenReghan : MonoBehaviour

{
    Animation animationDoor;
    public float doorProximity = 1.0f;
    public GameObject player;
    public Text doorText;
    public AudioSource doorAudio;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animationDoor = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {

        if (Vector3.Distance(player.transform.position, this.transform.position) < doorProximity)
        {
            doorText.enabled = true;
            if (Input.GetKeyDown("f"))
            {
                animationDoor.Play();
                doorAudio.Play();
            }
        }
        else
        {
            doorText.enabled=false;
        }
    }
}
