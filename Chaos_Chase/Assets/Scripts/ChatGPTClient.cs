using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using Newtonsoft.Json; // Use Newtonsoft.Json
 
public class ChatGPTClient : MonoBehaviour
{
    private string apiKey = "API_KEY"; // Replace with an actual API key
    private string apiUrl = "https://api.openai.com/v1/chat/completions";
 
    public IEnumerator GetChatGPTResponse(List<Message> message, System.Action<string> callback)
    {
        // Setting OpenAI API Request Data
        var jsonData = new
        {
            model = "gpt-4o",
            messages = message,
            temperature = 0.7,
            max_tokens = 100
        };
 
        string jsonString = JsonConvert.SerializeObject(jsonData);
 
        // HTTP request settings
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);
 
        yield return request.SendWebRequest();
 
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            callback?.Invoke("Error: " + request.error);
        }
        else
        {
            var responseText = request.downloadHandler.text;
            Debug.Log("Response: " + responseText);
            // Parse the JSON response to extract the required parts
            var response = JsonConvert.DeserializeObject<OpenAIResponse>(responseText);
            callback(response.choices[0].message.content.Trim());
        }
    }
 
    public class OpenAIResponse
    {
        public Choice[] choices { get; set; }
    }
 
    public class Choice
    {
        public Message message { get; set; }
    }
 
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}