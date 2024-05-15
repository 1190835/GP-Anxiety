using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenCtrl : MonoBehaviour
{
    //Esta funcao devia estar attached ao end screen UI e procurar pelo texto em children. Em vez disso esta attached a child 
    //que tem o texto. Porque e mais facil e tou com pressa. :)
    void Start()
    {
        float time = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().gameTime;
        string timeStr = TimeSpan.FromSeconds((double)time).ToString(@"m\:ss");
        this.gameObject.GetComponent<Text>().text= $"Parabéns! Fugiste do edifício em "+timeStr+".";
    }
    public void EndGame(){
        Debug.Log("game closing");
        Application.Quit();
    }
}
