using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetNamePlate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text namePlate;
    [SerializeField] private CharacterInfo characterInfo;
    void Start()
    {
        namePlate.text = characterInfo.GetCharacterName();
    }
}
