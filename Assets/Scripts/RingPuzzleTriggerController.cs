using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RingPuzzleTriggerController : MonoBehaviour, IInteractible
{
    public void Interact(){
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().hideUI();
        SceneManager.LoadScene("RingGame");
    }
}
