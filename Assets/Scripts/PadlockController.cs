using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PadlockController : MonoBehaviour
{
    public GameObject[] rounds;

    //Numero de slots q pararam de rodar
    public int stoppedIdx =0;
    float speed = 0.01f;
    private float [] faceRotations = {108, 144, 180, 216, 252, -72, -36, 0, 36, 72};
    private int[] combination = {1,2,3,4,5};
    public int[] input = new int[5];

    //36

    void Update(){
        //De cima para baixo, faz rodar todos os slots q ainda nao pararam (4->0)
        for(int i=4; i>=stoppedIdx;i--){
            rounds[i].transform.rotation = Quaternion.Lerp(rounds[i].transform.rotation, rounds[i].transform.rotation * Quaternion.Euler(90, 0, 0), speed);
            
        }
        

        if(Input.GetKeyDown(KeyCode.Mouse0) && stoppedIdx<=4){
            alignRound(stoppedIdx);
            stoppedIdx++;
        }
        if(stoppedIdx>4){
            checkCode();
        }
    }

    private void alignRound(int i){
            //Se um slot esta parado arredonda a rotation para o multiplo mais proximo de 36, para que uma face esteja sempre virada para
            // a camara 
            Vector3 rotate = rounds[i].transform.rotation.eulerAngles;
            rotate = new Vector3((float)(Math.Round(rotate.x / 36) * 36), rotate.y, rotate.z);
            rounds[i].transform.rotation = Quaternion.Euler(rotate);
    }

    private bool checkCode(){
        for(int i =0;i<5;i++){
            input[i] = Array.IndexOf(faceRotations,(float)Math.Round(rounds[i].transform.localRotation.x));
            Debug.Log((float)Math.Round(rounds[i].transform.localRotation.x*360));
        }
        foreach(int i in input){
            Debug.Log(i.ToString());
        }
        stoppedIdx=0;
        return input.Equals(combination);
    }
}
