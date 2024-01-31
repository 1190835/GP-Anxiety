using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPuzzleTriggerController : MonoBehaviour, IInteractible
{
    public GameObject puzzleCanvas;
    public void Interact(){
        puzzleCanvas.SetActive(true);
    }
}
