using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPickupController : MonoBehaviour, IInteractible
{
    public GameObject lockedDoorTrigger;
    public GameObject doorLoadTrigger;
    public GameObject padlockCodeSprites;
    public void Interact(){
        //Give the player the camera
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().unlockCamera();
        //Save first stage time
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().saveFirstStageTime();

        //Unlock door
        lockedDoorTrigger.SetActive(false);
        doorLoadTrigger.SetActive(true);
        Debug.Log(doorLoadTrigger.active);
        Debug.Log(doorLoadTrigger.tag);
        Debug.Log(doorLoadTrigger.GetComponent<PadlockPuzzleTriggerController>()!=null);

        //Turn off lights
        GameObject room = GameObject.Find("Room");
        foreach(Transform child in room.transform){
            if(child.gameObject.GetComponent<Light>()!=null){
                //Debug.Log(child.gameObject.name);
                child.gameObject.GetComponent<Light>().enabled = false;
            }
        }

        GameObject.FindGameObjectWithTag("SFX").GetComponent<AnxietySFXController>().enabled = true;

        //Write Code on the Wall
        padlockCodeSprites.SetActive(true);

        this.gameObject.SetActive(false);
    }
}
