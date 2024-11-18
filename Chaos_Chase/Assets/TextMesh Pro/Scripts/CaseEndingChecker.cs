using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseEndingChecker : MonoBehaviour
{
    [HideInInspector]
    public CulpritButtonController culpritButtonController;
    public GameObject ResultCheck_Btn;
    public GameObject ResultCheck_CorrectPanel;
    public GameObject ResultCheck_WrongPanel;

    void Start()
    {
        //EndingScene에서 CulpritButtonController 동적 참조
        culpritButtonController = FindObjectOfType<CulpritButtonController>();

        if (culpritButtonController == null)
        {
            Debug.LogError("CulpritButtonController not found in the scene!");
        }
        
        ResultCheck_CorrectPanel.SetActive(false);
        ResultCheck_WrongPanel.SetActive(false);
    }


    public void OnResultCheckBtnClick()
    {
        ResultCheck_Btn.SetActive(false);
        if (culpritButtonController.Culprit == culpritButtonController.Maya_Btn )
        {
            ResultCheck_CorrectPanel.SetActive(true);
            Debug.Log("정답");
        }
        else
        {
            ResultCheck_WrongPanel.SetActive(true) ;
            Debug.Log("오답");
        }
    }
}

