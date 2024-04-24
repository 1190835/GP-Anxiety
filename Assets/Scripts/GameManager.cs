using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public AudioClip doorClose1;
    public AudioClip doorClose2;
    public GameObject flashButton;
    public int roomIdx;
    public bool hasKey = false;
    public bool hasCamera = false;
    [Header("Metrics")]
    public int cameraClicks =0;
    public float gameTime=0f;
    public float firstStageTime;
    public float ringTime1;
    public float ringTime2;
    public float padlockTime1;
    public float padlockTime2;
    public int ringFails1;
    public int ringFails2;
    public int padlockFails1;
    public int padlockFails2;
    public int[] padlockCode1 = new int[5];
    public int [] padlockCode2 = new int[5];
    private void Awake(){
        DontDestroyOnLoad(transform.gameObject);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 100;
        GameObject.FindGameObjectWithTag("Music").GetComponent<AmbientMusicController>().updateBackgroundAudio(roomIdx);
        GeneratePadlockCodes();
    }
    void Start(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Update(){
        gameTime+=Time.deltaTime;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        roomIdx=SceneManager.GetActiveScene().buildIndex;
        GameObject.FindGameObjectWithTag("Music").GetComponent<AmbientMusicController>().updateBackgroundAudio(roomIdx);
        AudioSource src; 
        GameObject.FindGameObjectWithTag("SFX").TryGetComponent<AudioSource>(out src);

        if(src!=null){
            if(roomIdx==3){
                src.PlayOneShot(doorClose1);
            }
            if(roomIdx==5){
                src.PlayOneShot(doorClose2);
            }
        }
    }

    private void GeneratePadlockCodes(){
        Random rnd = new Random();
        for(int i=0;i<padlockCode1.Length;i++){
            padlockCode1[i]=rnd.Next(9);
            padlockCode2[i]=rnd.Next(9);
        }
    }

    public void unlockCamera(){
        hasKey=true;
        hasCamera=true;
        GameObject.FindGameObjectWithTag("Player").transform.Find("CameraHoldRoot").transform.Find("Photo").gameObject.SetActive(true);
    }
    public void saveFirstStageTime(){
        firstStageTime=gameTime;
    }
    public void saveRingMetrics(int fails, float time){
        if(hasCamera){
            ringFails2=fails;
            ringTime2=time;
        }
        else{
            ringFails1=fails;
            ringTime1=time;
        }
    }
    public void savePadlockMetrics(int fails, float time){
        if(hasCamera){
            padlockFails2=fails;
            padlockTime2=time;
        }
        else{
            padlockFails1=fails;
            padlockTime1=time;
        }
    }
    public void incrementFlashCount(){
        cameraClicks++;
    }
    public void hideUI(){
        GameObject.FindGameObjectWithTag("UI").SetActive(false);
    }
    public void showUI(){
        GameObject.FindGameObjectWithTag("UI").SetActive(true);
    }
}
