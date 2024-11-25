using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartExit : MonoBehaviour
{
    public GameObject SelectorPanel;
    public SelectorScript selectorScript;

    public void OnStartBtnClick()
    {
        SelectorPanel.SetActive(true);
    }

    public void OnConfirmBtnClick()
    {
        SceneManager.LoadScene("InvestigationScene");
    }
    public void ClosePanel()
    {
        SelectorPanel.SetActive(false);
    }

    public void OnExitBtnClick()
    {
        Application.Quit();

        // 에디터 환경에서는 실행 중지로 게임 종료 구현
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
