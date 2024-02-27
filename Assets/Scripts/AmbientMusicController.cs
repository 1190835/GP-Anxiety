using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusicController : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip[] bgAudio;
    private int roomId;

    private Dictionary<int, int> roomBgmMap = new Dictionary<int, int>{
        {0,0},
        {1,0},
        {2,0},
        {3,0},
        {4,1}
    };
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void updateBackgroundAudio(int idx){
        if(_audioSource.clip!=bgAudio[roomBgmMap[idx]]){
            StopMusic();
            _audioSource.clip=bgAudio[roomBgmMap[idx]];
            PlayMusic();
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
