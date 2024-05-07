using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public AudioClip doorClose1;
    public AudioClip doorClose2;
    public GameObject flashButton;
    public string username;
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
        if(roomIdx!=0){
            gameTime+=Time.deltaTime;
        }
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

    public void saveUsername(string name){
        username=name;
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
        GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerCameraRoot").transform.Find("CameraHoldRoot").transform.Find("Photo").gameObject.SetActive(true);
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

    public void saveFinalMetrics(){
        Metrics metrics = new Metrics(cameraClicks, gameTime, firstStageTime, ringTime1, ringTime2, padlockTime1, padlockTime2, ringFails1, ringFails2, padlockFails1, padlockFails2);
        string json = JsonUtility.ToJson(metrics);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/MetricsData.json", json);
    }
}

[System.Serializable]
internal class Metrics
{
    public int cameraClicks;
    public float gameTime;
    public float firstStageTime;
    public float ringTime1;
    public float ringTime2;
    public float padlockTime1;
    public float padlockTime2;
    public int ringFails1;
    public int ringFails2;
    public int padlockFails1;
    public int padlockFails2;

    public Metrics(int cameraClicks, float gameTime, float firstStageTime, float ringTime1, float ringTime2, float padlockTime1, float padlockTime2, int ringFails1, int ringFails2, int padlockFails1, int padlockFails2)
    {
        this.cameraClicks = cameraClicks;
        this.gameTime = gameTime;
        this.firstStageTime = firstStageTime;
        this.ringTime1 = ringTime1;
        this.ringTime2 = ringTime2;
        this.padlockTime1 = padlockTime1;
        this.padlockTime2 = padlockTime2;
        this.ringFails1 = ringFails1;
        this.ringFails2 = ringFails2;
        this.padlockFails1 = padlockFails1;
        this.padlockFails2 = padlockFails2;
    }
}