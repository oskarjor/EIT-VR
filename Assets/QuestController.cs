using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    // Start is called before the first frame update
    private int numTeachers;
    private int teachersMet = 0;
    [SerializeField] private TMP_Text questText;

    private GameObject[] teachers;
    void Start()
    {
        // count the number of teachers in the scene, they have the tag "Teacher"
        teachers = GameObject.FindGameObjectsWithTag("Teacher");
        numTeachers = teachers.Length;
    }

    // Update is called once per frame
    void Update()
    {
        teachersMet = 0;
        foreach (var teacher in teachers)
        {
            // get the variable hasInteractedWithPlayer from the Chatbot script
            var chatbot = teacher.GetComponent<Scripts.Chatbot>();
            if (chatbot.hasInteractedWithPlayer)
            {
                teachersMet++;
            }
        }

        if (teachersMet == numTeachers)
        {
            questText.text = "Du har møtt alle lærerene!";
        }

        questText.text = $"Du har møtt {teachersMet}/{numTeachers} lærere!";
    }
}
