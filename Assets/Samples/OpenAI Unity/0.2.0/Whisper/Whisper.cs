using OpenAI;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Samples.Whisper
{
    public class Whisper : MonoBehaviour
    {
        [SerializeField] private Button recordButton;
        [SerializeField] private Image progressBar;
        [SerializeField] private Text message;
        [SerializeField] private Dropdown dropdown;
        
        private readonly string fileName = "output.wav";
        private readonly int duration = 8;
        
        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> chatMessages = new List<ChatMessage>();

        public AudioSource audioSource;

        private void Start()
        {       
            audioSource = GetComponent<AudioSource>();     
            #if UNITY_WEBGL && !UNITY_EDITOR
            dropdown.options.Add(new Dropdown.OptionData("Microphone not supported on WebGL"));
            #else
            foreach (var device in Microphone.devices)
            {
                dropdown.options.Add(new Dropdown.OptionData(device));
            }
            recordButton.onClick.AddListener(StartRecording);
            dropdown.onValueChanged.AddListener(ChangeMicrophone);
            
            var index = PlayerPrefs.GetInt("user-mic-device-index");
            dropdown.SetValueWithoutNotify(index);
            #endif

            ChatMessage chatMessage = new ChatMessage
            {
                Content = "Du er en hjelpsom samtalepartner! Bruk enkelt språk og vær tålmodig.",
                Role = "system"
            };
        }

        private void ChangeMicrophone(int index)
        {
            PlayerPrefs.SetInt("user-mic-device-index", index);
        }
        
        private void StartRecording()
        {
            isRecording = true;
            recordButton.enabled = false;

            var index = PlayerPrefs.GetInt("user-mic-device-index");
            
            #if !UNITY_WEBGL
            clip = Microphone.Start(dropdown.options[index].text, false, duration, 44100);
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

            message.text = "Transcribe audio: \n" + userMessage;

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

                // Debug.Log(chatResponse.Content);

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
