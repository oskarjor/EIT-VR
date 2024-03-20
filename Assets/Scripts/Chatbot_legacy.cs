using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.UI;

namespace Scripts {

public class ChatbotLegacy : MonoBehaviour
{
    [SerializeField] private Button recordButton;
    [SerializeField] private Image progressBar;
    [SerializeField] private Text message;
    
    private readonly string fileName = "output.wav";
    private readonly int duration = 8;
    
    private AudioClip clip;
    private bool isRecording;
    private float time;
    private OpenAIApi openai = new OpenAIApi("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");

    private List<ChatMessage> chatMessages = new List<ChatMessage>();

    private string micDeviceName;

    public AudioSource audioSource;

    private void Start()
    {       
        message.text = "Hei, jeg er en chatbot. Trykk på knappen og snakk til meg.";

        ChatMessage chatMessage = new ChatMessage
        {
            Role = "system",
            Content = "Oppfør deg som en NPC i et spill. \n" + 
                      "Svar på spørsmål knyttet til verdenen og yrket ditt", 
        };

        chatMessages.Add(chatMessage);

        audioSource = GetComponent<AudioSource>();     
        #if UNITY_WEBGL && !UNITY_EDITOR
        micDeviceName = null;
        #else
        micDeviceName = Microphone.devices[0];
        recordButton.onClick.AddListener(StartRecording);
        #endif


        // code after if does not run ???
    }
    
    private void StartRecording()
    {
        isRecording = true;
        recordButton.enabled = false;
        
        #if !UNITY_WEBGL
        clip = Microphone.Start(micDeviceName, false, duration, 44100);
        #endif
    }
    

    private async void EndRecording()
    {
        message.text = "Transcribing...";
        
        #if !UNITY_WEBGL
        Microphone.End(null);
        #endif
        
        byte[] data = SaveWav.Save(fileName, clip);
        
        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() {Data = data, Name = "audio.wav"},
            Model = "whisper-1",
            Language = "no"
        };

        var res = await openai.CreateAudioTranscription(req);

        var userMessage = res.Text;

        message.text = "Transcribe audio: \n" + userMessage + "\n\n" + chatMessages.Count + " messages";

        ChatMessage chatMessage = new ChatMessage
        {
            Content = userMessage,
            Role = "user"
        };

        chatMessages.Add(chatMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest
        {
            Messages = chatMessages,
            Model = "gpt-3.5-turbo"
        };

        var response = await openai.CreateChatCompletion(request);
        var responseMessage = "Beklager, det kan jeg ikke svare på";

        if (response.Choices != null && response.Choices.Count > 0){
            var chatResponse = response.Choices[0].Message;
            chatMessages.Add(chatResponse);

            responseMessage = chatResponse.Content;
        }

        message.text = "Response: \n" + responseMessage;

        var TTSrequest = new CreateTextToSpeechRequest
        {
            Input = responseMessage, // The text to be read aloud
            Model = "tts-1", // Text to speech model to use
            Voice = "alloy" // Voice to use
        };
        
        var TTSresponse = await openai.CreateTextToSpeech(TTSrequest);
        
        if(TTSresponse.AudioClip) audioSource.PlayOneShot(TTSresponse.AudioClip);

        progressBar.fillAmount = 0;
        recordButton.enabled = true;
    }

    private void Update()
    {
        if (isRecording)
        {
            time += Time.deltaTime;
            progressBar.fillAmount = time / duration;
            
            if (time >= duration)
            {
                time = 0;
                isRecording = false;
                EndRecording();
            }
        }
    }
}
}
