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
                buttonsScript.correctPositionedWords++;
            }
            else other.gameObject.GetComponent<MeshRenderer>().material = redMaterial;
        }
        
    }
}
