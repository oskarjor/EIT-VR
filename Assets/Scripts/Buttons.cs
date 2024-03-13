using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Class for adding name and price of grocery on cashout screen when item is scanned
public class Buttons : MonoBehaviour
{
    public Transform buttonPrefab;
    public Transform table;
    public Transform boxPrefab;

    public List<string> sentence1string;
    public AudioClip sentence1word1audio, sentence1word2audio, sentence1word3audio, sentence1word4audio, sentence1word5audio, sentence1word6audio;
    
    public List<AudioClip> sentence1audioclips;
    public List<ListOfString> sentence1;
    public List<GameObject> gameObjectsForSentence;
    public int correctPositionedWords = 0;
    
    float moveX = 0;
    int count = 0;

    void Start(){
        
        sentence1audioclips= new List<AudioClip> () { 
            sentence1word1audio, 
            sentence1word2audio, 
            sentence1word3audio,
            sentence1word4audio,
            sentence1word5audio,
            sentence1word6audio
        };

        sentence1string = new List<string>() { "Hvordan", "går", "det", "med", "barnet", "mitt?" };

        sentence1 = new List<ListOfString> () { 
            new ListOfString(sentence1string[0], sentence1audioclips[0]),
            new ListOfString(sentence1string[1], sentence1audioclips[1]),
            new ListOfString(sentence1string[2], sentence1audioclips[2]),
            new ListOfString(sentence1string[3], sentence1audioclips[3]),
            new ListOfString(sentence1string[4], sentence1audioclips[4]),
            new ListOfString(sentence1string[5], sentence1audioclips[5])
            };

        for (int i= sentence1.Count-1; i>-1; i--){
            ListOfString current = sentence1[Random.Range(0, sentence1.Count-1)];
            createButtonForWord(current);
            sentence1.Remove(current);
        }
    }

    public void createButtonForWord(ListOfString listOfString)
    {
        Transform dynamicText = (Transform)Instantiate(buttonPrefab, new Vector3(buttonPrefab.position.x, buttonPrefab.position.y, buttonPrefab.position.z), Quaternion.Euler(90, 0, 0));
        dynamicText.transform.SetParent(table, false);
        dynamicText.transform.position += new Vector3(moveX, 0, 0);
        dynamicText.gameObject.name = listOfString.word;
        dynamicText.gameObject.GetComponent<AudioSource>().clip = listOfString.wordPronunciation;
        dynamicText.gameObject.GetComponent<ButtonInBox>().buttonsScript = GetComponent<Buttons>();
        dynamicText.transform.Find("Canvas").Find("word").gameObject.GetComponent<TextMeshProUGUI>().text = listOfString.word;

        Transform boxGameObject = (Transform)Instantiate(boxPrefab, new Vector3(boxPrefab.position.x, boxPrefab.position.y, boxPrefab.position.z), Quaternion.identity);
        boxGameObject.transform.SetParent(table, false);
        boxGameObject.transform.position += new Vector3(moveX, 0, 0);
        boxGameObject.gameObject.name = sentence1string[count];

        gameObjectsForSentence.Add(boxGameObject.gameObject);
        gameObjectsForSentence.Add(dynamicText.gameObject);

        moveX += 1f;
        count++;
    }

    public void CheckIfTaskIsDone()
    {
        correctPositionedWords++;
        if(correctPositionedWords == sentence1string.Count)
        {
            StartCoroutine(destroyObjects());
            //set task as complete in status script
        }
    }

    IEnumerator destroyObjects()
    {
        yield return new WaitForSeconds(4);
        foreach (GameObject go in gameObjectsForSentence) Destroy(go);
    }
}









