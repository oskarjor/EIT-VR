using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportTask : MonoBehaviour
{
    public GameObject basketball, football, tennis, volleyball;

    public AudioClip taskDescription, pickFootball, pickBasketball, pickTennis, pickVolleyball, tryAgain;

    public AudioController audioController;

    public List<SportActivity> sports;

    public SportActivity currentSport;

    public Status statusScript;

    void Start()
    {
        //audioController.playSound(taskDescription);
        sports = new List<SportActivity>() {
            new SportActivity(pickBasketball, basketball, "basketball"), 
            new SportActivity(pickFootball, football, "football"), 
            new SportActivity(pickTennis, tennis, "tennis"), 
            new SportActivity(pickVolleyball, volleyball, "volleyball")
        };

        currentSport = sports[Random.Range(0,sports.Count-1)];
        //currentSport = sports[1];
        audioController.playSound(currentSport.nameClip);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == currentSport.tag)
        {
            currentSport.sport.GetComponent<Outline>().enabled = true;
            sports.Remove(currentSport);
            if(sports.Count == 0){
                statusScript.task1 = true;
            } else {
                currentSport = sports[Random.Range(0,sports.Count-1)];
                audioController.playSound(currentSport.nameClip);
            }
        } //else audioController.playSound(tryAgain);
    }
}
