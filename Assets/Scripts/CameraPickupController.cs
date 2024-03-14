using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPickupController : MonoBehaviour, IInteractible
{
    public GameObject doorLoad;
    public GameObject doorLoadTrigger;
    public void Interact(){
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().unlockCamera();

        //Unlock door
        doorLoad.GetComponent<LockedDoorTriggerController>().enabled=false;
        doorLoadTrigger.SetActive(true);

        this.gameObject.SetActive(false);
    }
}
