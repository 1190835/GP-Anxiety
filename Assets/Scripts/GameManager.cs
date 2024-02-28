using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip doorClose1;
    public AudioClip doorClose2;
    public int roomIdx;
    public bool hasKey = false;
    public bool hasCamera = false;
    private void Awake(){
        DontDestroyOnLoad(transform.gameObject);
        GameObject.FindGameObjectWithTag("Music").GetComponent<AmbientMusicController>().updateBackgroundAudio(roomIdx);
    }
    void Start(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        roomIdx=SceneManager.GetActiveScene().buildIndex;
        GameObject.FindGameObjectWithTag("Music").GetComponent<AmbientMusicController>().updateBackgroundAudio(roomIdx);
        AudioSource src; 
        GameObject.FindGameObjectWithTag("SFX").TryGetComponent<AudioSource>(out src);

        if(src!=null){
            if(roomIdx==2){
                src.PlayOneShot(doorClose1);
            }
            if(roomIdx==4){
                src.PlayOneShot(doorClose2);
            }
        }
    }

    public void unlockCamera(){
        hasKey=true;
        hasCamera=true;
        GameObject.FindGameObjectWithTag("Player").transform.Find("CameraHoldRoot").transform.Find("Photo").gameObject.SetActive(true);
    }
}
