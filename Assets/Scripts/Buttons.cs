using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Class for adding name and price of grocery on cashout screen when item is scanned
public class CanvasForCashOut : MonoBehaviour
{
    public Transform buttonPrefab;
    public Transform table;

    public List<string> sentence1string = new List<string> () { "Jeg", "heter", "Nissanth"};
    public AudioClip sentence1word1audio, sentence1word2audio, sentence1word3audio;
    public List<AudioClip> sentence1audioclips;
    public List<ListOfString> sentence1; 
    
    float moveX = 0;

    public void start(){
        sentence1audioclips= new List<AudioClip> () { 
            sentence1word1audio, 
            sentence1word2audio, 
            sentence1word3audio
        };

        sentence1 = new List<ListOfString> () { 
            new ListOfString(sentence1string[0], sentence1audioclips[0]),
            new ListOfString(sentence1string[1], sentence1audioclips[1]),
            new ListOfString(sentence1string[2], sentence1audioclips[2]),
            };

        for (int i=0; i<sentence1.Count; i++){
            createButtonForWord(sentence1[i]);
        }
    }

    //Method for adding prefab with name and price and positioning under last scanned item
    //Adding price of item to total price
    public void createButtonForWord(ListOfString listOfString)
    {
        //string nameOfObject = scannedObject.name.Substring(0, scannedObject.name.IndexOf(" "));
        //ItemStruct parentStruct = scannedObject.GetComponent<ItemStruct>();
        //string name = parentStruct.name;


        Transform dynamicText = (Transform)Instantiate(buttonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        dynamicText.transform.SetParent(table, false);
        dynamicText.transform.position += new Vector3(moveX, 0, 0);
        dynamicText.gameObject.name = listOfString.word; //nameOfObject

        dynamicText.transform.Find("word").gameObject.GetComponent<TextMeshPro>().text = listOfString.word;

        moveX -= 0.035f;
    }
}









