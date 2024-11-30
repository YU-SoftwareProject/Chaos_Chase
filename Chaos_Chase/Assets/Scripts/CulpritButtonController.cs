using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CulpritButtonController : MonoBehaviour
{
    public GameObject CulpritSelection_Panel;
    public GameObject Culprit_Button;
    public GameObject Culprit;
    public GameObject MessageBox;

    public GameObject Maya_Btn;
    public GameObject Oliver_Btn;
    public GameObject Lucy_Btn;
    public GameObject Leo_Btn;
    
    void Start()
    {
        CulpritSelection_Panel.SetActive(false);
        MessageBox.SetActive(false);
        //새롭게 추가 (2024/11/18) 씬 전환시 Object 유지
        DontDestroyOnLoad(gameObject); 
    }

    public void OnMayaBtnClick() 
    {
        Culprit = Maya_Btn;
    }

    public void OnOliverBtnClick() 
    {
        Culprit = Oliver_Btn;
    }

    public void OnLucyBtnClick() 
    {
        Culprit = Lucy_Btn;
    }

    public void OnLeoBtnClick() 
    {
        Culprit = Leo_Btn;
    }

    public void OnCulpritBtnClick()
    {
        // 범인을 선택하지 않은 상태에서 범인 선택 버튼을 클릭한 경우, 메시지 박스 3초간 활성화
        if (Culprit == null)
        {
            StartCoroutine(ActivateMessageBox(3f));
        }

        // 범인을 선택한 상태여야 범인 선택 버튼을 클릭 시 씬 전환 코드가 실행되도록 로직 변경
        if (Culprit != null)
        {
            LoadingSceneController.LoadScene("EndingScene");
        }
        
    }

    // 'Please Decide the Culprit' 메시지 박스를 활성화하는 함수
    IEnumerator ActivateMessageBox(float seconds) 
    {
        MessageBox.SetActive(true);
        yield return new WaitForSeconds(seconds);
        MessageBox.SetActive(false);
    }
}
