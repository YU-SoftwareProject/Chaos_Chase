using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class CaseEndingChecker : MonoBehaviour
{
    [HideInInspector]
    public CulpritButtonController culpritButtonController;
    public GameObject ResultCheck_Btn;
    public GameObject MainMenuButton;
    public GameObject ResultCheck_CorrectPanel;
    public GameObject ResultCheck_WrongPanel;
    // 정답 패널의 텍스트 배열
    public TextMeshProUGUI[] CorrectTextFields;
    //오답 패널의 텍스트 배열
    public TextMeshProUGUI[] WrongTextFields;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerNameText2;

    private int currentTextIndex = 0;
    private string[] correctTexts = { "드디어 사건이 끝났다.", "아주 깔끔하고 명확한 추리였다.", "얼른 집에서 따뜻한 물로 씻고, 자야겠다." };
    private string[] wrongTexts = { "...정말 사건이 잘 마무리된 게 맞을까", "무언가 놓치고 있는 것만 같은 느낌이 든다." };

    void Start()
    {
        //EndingScene에서 CulpritButtonController 동적 참조
        culpritButtonController = FindObjectOfType<CulpritButtonController>();

        if (culpritButtonController == null)
        {
            Debug.LogError("CulpritButtonController not found in the scene!");
        }
        //처음에 결과 패널과 메인 메뉴 버튼 비활성화
        ResultCheck_CorrectPanel.SetActive(false);
        ResultCheck_WrongPanel.SetActive(false);
        MainMenuButton.SetActive(false);

        //PlayerNameText에 현재 플레이어에 저장된 이름을 대입하여 표시 
        playerNameText.text = DataManager.instance.currentPlayer.name;
        playerNameText2.text = DataManager.instance.currentPlayer.name;

    }


    public void OnResultCheckBtnClick()
    {
        //결과 확인 버튼 클릭하면 버튼은 비활성화
        ResultCheck_Btn.SetActive(false);

        //Maya를 범인으로 지목했다면 Correct Panel 활성화, 그렇지 않으면 Worng Panel 활성화
        if (culpritButtonController.Culprit == culpritButtonController.Maya_Btn)
        {
            ResultCheck_CorrectPanel.SetActive(true);
            currentTextIndex = 0;
            UpdateCorrectText();
            Debug.Log("정답");
        }
        else
        {
            ResultCheck_WrongPanel.SetActive(true);
            currentTextIndex = 0;
            UpdateWrongText();
            Debug.Log("오답");
        }
    }
    private void UpdateCorrectText()
    {
        //CorrectTextFields에 저장된 인덱스에 해당하는 텍스트를 활성화하고, 해당 인덱스를 제외한 텍스트는 비활성화
        for (int i = 0; i < CorrectTextFields.Length; i++)
        {
            if (i == currentTextIndex)
            {
                CorrectTextFields[i].gameObject.SetActive(true);
                CorrectTextFields[i].text = correctTexts[i];
            }
            else
            {
                CorrectTextFields[i].gameObject.SetActive(false);
            }
        }
    }
    private void UpdateWrongText()
    {
        for (int i = 0; i < WrongTextFields.Length; i++)
        {
            if (i == currentTextIndex)
            {
                WrongTextFields[i].gameObject.SetActive(true);
                WrongTextFields[i].text = wrongTexts[i];
            }
            else
            {
                WrongTextFields[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnCorrectPanelClick()
    {
        //패널 클릭할 때 인덱스 1 증가시키고 해당 텍스트 활성화/인덱스가 저장된 배열 수 넘어가면 패널 비활성화로 결과 확인 끝 & 메인 메뉴로 돌아가는 버튼 활성화
        if (currentTextIndex < correctTexts.Length - 1)
        {
            currentTextIndex++;
            UpdateCorrectText();
        }
        else
        {
            ResultCheck_CorrectPanel.SetActive(false);
            MainMenuButton.SetActive(true);
            ResultCheck_Btn.SetActive(true);
        }
    }

    public void OnWrongPanelClick()
    {
        if (currentTextIndex < wrongTexts.Length - 1)
        {
            currentTextIndex++;
            UpdateWrongText();
        }
        else
        {
            ResultCheck_WrongPanel.SetActive(false);
            MainMenuButton.SetActive(true);
            ResultCheck_Btn.SetActive(true);
        }
    }

    public void OnReturnMainBtnClick()
    {
        LoadingSceneController.LoadScene("MainMenuScene");
    }

}

