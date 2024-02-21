using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SportActivity
{
    public AudioClip nameClip;
    public GameObject sport;

    public string tag;
   
    public SportActivity(AudioClip nameClip, GameObject sport, string tag) {
        this.nameClip = nameClip;
        this.sport = sport;
        this.tag = tag;
    }
}
