using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPickupController : MonoBehaviour, IInteractible
{
    public void Interact(){
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().unlockCamera();
        this.gameObject.SetActive(false);
    }
}
