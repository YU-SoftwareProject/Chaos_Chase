using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtonController : MonoBehaviour
{
    public GameObject NPCSelection_Panel;
    public GameObject FinishButton;
    public NPCManager npcManager;

    //새롭게 추가(2024/11/13)
    public GameObject Chat_Panel;

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
        NPCSelection_Panel.SetActive(true);
        Chat_Panel.SetActive(false);

        if (npcManager != null)
        {
            npcManager.DeactivateAllNPCs();
        }
    }
}
