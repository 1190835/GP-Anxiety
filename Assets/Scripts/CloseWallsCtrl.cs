using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseWallsCtrl : MonoBehaviour
{
    private GameManager manager;
    void Start(){
        manager=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.anxTimer<=20f && GetComponent<Animator>().GetBool("closing")==false){
            GetComponent<Animator>().SetBool("closing",true);
        }
    }
}
