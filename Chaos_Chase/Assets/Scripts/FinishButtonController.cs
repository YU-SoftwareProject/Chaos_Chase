using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtonController : MonoBehaviour
{
    public GameObject NPCSelection_Panel;
    public GameObject FinishButton;
    public NPCManager npcManager;

    private int FinishButtonClickNum = 0;
    public GameObject CulpritSelection_Panel;

    //새롭게 추가(2024/11/13)
    public GameObject Chat_Panel;
    public GameObject LieDetectPanel;

    void Start()
    {
        FinishButton.SetActive(false);
    }

    public void ShowFinishButton()
    {
        NPCSelection_Panel.SetActive(false);
        FinishButton.SetActive(true);
    }

    public void OnFinishButtonClick()
    {
        FinishButton.SetActive(false);
        FinishButtonClickNum += 1;
        NPCSelection_Panel.SetActive(true);
        Chat_Panel.SetActive(false);
        LieDetectPanel.SetActive(false);

        if (npcManager != null)
        {
            npcManager.DeactivateAllNPCs();
        }

        // 모든 NPC와의 심문을 종료했을 경우, NPC 선택 패널을 비활성화하고 범인 선택 패널을 활성화
        if (FinishButtonClickNum == 4)
        {
            NPCSelection_Panel.SetActive(false);
            CulpritSelection_Panel.SetActive(true);
        }
    }
}
