using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for working with UI

public class SentenceAssembler : MonoBehaviour
{
    public Text sentenceText; // Assign in the Inspector
    public Button wordButton; // Assign in the Inspector
    private string partOne = "my "; // First part of the sentence
    private string partTwo = " is Tom"; // Second part of the sentence
    private string missingWord = "name"; // The word that the button represents

    void Start()
    {
        // Initialize the sentence with a gap for the missing word
        sentenceText.text = partOne + "___" + partTwo;

        // Add a click listener to the button to call the PlaceWord method when clicked
        wordButton.onClick.AddListener(PlaceWord);
    }

    void PlaceWord()
    {
        // When the button is clicked, insert the missing word into the sentence
        sentenceText.text = partOne + missingWord + partTwo;

        // Optionally, disable the button after it's placed
        wordButton.gameObject.SetActive(false);
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// //Class for adding name and price of grocery on cashout screen when item is scanned
// public class CanvasForCashOut : MonoBehaviour
// {
//     public Transform textForCashOutCanvasPrefab;
//     public Transform cashoutCanvas;
//     public TMP_Text totalPrice;

//     float moveY = 0;
//     int count = 0;

//     //Method for adding prefab with name and price and positioning under last scanned item
//     //Adding price of item to total price
//     public void setScannedItemOnCanvas(GameObject scannedObject)
//     {
//         //string nameOfObject = scannedObject.name.Substring(0, scannedObject.name.IndexOf(" "));
//         ItemStruct parentStruct = scannedObject.GetComponent<ItemStruct>();
//         int price = parentStruct.price;
//         string name = parentStruct.name;


//         Transform dynamicText = (Transform)Instantiate(textForCashOutCanvasPrefab, new Vector3(0, 0, count), Quaternion.identity);
//         dynamicText.transform.SetParent(cashoutCanvas, false);
//         dynamicText.transform.position += new Vector3(0, moveY, 0);
//         dynamicText.gameObject.name = name; //nameOfObject

//         dynamicText.transform.Find("NameOfObject").gameObject.GetComponent<Text>().text = name;
//         dynamicText.transform.Find("price").gameObject.GetComponent<Text>().text = price.ToString() + " kr"; 
//         totalPrice.text = (double.Parse(totalPrice.text) + price).ToString();

//         moveY -= 0.035f;
//         count -= 10;
//     }
// }