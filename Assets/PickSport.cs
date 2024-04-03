using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSport : MonoBehaviour
{
    public SportTask st;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == st.currentSport.tag)
        {
            st.pickedCorrect();
        } //else audioController.playSound(tryAgain);
    }
}
