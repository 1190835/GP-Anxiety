using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PadlockPuzzleTriggerController : MonoBehaviour, IInteractible
{
    public void Interact(){
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().hideUI();
        SceneManager.LoadScene("PadlockPuzzle");
    }
}
