using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportTask : MonoBehaviour
{
    public GameObject basketball;
    public GameObject football;

    public string currentItem = "basketball";

    public bool finishedTask = false;

    void OnTriggerEnter(Collider other){
        if(other.tag == currentItem){
            finishedTask = true;
        }
    }
}
