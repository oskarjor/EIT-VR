using UnityEngine;

public class CharacterInfo : MonoBehaviour

{
    [SerializeField, TextArea] private string characterJob;
    [SerializeField, TextArea] private string characterName;
    [SerializeField, TextArea] private string characterDescription;

    // Update is called once per frame
    public string GetCharacterInfo()
    {
        return $"Navn: {characterName}\n" +
               $"Jobb: {characterJob}\n" +
               $"Beskrivelse: {characterDescription}\n";
    }

    public string GetCharacterName()
    {
        return characterName;
    }
}
