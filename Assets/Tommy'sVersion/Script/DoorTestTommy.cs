using UnityEngine;

public class DoorTest : MonoBehaviour
{
    public Animation animation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Use this for initialization
    void Start () {
    
}
	
// Update is called once per frame
void Update () {

if (Input.GetKeyDown("f"))
       	GetComponent<Animation>().Play();
}

}
