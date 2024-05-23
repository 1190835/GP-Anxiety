using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCtrl : MonoBehaviour
{
    void Start()
    {
        float time = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().gameTime;
        string timeStr = TimeSpan.FromSeconds((double)time).ToString(@"m\:ss");
        this.gameObject.GetComponent<Text>().text= $"GAME\nOVER\n"+timeStr;
    }
    public void EndGame(){
        Debug.Log("game closing");
        Application.Quit();
    }
}
