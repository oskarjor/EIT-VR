using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInBox : MonoBehaviour
{
    public Material greenMaterial;
    public Material redMaterial;
    public Buttons buttonsScript;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "box")
        {
            if (name == other.gameObject.name)
            {
                other.gameObject.GetComponent<MeshRenderer>().material = greenMaterial;
                buttonsScript.CheckIfTaskIsDone();
                //tomake sure one object dont count as many correct words:
                //set position of object nicely on top of the box
                //disable grab interactable on the object
            }
            else other.gameObject.GetComponent<MeshRenderer>().material = redMaterial;
        }
        
    }
}
