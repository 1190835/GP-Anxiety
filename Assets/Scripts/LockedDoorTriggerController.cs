using UnityEngine;
using UnityEngine.SceneManagement;

public class LockedDoorTriggerController : MonoBehaviour, IInteractible
{
    public AudioClip doorLockedSFX;
    public AudioSource audioSource;
    public void Interact(){
        audioSource.PlayOneShot(doorLockedSFX);
    }
}