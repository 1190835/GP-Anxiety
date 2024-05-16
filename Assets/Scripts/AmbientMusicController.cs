using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusicController : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip[] bgAudio;
    //Build idx no Unity, identifica a sala atual
    private int roomId;

    //Este dictionary mapeia o indice da Scene ao indice do som de background que deve ser reproduzido nessa sala
    //Essencialmente para evitar escrever um switch case que faria o mesmo
    private Dictionary<int, int> roomBgmMap = new Dictionary<int, int>{
        {1,0},
        {2,0},
        {3,0},
        {4,0},
        {5,1},
        {6,1},
        {7,1},
        {8,1},
        {9,0},
        {10,1},
        {11,1}
    };
    private void Awake()
    {
        //Dont destroy para permitir que o som de fundo transicione entre cenas diferentes de forma continua
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    //Altera o audio clip apenas quando a musica da sala atual muda. Nao substitui a musica 1 pela musica 1, por exemplo.
    //Para transicionar de forma continua
    public void updateBackgroundAudio(int idx){
        if(idx!=0){
            if(_audioSource.clip!=bgAudio[roomBgmMap[idx]]){
                StopMusic();
                _audioSource.clip=bgAudio[roomBgmMap[idx]];
                PlayMusic();
            }
        }
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
