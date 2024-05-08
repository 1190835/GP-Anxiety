using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDoorController : MonoBehaviour, IInteractible
{
    public void Interact(){
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if(!gameManager.hasCamera){
            gameManager.saveFinalMetrics();
            SceneManager.LoadScene("MainMenu");
        }
    }
}
