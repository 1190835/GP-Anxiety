using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPickupController : MonoBehaviour, IInteractible
{
    public GameObject lockedDoorTrigger;
    public GameObject doorLoadTrigger;
    public void Interact(){
        //Give the player the camera
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().unlockCamera();
        //Save first stage time
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().saveFirstStageTime();

        //Unlock door
        lockedDoorTrigger.SetActive(false);
        doorLoadTrigger.SetActive(true);

        this.gameObject.SetActive(false);
    }
}
