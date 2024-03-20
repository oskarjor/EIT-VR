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

    public PickSport psLeft;

    public PickSport psRight;

    void Start()
    {
        audioController.playSound(taskDescription);
        sports = new List<SportActivity>() {
            new SportActivity(pickBasketball, basketball, "basketball"), 
            new SportActivity(pickFootball, football, "football"), 
            new SportActivity(pickTennis, tennis, "tennis"), 
            new SportActivity(pickVolleyball, volleyball, "volleyball")
        };

        currentSport = sports[Random.Range(0,sports.Count-1)];
        audioController.playSound(currentSport.nameClip);
    }

    public void pickedCorrect(){
        currentSport.sport.GetComponent<Outline>().enabled = true;
        sports.Remove(currentSport);
        if(sports.Count == 0){
            psLeft.enabled = false;
            psRight.enabled = false;
            statusScript.StartNextTask();
            Destroy(basketball);
            Destroy(tennis);
            Destroy(football);
            Destroy(volleyball);
        } else {
            currentSport = sports[Random.Range(0,sports.Count-1)];
            audioController.playSound(currentSport.nameClip);
        }
    }
}
