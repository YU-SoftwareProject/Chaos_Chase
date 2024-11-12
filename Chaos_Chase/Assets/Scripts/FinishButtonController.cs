using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtonController : MonoBehaviour
{
    public GameObject NPCSelection_Panel;
    public GameObject FinishButton;
    public NPCManager npcManager;

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

        if (npcManager != null)
        {
            npcManager.DeactivateAllNPCs();
        }
    }
}
