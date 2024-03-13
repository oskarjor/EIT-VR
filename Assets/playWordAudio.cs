using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playWordAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;


    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hånd");
        if (other.tag == "hand")
        {
            Debug.Log("hånd inni hånd");
            audioSource.Play();
        }
    }
}
