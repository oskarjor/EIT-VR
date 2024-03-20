using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playSound(AudioClip audioClip){
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
