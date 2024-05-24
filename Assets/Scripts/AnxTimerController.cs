using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnxTimerController : MonoBehaviour
{
    public float time;
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().hasCamera){
            time = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().anxTimer;
            string str = TimeSpan.FromSeconds((double)time).ToString(@"m\:ss");
            if(time<0){
                str = "-"+str;
                this.gameObject.GetComponent<Text>().color = Color.red;
                Invoke("EndGame",1f);
            }
            this.gameObject.GetComponent<Text>().text = str;
        }
    }
    private void EndGame(){
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().saveFinalMetrics();
        SceneManager.LoadScene("Hell");
    }
}
