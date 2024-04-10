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
        if (other.tag == "hand")
        {
            audioSource.Play();
            Destroy(this);
        }
    }
}
