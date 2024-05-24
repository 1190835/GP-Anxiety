using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PadlockTutorial : MonoBehaviour
{
    public GameObject[] rounds;

    //Numero de slots q pararam de rodar
    public int stoppedIdx =0;
    float speed = 1.5f;
    //Este array guarda as rotations dos cilindros que correspondem a um certo digito estar virado para a camera
    //Por ordem numerica 0->9
    //Isto e essencial para saber que combinacao o jogador introduziu. Tende a deixar de funcionar randomly. N sei pq. Hj funciona
    //private float [] faceRotations = {-72,  -36, 0, 36, 72, 108, 144, -180, -144, -108};
    //este puzzle deixou de funcionar out of nowhere, dps de dar debug descubri q as rotations mudaram para negativo. n sei pq. might acontecer outra vez
    private float [] faceRotations = {72, 36, 0, -36, -72, -108, -144, 180, 144, 108};
    private int[] combination = {2,8,5,4,5};
    public int[] input = new int[5];
    //public bool gameEnd = false;
    public bool pressed = false;
    public GameObject camera;

    //Padlock open animation
    public Animator anim;
    public AudioClip padlockUnlockSFX;
    public AudioClip failSFX;
    public AudioSource audioSource;
    public AudioSource padlockAudioSource;

    //No caso do jogador estar a entrar no puzzle pela segunda vez, retomar o progresso salvo anteriormente
    void Start(){
        
    }

    void Update(){
        //De cima para baixo, faz rodar todos os slots q ainda nao pararam (4->0)
        for(int i=4; i>=stoppedIdx;i--){
            rounds[i].transform.rotation = Quaternion.Lerp(rounds[i].transform.rotation, rounds[i].transform.rotation * Quaternion.Euler(90, 0, 0), speed*Time.deltaTime);
            
        }
        if(Input.touchCount>0){
            Touch touch = Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began && stoppedIdx<=4){
                alignRound(stoppedIdx);
                stoppedIdx++;
            }
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
            float angle = getRelativeAngle(rounds[i].transform.forward,camera.transform.forward);
            angle = (float)Math.Round(angle);
            input[i]=Array.IndexOf(faceRotations,angle);
            Debug.Log("Angle: "+angle+", Number: "+input[i]);
    }

    //Traduz o angulo in-engine do cilindro para o angulo relativo ao vetor forward da camera
    //Porque o unity da handle dos angulos de rotation de uma forma que trabalha contra o nosso objetivo
    //Assim obtemos a informacao que queremos sempre (exceto quando nao funciona)
    //Se o padlock puzzle nao estiver a funcionar o problema e 99% aqui. Dar debug do transform.rotation de um cilindro
    private float getRelativeAngle(Vector3 va, Vector3 vb){
        float angle = Vector3.Angle(va,vb);
        Vector3 cross = Vector3.Cross(va,vb);
        if(cross.x<0){
            angle = -angle;
        }
        return angle;
    }

    private void checkCode(){
        if(Enumerable.SequenceEqual(input, combination)){
            Debug.Log("Correct code");
            anim.SetBool("Open",true);
            padlockAudioSource.PlayOneShot(padlockUnlockSFX);
            Invoke("reset",1);
        }
        else{
            stoppedIdx=0;
            for(int i =0; i<input.Length;i++){
                input[i]=-1;
            }
            audioSource.PlayOneShot(failSFX);
        }
    }

    private void reset(){
        anim.SetBool("Open", false);
        stoppedIdx=0;
        for(int i =0; i<input.Length;i++){
            input[i]=-1;
        }
    }

    public void VirtualPressInput(bool virtualPressed){
        pressed=virtualPressed;
    }
    public void returnToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}