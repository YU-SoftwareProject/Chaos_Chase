using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectorScript : MonoBehaviour
{
    public bool char1;
    public bool char2;

    public bool gameStart;
    public bool isSelectorCheck;

    public GameObject[] checkList;

    public GameObject character1;
    public GameObject character2;

    public string playerName;
    public int playerAge;
    public bool isMale;

    public InputField nameInputField;
    public InputField ageInputField;

    public Toggle maleToggle;
    public Toggle femaleToggle;
    public ToggleGroup genderGroup;

    void Start()
    {
        char1 = true;
        char2 = false;

        maleToggle.isOn = true;
        femaleToggle.isOn = false;

        if (gameStart)
        {
            ActivateSelectedCharacter();
        }
    }

    private void ActivateSelectedCharacter()
    {
        if (char1)
        {
            character1.SetActive(true);
            character2.SetActive(false);
        }
        else if (char2)
        {
            character2.SetActive(true);
            character1.SetActive(false);
        }
    }

    public void SelectChar1()
    {
        char1 = true;
        char2 = false;

        UpdateCheckmarks();
    }

    public void SelectChar2()
    {
        char1 = false;
        char2 = true;

        UpdateCheckmarks();
    }

    private void UpdateCheckmarks()
    {
        if (checkList.Length >= 2)
        {
            checkList[0].SetActive(char1);
            checkList[1].SetActive(char2);
        }
    }

    public void OnGenderChanged()
    {
        isMale = maleToggle.isOn;
    }
    public void SavePlayerInfo()
    {
        if (nameInputField != null)
        {
            playerName = nameInputField.text;
        }
        if (ageInputField != null)
        {
            playerAge = int.TryParse(ageInputField.text, out int age) ? age : 0;
        }
        isMale = maleToggle.isOn;
    }

    private void Update()
    {
        if (isSelectorCheck)
        {
            UpdateCheckmarks();
        }
    }
}