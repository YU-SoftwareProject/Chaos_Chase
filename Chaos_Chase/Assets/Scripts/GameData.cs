using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{  
    // 플레이어 이름, 나이 Inputfield
    public TMP_InputField nameInputField;
    public TMP_InputField ageInputField;

    // 플레이어 성별 Toggle
    public Toggle maleToggle;
    public Toggle femaleToggle;

    // 플레이어 캐릭터, 선택한 스테이지
    public string playerCharacter;
    public int selectStage;

    public void SelectMaleCharacter()
    {
        playerCharacter = "Man";
    }

    public void SelectFemaleCharacter()
    {
        playerCharacter = "Woman";
    }

    public void SelectStage1()
    {
        selectStage = 1;
    }

    public void SavePlayerData()
    {
        DataManager.instance.currentPlayer.name = nameInputField.text;
        DataManager.instance.currentPlayer.age = int.Parse(ageInputField.text);

        // 선택한 성별 Toggle에 알맞게 현재 플레이어 정보에 남성/여성으로 저장
        if (maleToggle.isOn)
        {
            DataManager.instance.currentPlayer.gender = "Male";
        }
        
        else if (femaleToggle.isOn)
        {
            DataManager.instance.currentPlayer.gender = "Female";
        }

        // 선택한 캐릭터 성별에 알맞게 현재 플레이어 정보에 남자/여자로 저장
        if (playerCharacter == "Man")
        {
            DataManager.instance.currentPlayer.character = "Man";
        }

        else if (playerCharacter == "Woman")
        {
            DataManager.instance.currentPlayer.character = "Woman";
        }

        DataManager.instance.SaveData();
    }

    public void SaveStageData()
    {
        // 선택한 스테이지에 알맞게 현재 플레이어 정보에 스테이지 번호를 저장
        if (selectStage == 1)
        {
            DataManager.instance.currentPlayer.stage = 1;
        }
        DataManager.instance.SaveData();
    }
}
