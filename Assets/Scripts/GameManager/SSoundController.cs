using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;


public class SSoundController : Singleton<SSoundController>
{
    public AudioSource backgroundMusicPlayer;
    public AudioClip clip;


    public void PlayBackGroundMusic(AudioClip clip)
    {
        backgroundMusicPlayer.clip = clip;
        backgroundMusicPlayer.Play();
    }
    void Awake(){
        PlayBackGroundMusic(clip);
    }
    
}
