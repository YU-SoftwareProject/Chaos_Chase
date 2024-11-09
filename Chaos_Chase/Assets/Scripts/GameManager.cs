using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject casePanel;
    public GameObject noteImage;
    public TMP_InputField inputName;
    public TMP_InputField inpuCaseType;
    public TMP_InputField inpuVictimName;
    public TMP_InputField inputOccupation;
    public TMP_InputField inputBackground;
    public TMP_InputField inputSuspect;
    public TMP_InputField inputNote;

    private int caseBtnClickCount = 0;
    private int noteBtnClickCount = 0;

    public GameObject inventoryPanel;
    bool activeInventory = false;

    public void OnCaseBtnClick() 
    {
        // Case Record 버튼을 처음 눌렀을 경우, 패널 활성화
        if (caseBtnClickCount == 0) 
        {
            casePanel.SetActive(true);
        }
        // Case Record 버튼을 다시 눌러 Case Record 패널을 켰을 경우, 불러오기 및 패널 활성화
        else if (caseBtnClickCount % 2 == 0 && caseBtnClickCount > 0)
        {
            LoadCase();
            casePanel.SetActive(true);
        }
        // Case Record 버튼을 다시 눌러 Case Record 패널을 껐을 경우, 저장 및 패널 비활성화
        else 
        {
            SaveCase();
            casePanel.SetActive(false);
        }
         caseBtnClickCount++;
    }
    
    public void SaveCase() 
    {
        PlayerPrefs.SetString("Name", inputName.text);
        PlayerPrefs.SetString("CaseType", inpuCaseType.text);
        PlayerPrefs.SetString("VictimName", inpuVictimName.text);
        PlayerPrefs.SetString("Occupation", inputOccupation.text);
        PlayerPrefs.SetString("Background", inputBackground.text);
        PlayerPrefs.SetString("Suspect", inputSuspect.text);
    }

    public void LoadCase() 
    {
        if(PlayerPrefs.HasKey("Name"))
        {
            inputName.text = PlayerPrefs.GetString("Name");
            inpuCaseType.text = PlayerPrefs.GetString("CaseType");
            inpuVictimName.text = PlayerPrefs.GetString("VictimName");
            inputOccupation.text = PlayerPrefs.GetString("Occupation");
            inputBackground.text = PlayerPrefs.GetString("Background");
            inputSuspect.text = PlayerPrefs.GetString("Suspect");
        }
    }

    public void OnNoteBtnClick()
    {
        // Note 버튼을 처음 눌렀을 경우, 패널 활성화
        if (noteBtnClickCount == 0) 
        {
            noteImage.SetActive(true);
        }
        // Note 버튼을 다시 눌러 Note를 켰을 경우, 불러오기 및 패널 활성화
        else if (noteBtnClickCount % 2 == 0 && noteBtnClickCount > 0)
        {
            LoadNote();
            noteImage.SetActive(true);
        }
        // Note 버튼을 다시 눌러 Note를 껐을 경우, 저장 및 패널 비활성화
        else 
        {
            SaveNote();
            noteImage.SetActive(false);
        }
         noteBtnClickCount++;
    }

    public void SaveNote()
    {
        PlayerPrefs.SetString("Note", inputNote.text);
    }

    public void LoadNote()
    {
        if(PlayerPrefs.HasKey("Name"))
        {
            inputNote.text = PlayerPrefs.GetString("Note");
        }
    }

    private void Update() 
    {
        // I 키를 누르면 인벤토리 활성화
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }
}
