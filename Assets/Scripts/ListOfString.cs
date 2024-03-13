using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ListOfString {

    public string word; 
    public AudioClip wordPronunciation; 

    public ListOfString(string word, AudioClip wordPronunciation){
        this.word = word; 
        this.wordPronunciation = wordPronunciation;
    }
}
