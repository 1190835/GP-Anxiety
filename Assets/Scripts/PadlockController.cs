using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class PadlockController : MonoBehaviour
{
    public GameObject[] rounds;

    //Numero de slots q pararam de rodar
    public int stoppedIdx =0;
    float speed = 1.5f;
    private float [] faceRotations = {-72,  -36, 0, 36, 72, 108, 144, -180, -144, -108};
    private int[] combination = {1,2,3,4,5};
    public int[] input = new int[5];
    public bool gameEnd = false;
    public bool pressed = false;
    public GameObject camera;
    public Animator anim;
    public AudioClip doorOpenSFX;
    public AudioClip padlockUnlockSFX;
    public AudioSource audioSource;
    public AudioSource padlockAudioSource;
    public int failCounter=0;
    public float timer=0f;

    void Update(){
        //De cima para baixo, faz rodar todos os slots q ainda nao pararam (4->0)
        for(int i=4; i>=stoppedIdx;i--){
            rounds[i].transform.rotation = Quaternion.Lerp(rounds[i].transform.rotation, rounds[i].transform.rotation * Quaternion.Euler(90, 0, 0), speed*Time.deltaTime);
            
        }
        if(Input.GetTouch(0).phase==TouchPhase.Began && stoppedIdx<=4){
            alignRound(stoppedIdx);
            stoppedIdx++;
        }
        if(stoppedIdx>4){
            checkCode();
        }
        timer+=Time.deltaTime;
    }



    private void alignRound(int i){
            //Se um slot esta parado arredonda a rotation para o multiplo mais proximo de 36, para que uma face esteja sempre virada para
            // a camara 
            Vector3 rotate = rounds[i].transform.rotation.eulerAngles;
            rotate = new Vector3((float)(Math.Round(rotate.x / 36) * 36), rotate.y, rotate.z);
            rounds[i].transform.rotation = Quaternion.Euler(rotate);
            float angle = getRelativeAngle(rounds[i].transform.forward,camera.transform.forward);
            angle = (float)Math.Round(angle);
            input[i]=Array.IndexOf(faceRotations,angle);
            Debug.Log("Angle: "+angle+", Number: "+input[i]);
    }

    private float getRelativeAngle(Vector3 va, Vector3 vb){
        float angle = Vector3.Angle(va,vb);
        Vector3 cross = Vector3.Cross(va,vb);
        if(cross.x<0){
            angle = -angle;
        }
        return angle;
    }

    private void checkCode(){
        if(!Enumerable.SequenceEqual(input, combination)){
            Debug.Log("Correct code");
            anim.SetBool("Open",true);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().savePadlockMetrics(failCounter,timer);
            this.enabled=false;
            padlockAudioSource.PlayOneShot(padlockUnlockSFX);
            audioSource.PlayOneShot(doorOpenSFX);
            Invoke("ChangeScene",doorOpenSFX.length);
        }
        else{
            stoppedIdx=0;
            for(int i =0; i<input.Length;i++){
                input[i]=-1;
            }
            failCounter++;
        }
    }
    private void ChangeScene(){
        Debug.Log("changing scenes");
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().hasCamera){
            SceneManager.LoadScene("SideHallwayOtherworld");
        }
        SceneManager.LoadScene("Room");
        //GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().showUI();
    }

    public void VirtualPressInput(bool virtualPressed){
        pressed=virtualPressed;
    }
}
