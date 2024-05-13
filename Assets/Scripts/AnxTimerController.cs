using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxTimerController : MonoBehaviour
{
    private float time;
    // Update is called once per frame
    void Update()
    {
        time = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().anxTimer;
        string str = TimeSpan.FromSeconds((double)time).ToString(@"m\:ss");
        this.gameObject.GetComponent<Text>().text = str;
    }
}
