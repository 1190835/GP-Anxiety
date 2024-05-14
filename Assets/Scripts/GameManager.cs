using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Rendering;
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
    public float anxTimer;
    public int[] padlockCode1 = new int[5];
    public int [] padlockCode2 = new int[5];
    [Header("Metrics")]
    public string username;
    public string gameStart;
    public string gameEnd;
    public float gameTime=0f;
    public string firstStageStart;
    public string firstStageEnd;
    public float firstStageTime;
    public string secondStageStart;
    public string secondStageEnd;
    public float secondStageTime;
    public int cameraClicks =0;
    public float ringTime1;
    public float ringTime2;
    public float padlockTime1;
    public float padlockTime2;
    public int ringFails1;
    public int ringFails2;
    public int padlockFails1;
    public int padlockFails2;
    
    private void Awake(){
        DontDestroyOnLoad(transform.gameObject);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 100;
        GameObject.FindGameObjectWithTag("Music").GetComponent<AmbientMusicController>().updateBackgroundAudio(roomIdx);
        GeneratePadlockCodes();
        gameStart=DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
    }
    void Start(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Update(){
        if(roomIdx!=0){
            gameTime+=Time.deltaTime;
        }
        if(hasCamera && anxTimer>0){
            anxTimer-=Time.deltaTime;
            secondStageTime+=Time.deltaTime;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        roomIdx=SceneManager.GetActiveScene().buildIndex;
        GameObject.FindGameObjectWithTag("Music").GetComponent<AmbientMusicController>().updateBackgroundAudio(roomIdx);
        AudioSource src; 
        GameObject.FindGameObjectWithTag("SFX").TryGetComponent<AudioSource>(out src);

        if(src!=null){
            if(roomIdx==3 || roomIdx==7){
                src.PlayOneShot(doorClose1);
            }
            if(roomIdx==5 || roomIdx==6){
                src.PlayOneShot(doorClose2);
            }
        }
    }

    public void saveUsername(string name){
        username=name;
        //tive preguica de criar uma funcao nova, surely isto n vai criar problemas :)
        firstStageStart=DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
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
        anxTimer=90f;
    }
    public void saveFirstStageTime(){
        firstStageTime=gameTime;
        firstStageEnd=DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        secondStageStart=firstStageEnd;
        secondStageTime=0f;
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
        gameEnd=DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        secondStageEnd=gameEnd;

        Metrics metrics = new Metrics(username,gameStart,gameEnd,gameTime,firstStageStart,firstStageEnd,firstStageTime,secondStageStart,secondStageEnd,secondStageTime,cameraClicks,ringTime1,ringTime2,padlockTime1,padlockTime2,ringFails1,ringFails2,padlockFails1,padlockFails2);

        if(File.Exists(Application.persistentDataPath + "/OtherworldMetricsData.json")){
            string currentJson = System.IO.File.ReadAllText(Application.persistentDataPath + "/OtherworldMetricsData.json");
            Debug.Log(currentJson);
            MetricsList metricsList = JsonUtility.FromJson<MetricsList>(currentJson);
            metricsList._list.Add(metrics);
            string json = JsonUtility.ToJson(metricsList);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/OtherworldMetricsData.json", json);
        }
        else{
            MetricsList metricsList = new MetricsList();
            metricsList._list.Add(metrics);
            string json = JsonUtility.ToJson(metricsList);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/OtherworldMetricsData.json", json);
        }
    }
}

[System.Serializable]
internal class Metrics
{
    public string username;
    public string gameStart;
    public string gameEnd;
    public float gameTime;
    public string firstStageStart;
    public string firstStageEnd;
    public float firstStageTime;
    public string secondStageStart;
    public string secondStageEnd;
    public float secondStageTime;
    public int cameraClicks;
    public float ringTime1;
    public float ringTime2;
    public float padlockTime1;
    public float padlockTime2;
    public int ringFails1;
    public int ringFails2;
    public int padlockFails1;
    public int padlockFails2;

    public Metrics(string username, string gameStart, string gameEnd, float gameTime, string firstStageStart, string firstStageEnd, float firstStageTime, string secondStageStart, string secondStageEnd, float secondStageTime, int cameraClicks, float ringTime1, float ringTime2, float padlockTime1, float padlockTime2, int ringFails1, int ringFails2, int padlockFails1, int padlockFails2)
    {
        this.username = username;
        this.gameStart = gameStart;
        this.gameEnd = gameEnd;
        this.gameTime = gameTime;
        this.firstStageStart = firstStageStart;
        this.firstStageEnd = firstStageEnd;
        this.firstStageTime = firstStageTime;
        this.secondStageStart = secondStageStart;
        this.secondStageEnd = secondStageEnd;
        this.secondStageTime = secondStageTime;
        this.cameraClicks = cameraClicks;
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
[System.Serializable]
internal class MetricsList{
    public List<Metrics> _list;

    public MetricsList(){
        _list = new List<Metrics>();
    }
}