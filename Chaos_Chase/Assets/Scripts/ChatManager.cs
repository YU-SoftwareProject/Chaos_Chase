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
    public TMP_InputField userInputField;
    public Button[] suspectButtons;
    public Button sendButton;
    public string fileContent;
    public ChatGPTClient chatGPTClient;
    private List<ChatGPTClient.Message> messageHistory = new List<ChatGPTClient.Message>();
    private Dictionary<string, NPC> npcSettings = new Dictionary<string, NPC>();

    public class NPC
    {
        public int StressLevel;
        public List<string> TopicsToLie;
        public int BaselineStress;
        public int StressResistance;
        public Dictionary<string, int> SensitiveTopics;

        public NPC()
        {
            StressLevel = 0;
            TopicsToLie = new List<string>();
            BaselineStress = 0;
            StressResistance = 0;
            SensitiveTopics = new Dictionary<string, int>();
        }
    }
    /*
    스트레스 레벨 업데이트 함수
    @param npcname: NPC 이름
    @param userMessage: 사용자 메시지
    @param interactionType: 상호작용 유형
    */
    public void UpdateStressLevel(string npcname, string userMessage, InteractionType interactionType)
    {
        if (npcSettings.ContainsKey(npcname))
        {
            NPC npc = npcSettings[npcname];
            int stressChange = 0;
            switch (interactionType)
            {
                case InteractionType.SenstiveQuestion:
                    foreach (string topic in npc.SensitiveTopics.Keys)
                    {
                        if (userMessage.Contains(topic))
                            {
                                stressChange += npc.SensitiveTopics[topic];
                            }
                    }
                    break;
                case InteractionType.AggressiveQuestion:
                    stressChange += 10;
                    break;
                case InteractionType.RepeatQuestion:
                    stressChange += 5;
                    break;
                case InteractionType.PositiveQuestion:
                    stressChange -= 10;
                    break;
            }

            stressChange = Mathf.RoundToInt(stressChange/npc.StressResistance);

            npc.StressLevel += stressChange;
            npc.StressLevel = Mathf.Clamp(npc.StressLevel, 0, 100);
            Debug.Log($"Stress change: {stressChange}, new stress level: {npc.StressLevel}");

        }
    }
    /*
    스트레스 레벨에 따른 감정 결정 함수
    @param stressLevel: 스트레스 레벨
    @return: 감정 메시지
    */
    string DetermineEmotion(int stressLevel)
    {
        Debug.Log($"Stress Level: {stressLevel}");
        if (stressLevel == 100)
        {
            return "현재 스트레스가 최대치입니다. 질문에 불쾌감을 표출하며 때때로 답변 거부도 합니다.";
        }
        else if (stressLevel > 70)
        {
            return "현재 스트레스를 많이 받은 상태이므로 거짓말을 하고, 횡설수설하며 감정표현이 더 격해집니다.";
        }

        else if (stressLevel > 40)
        {
            return "현재 약간 스트레스를 받은 상태입니다. 초초해하며 감정표현이 조금 격해집니다.";
        }
        else
        {
            return "현재 스트레스는 없는 상태입니다. 평소와 같이 대화를 합니다.";
        }
    }
    /*
    공격적인 질문 결정 함수
    @param question: 질문
    @return: 공격적인 질문 여부
    */
    bool IsAggressiveQuestion(string question)
    {
        string[] aggressiveQuestions = {"당신", "똑바로", "거짓말"};
        foreach (string keyword in aggressiveQuestions)
        {
            if (question.Contains(keyword))
                return true;
        }
        return false;
    }

    /*
    상호작용 유형 열거형
    */
    public enum InteractionType
    {
        SenstiveQuestion,
        AggressiveQuestion,
        RepeatQuestion,
        PositiveQuestion
    }

    /*
    질문의 종류 분류 함수
    @param npcname: NPC 이름
    @param question: 질문
    @param interactionType: 상호작용 유형
    @return: 진실 또는 거짓 여부
    */
    bool DecideTruthOrLie(string npcname, string question, out InteractionType interactionType)
    {
        interactionType = InteractionType.SenstiveQuestion;

        if (npcSettings.ContainsKey(npcname))
        {
            NPC npc = npcSettings[npcname];

            foreach (string topic in npc.SensitiveTopics.Keys)
            {
                if (question.Contains(topic))
                {
                    interactionType = InteractionType.SenstiveQuestion;
                    return false;
                }
            }

            if (IsAggressiveQuestion(question))
            {
                interactionType = InteractionType.AggressiveQuestion;
                return false;
            }

            interactionType = InteractionType.PositiveQuestion;
            return true;
        }

        return false;
    }
    /*
    NPC 채팅 시작 함수
    @param npcname: NPC 이름
    */
    public void StartNPCChat(string npcname)
    {
        Chat_Panel.SetActive(true);
        NPCName.text = npcname;
        chatText.text = "";

        messageHistory.Clear();

        string fileContent = File.ReadAllText($"Assets/Resources/{npcname}Prompt.txt");
        messageHistory.Add(new ChatGPTClient.Message { role = "system", content = fileContent});
    }

    /*
    NPC 1 채팅 시작 함수
    @param clickedButton: 버튼
    */
    public void StartNPC1Chat(Button clickedButton)
    {
        StartNPCChat("Maya");
    }

    public void StartNPC2Chat(Button clickedButton)
    {
        StartNPCChat("Oliver");
    }

    public void StartNPC3Chat(Button clickedButton)
    {
        StartNPCChat("Lucy");
    }

    public void StartNPC4Chat(Button clickedButton)
    {
        StartNPCChat("Leo");
    }

    /*
    사용자 메시지 전송 함수
    @param userMessage: 사용자 메시지
    */
    public void SendUserMessage(string userMessage)
    {
        if (string.IsNullOrWhiteSpace(userMessage))
            return;

        // 사용자 메시지 추가
        messageHistory.Add(new ChatGPTClient.Message { role = "user", content = userMessage });

        string npcName = NPCName.text;

        if (!npcSettings.ContainsKey(npcName))
        {
            Debug.LogError($"NPC {npcName} not found.");
            return;
        }

        NPC currentNPC = npcSettings[npcName];

        InteractionType interactionType;
        bool isTruth = DecideTruthOrLie(npcName, userMessage, out interactionType);

        Debug.Log($"InteractionType after DecideTruthOrLie: {interactionType}");

        UpdateStressLevel(npcName, userMessage, interactionType);

        //int stressLevel = currentNPC.StressLevel;
        string systemMessage = DetermineEmotion(currentNPC.StressLevel);
        currentNPC.StressLevel -= 10;
        messageHistory.Add(new ChatGPTClient.Message { role = "system", content = systemMessage });

        Debug.Log($"User: {systemMessage}");
        Debug.Log($"sensitiveTopics: {currentNPC.SensitiveTopics.Count}");

        // ChatGPT에 메시지 전송
        StartCoroutine(chatGPTClient.GetChatGPTResponse(new List<ChatGPTClient.Message>(messageHistory), OnChatGPTResponse));
    }

    /*
    ChatGPT 응답 함수
    @param response: 응답
    */
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
        NPC maya = new NPC();
        maya.StressResistance = 1;
        // 불륜 및 Leo 관련 증거
        maya.SensitiveTopics.Add("반지", 80);
        maya.SensitiveTopics.Add("불륜", 90);  // '불륜', '바람' 등의 단어는 극도로 민감
        maya.SensitiveTopics.Add("바람", 90);
        maya.SensitiveTopics.Add("Leo", 60);  // Leo 언급 자체도 큰 스트레스
        // 직접적으로 들통날 수 있는 증거물
        maya.SensitiveTopics.Add("시계", 70); // 이미 코드 예시 존재, Leo와 연관
        maya.SensitiveTopics.Add("사진", 10); // Leo에게 준 선물로 확인 가능
        // 인터뷰 기사나 언론 노출 관련
        maya.SensitiveTopics.Add("인터뷰", 40); // 자신이 한 인터뷰 언급 시 이미지 관리 부담
        maya.SensitiveTopics.Add("스캔들", 50); // 스캔들성 질문은 이미지에 타격
        // Hazel 관련
        maya.SensitiveTopics.Add("죽음", 30); // 죽음 언급 자체도 심리적 압박
        maya.SensitiveTopics.Add("액세서리", 20); // Hazel이 준 액세서리에 대한 언급 (죄책감 유발)
        maya.SensitiveTopics.Add("목걸이", 25); // Hazel 선물로 받은 목걸이 언급 시 미묘한 스트레스
        // 커리어, 이미지 관련
        maya.SensitiveTopics.Add("커리어", 50); // 커리어 관련 위협적 질문 시 스트레스
        maya.SensitiveTopics.Add("이미지", 50); // 이미지 관리에 매우 민감

        npcSettings.Add("Maya", maya);

        NPC oliver = new NPC();
        // 경제적 압박 관련
        oliver.SensitiveTopics.Add("돈", 50);        // 돈 문제 직접 언급
        oliver.SensitiveTopics.Add("35,000달러", 50); // 빌린 구체적 금액
        oliver.SensitiveTopics.Add("채무", 50);      // 빚/채무 언급
        oliver.SensitiveTopics.Add("무직", 40);      // 현재 일자리 없음 언급
        oliver.SensitiveTopics.Add("일자리", 40);    // 일자리 구하지 못한 상태 지적
        oliver.SensitiveTopics.Add("무능함", 40);    // 능력이 부족하다는 식의 모욕
        // 관계 및 감정적 압박 관련
        oliver.SensitiveTopics.Add("Hazel 죽음", 30); // Hazel 죽음 자체도 심리적 압박
        oliver.SensitiveTopics.Add("멸시", 40);       // Hazel로부터 받은 멸시나 망신 행위
        oliver.SensitiveTopics.Add("망신", 40);       // 공개적인 망신 언급

        // 불륜(바람) 관련
        oliver.SensitiveTopics.Add("불륜", 60);      // Leo의 바람 언급 -> 죄책감 극대화
        oliver.SensitiveTopics.Add("바람", 60);      // 같은 의미로 처리

        // Leo 언급
        oliver.SensitiveTopics.Add("Leo", 20);       // Leo 언급은 불륜 관련만큼은 아니지만 부담

        NPC lucy = new NPC();
        lucy.StressResistance = 8;
        // 논문 및 인정받지 못한 노력 관련
        lucy.SensitiveTopics.Add("논문", 40);        // 논문 관련 전반적 언급
        lucy.SensitiveTopics.Add("이름 누락", 80);   // 이름 누락은 최상위 민감 주제
        lucy.SensitiveTopics.Add("인정받지 못함", 60); // 노력 무시에 대한 분노와 실망
        lucy.SensitiveTopics.Add("배신감", 50);       // Hazel에 대한 배신감

        // Hazel 관련
        lucy.SensitiveTopics.Add("Hazel 죽음", 30);   // Hazel 죽음으로 인한 혼란과 슬픔
        lucy.SensitiveTopics.Add("교수님", 20);       // Hazel(교수)에 대한 언급은 기본적 스트레스

        // 불륜 관련
        lucy.SensitiveTopics.Add("불륜", 20);         // Maya와 Leo의 불륜에 대한 언급은 불편하지만 최우선은 아님
        lucy.SensitiveTopics.Add("바람", 20);         // '불륜'과 유사한 맥락

        // 메모 관련
        lucy.SensitiveTopics.Add("메모", 10);         // 메모 언급은 낮은 정도의 스트레스

        NPC leo = new NPC();
        leo.StressResistance = 10;

        // 불륜 관련 키워드
        leo.SensitiveTopics.Add("불륜", 80);   // 비밀 관계 들통날 수 있는 핵심 키워드
        leo.SensitiveTopics.Add("바람", 80);   // 불륜과 동일한 맥락, 매우 민감

        // 관련 증거물(반지, 시계)
        leo.SensitiveTopics.Add("반지", 60);   // 화장실, 침실에서 발견된 반지 언급 시 스트레스
        leo.SensitiveTopics.Add("시계", 50);   // Maya가 준 시계 언급 시 스트레스

        // Hazel의 죽음
        leo.SensitiveTopics.Add("Hazel 죽음", 40);  // Hazel 죽음 언급 시 죄책감과 슬픔

        // Maya 언급
        leo.SensitiveTopics.Add("Maya", 30);  // Maya와의 관계를 의심케 하는 언급은 부담

        // 기타 이미지 관리 관련(스캔들, 연루)
        leo.SensitiveTopics.Add("스캔들", 50);   // 스캔들성 질문은 이미지와 커리어에 타격 -> 스트레스
        }

    /*
    버튼 클릭 함수
    */
    public void OnSendButtonClicked()
    {
        string userMessage = userInputField.text;
        SendUserMessage(userMessage);
        userInputField.text = "";
    }
} 