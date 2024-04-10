using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonInBox : MonoBehaviour
{
    public Material greenMaterial;
    public Material redMaterial;
    public Buttons buttonsScript;
    public List<GameObject> correctPlacedBoxes;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "box")
        {
            if(!correctPlacedBoxes.Contains(other.gameObject)){
                if (name == other.gameObject.name)
                {
                    other.gameObject.GetComponent<MeshRenderer>().material = greenMaterial;
                    buttonsScript.CheckIfTaskIsDone();
                    correctPlacedBoxes.Add(other.gameObject);
                    Destroy(GetComponent<XRGrabInteractable>());
                }
                else other.gameObject.GetComponent<MeshRenderer>().material = redMaterial;
            }
            
        }
        
    }
}