using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    public AudioSource audioSource;

    public AudioClip mouseLeftClick;

    public AudioClip Maintheme1;
    public AudioClip Maintheme2;

    public AudioClip ShotgunFire;


    void Start()
    {
        
    }



    public void PlayAudioOneTime(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

   public void PlayAudioLoop(AudioClip audioClip)
    {   
        audioSource.PlayOneShot(audioClip);
    }




    void Update()
      {         
       if (Input.GetMouseButtonDown(0)) 
         {
           audioSource.PlayOneShot(mouseLeftClick);  
         }
      }
    
}
