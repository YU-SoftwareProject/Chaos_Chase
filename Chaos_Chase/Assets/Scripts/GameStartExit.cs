using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartExit : MonoBehaviour
{
    public GameObject SelectorPanel;
    public SelectorScript selectorScript;

    public GameObject noDataPanel;
    public GameObject deleteDataPanel;

    public void OnStartBtnClick()
    {
        SelectorPanel.SetActive(true);
    }

    public void OnExitBtnClick()
    {
        Application.Quit();

        // 에디터 환경에서는 실행 중지로 게임 종료 구현
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OnBackBtnClcik()
    {
        SelectorPanel.SetActive(false);
        noDataPanel.SetActive(false);
        deleteDataPanel.SetActive(false);
    }

    public void OnConfirmBtnClick()
    {
        LoadingSceneController.LoadScene("InvestigationScene");
    }
}
