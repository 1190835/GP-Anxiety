using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AnxietySFXController : MonoBehaviour
{
    //som de batimento cardiaco. e tratado de forma separada dos outros efeitos sonoros pq toca sempre em loop
    public AudioClip heartbeatAudio;
    //conjunto de diversos efeitos sonoros que tocam de 30 em 30 segundos, aleatoriamente
    public AudioClip[] varietyAudio;
    //timer para determinar quando reproduzir o som do batimento cardiaco outra vez
    private float heartbeatTimer;
    //tempo entre efeitos sonoros aleatorios
    private float sfxCooldown = 10f;
    //timer para determinar quando reproduzir os efeitos sonoros aleatorios
    private float sfxTimer;
    private AudioSource _audioSource;

    void Start(){
        if(!GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().hasCamera){
            this.enabled=false;
        }
        heartbeatTimer=0;
        sfxTimer=0;
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        //decrementar os timers
        sfxTimer-=Time.deltaTime;
        heartbeatTimer-=Time.deltaTime;
        //quando os timers chegam a 0, o comportamento que eles gerem e ativado e o timer e reposto
        if(heartbeatTimer<0){
            _audioSource.PlayOneShot(heartbeatAudio);
            heartbeatTimer=heartbeatAudio.length;
        }
        if(sfxTimer < 0){
            int i = varietyAudio.Length;
            Random rnd = new Random();
            int idx = rnd.Next(i-1);
            _audioSource.PlayOneShot(varietyAudio[idx]);
            sfxTimer=sfxCooldown;
        }
    }
}
