using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadlockController : MonoBehaviour
{
    public GameObject[] rounds;

    //Numero de slots q pararam de rodar
    public int stoppedIdx =0;
    float speed = 0.01f;
    private float[] faceRotations = {72, 36, 0, 324, 288, 252, 216, 180, 144, 108};
    private int[] combination = {1,2,3,4,5};
    public int[] input = new int[5];

    //36

    void Update(){
        //De cima para baixo, faz rodar todos os slots q ainda nao pararam (4->0)
        for(int i=4; i>=stoppedIdx;i--){
            rounds[i].transform.rotation = Quaternion.Lerp(rounds[i].transform.rotation, rounds[i].transform.rotation * Quaternion.Euler(90, 0, 0), speed);
            
        }
        //Se um slot esta parado arredonda a rotation para o multiplo mais proximo de 36, para que uma face esteja sempre virada para
        // a camara 
        for(int i =0; i<stoppedIdx;i++){
            Vector3 rotate = rounds[i].transform.rotation.eulerAngles;
            rotate = new Vector3((float)(Math.Round(rotate.x / 36) * 36), rotate.y, rotate.z);
            rounds[i].transform.rotation = Quaternion.Euler(rotate);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && stoppedIdx<=4){
            stoppedIdx++;
        }
        if(stoppedIdx>4){
            checkCode();
        }
    }

    private bool checkCode(){
        for(int i =0;i<5;i++){
            input[i] = Array.IndexOf(faceRotations,(float)Math.Round(rounds[i].transform.rotation.eulerAngles.x));
            Debug.Log((float)Math.Round(rounds[i].transform.rotation.eulerAngles.x));
        }
        foreach(int i in input){
            Debug.Log(i.ToString());
        }
        stoppedIdx=0;
        return input.Equals(combination);
    }
}
