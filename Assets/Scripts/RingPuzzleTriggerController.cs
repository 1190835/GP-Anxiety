using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RingPuzzleTriggerController : MonoBehaviour, IInteractible
{
    public void Interact(){
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().hideUI();
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().hasCamera){
            SceneManager.LoadScene("RingGame2");
        }
        else{
            SceneManager.LoadScene("RingGame");
        }
    }
}
