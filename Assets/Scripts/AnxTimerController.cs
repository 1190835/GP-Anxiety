using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnxTimerController : MonoBehaviour
{
    public float time =20f;
    // Update is called once per frame
    void Update()
    {
        // if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().hasCamera){
        //     time = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().anxTimer;
        //     string str = TimeSpan.FromSeconds((double)time).ToString(@"m\:ss");
        //     if(time<0){
        //         str = "-"+str;
        //         this.gameObject.GetComponent<Text>().color = Color.red;
        //     }
        //     this.gameObject.GetComponent<Text>().text = str;
        // }
        if(SceneManager.GetActiveScene().buildIndex==12){
            if(time==20f){
                GameObject.Find("AnxMainHallwayOtherworld").GetComponent<Animator>().SetBool("closing",true);
            }
        }
    }
}
