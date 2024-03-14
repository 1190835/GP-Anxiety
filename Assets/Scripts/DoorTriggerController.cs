using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTriggerController : MonoBehaviour, IInteractible
{
    public void Interact(){
        if(SceneManager.GetActiveScene().buildIndex==4){
            SceneManager.LoadScene("SideHallwayOtherworld");
        }
        if(SceneManager.GetActiveScene().buildIndex==5){
            SceneManager.LoadScene("MainHallwayOtherworld");
        }
    }
}
