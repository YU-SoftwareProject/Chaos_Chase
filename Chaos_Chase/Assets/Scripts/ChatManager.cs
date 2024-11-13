using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;

public class ChatManager : MonoBehaviour
{
    public GameObject Chat_Panel;
    public TextMeshProUGUI chatText;
    public TextMeshProUGUI NPCName;
    //public InputField userInputField; // 대화 입력 필드 (선택 사항)
    public TMP_InputField userInputField;
    public Button[] suspectButtons;
    public Button sendButton;

    public string fileContent;

    public ChatGPTClient chatGPTClient;

    private List<ChatGPTClient.Message> messageHistory = new List<ChatGPTClient.Message>();
    //private string playerId = "Player1"; 예시: 플레이어 ID, 필요에 따라 변경

    public void StartNPC1Chat(Button clickedButton)
    {
        Chat_Panel.SetActive(true);
        NPCName.text = "Maya";
        chatText.text = "";

        // 초기 시스템 메시지 설정 (선택 사항)
        messageHistory.Clear();
        fileContent = File.ReadAllText("Assets/Resources/MayaPrompt.txt");
        messageHistory.Add(new ChatGPTClient.Message { role = "system", content = fileContent});
    }

    public void StartNPC2Chat(Button clickedButton)
    {
        Chat_Panel.SetActive(true);
        NPCName.text = "Oliver";
        chatText.text = "";

        // 초기 시스템 메시지 설정 (선택 사항)
        messageHistory.Clear();
        fileContent = File.ReadAllText("Assets/Resources/OliverPrompt.txt");
        messageHistory.Add(new ChatGPTClient.Message { role = "system", content = fileContent});
    }

    public void StartNPC3Chat(Button clickedButton)
    {
        Chat_Panel.SetActive(true);
        NPCName.text = "Lucy";
        chatText.text = "";

        // 초기 시스템 메시지 설정 (선택 사항)
        messageHistory.Clear();
        fileContent = File.ReadAllText("Assets/Resources/LucyPrompt.txt");
        messageHistory.Add(new ChatGPTClient.Message { role = "system", content = fileContent});
    }

    public void StartNPC4Chat(Button clickedButton)
    {
        Chat_Panel.SetActive(true);
        NPCName.text = "Leo";
        chatText.text = "";

        // 초기 시스템 메시지 설정 (선택 사항)
        messageHistory.Clear();
        fileContent = File.ReadAllText("Assets/Resources/LeoPrompt.txt");
        messageHistory.Add(new ChatGPTClient.Message { role = "system", content = fileContent});
        //테스트용 추가
        //StartCoroutine(chatGPTClient.GetChatGPTResponse(new List<ChatGPTClient.Message>(messageHistory), OnChatGPTResponse));
    }

    public void SendUserMessage(string userMessage)
    {
        if (string.IsNullOrWhiteSpace(userMessage))
            return;

        // 사용자 메시지 추가
        messageHistory.Add(new ChatGPTClient.Message { role = "user", content = userMessage });

        // ChatGPT에 메시지 전송
        StartCoroutine(chatGPTClient.GetChatGPTResponse(new List<ChatGPTClient.Message>(messageHistory), OnChatGPTResponse));
    }

    void OnChatGPTResponse(string response)
    {
        if (!string.IsNullOrEmpty(response))
        {
            // Assistant 메시지 추가
            messageHistory.Add(new ChatGPTClient.Message { role = "assistant", content = response });
            chatText.text = $"<b>{response}\n";
        }
    }
    void Start()
    {
    }

    public void OnSendButtonClicked()
    {
        string userMessage = userInputField.text;
        SendUserMessage(userMessage);
        userInputField.text = ""; // 입력 필드 비우기
    }
} 
