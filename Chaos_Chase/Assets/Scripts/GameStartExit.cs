using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartExit : MonoBehaviour
{
    public void OnStartBtnClick()
    {
        SceneManager.LoadScene("InvestigationScene");
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
