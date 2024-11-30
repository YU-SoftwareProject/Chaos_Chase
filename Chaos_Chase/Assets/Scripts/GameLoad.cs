using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameLoad : MonoBehaviour
{
    public GameObject gameStartBtn;
    public GameObject loadBtn;

    public GameObject noDataPanel;
    public GameObject loadDataPanel;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerNameText2;
    public GameObject deleteDataPanel;
    public GameObject deleteMessageBox;

    void Start()
    {
        string saveFilePath = Application.persistentDataPath + "/SaveData";

        // 처음에는 'Load' 버튼을 비활성화하여 'Game start' 버튼을 클릭하도록 안내
        gameStartBtn.SetActive(true);
        loadBtn.SetActive(false);

        // 지정 경로에 SaveData 파일이 존재하면 'Load' 버튼 활성화, 'Game Start' 버튼 비활성화
        if (File.Exists(saveFilePath))
        {
            gameStartBtn.SetActive(false);
            loadBtn.SetActive(true);
        }
    }

    // 메인 메뉴에서 Load 버튼을 클릭했을 때 실행
    public void OnLoadBtnClick()
    {
        string saveFilePath = Application.persistentDataPath + "/SaveData";

        // 지정 경로에 SaveData 파일이 존재하면 데이터 불러오기를 실행하고 LoadData 패널 활성화
        if (File.Exists(saveFilePath))
        {
            DataManager.instance.LoadData();

            loadDataPanel.SetActive(true);

            // LoadData 패널 내 playerNameText에 현재 플레이어 정보에 저장된 이름을 대입하여 표시
            playerNameText.text = DataManager.instance.currentPlayer.name;
        }
        
        // 파일이 존재하지 않으면 noData 패널을 활성화하여 Game start 버튼을 클릭하도록 안내
        else 
        {
            noDataPanel.SetActive(true);
        }
    }

    // 불러오기 패널에서 Yes 버튼을 클릭하면 바로 이어서 게임 진행될 수 있도록 불러오기
    public void OnLoadYesBtnClick()
    {
        // 비동기 씬 전환 기능 개발 후 작성
    }

    // 불러오기 패널에서 Delete 버튼을 클릭하면 데이터 삭제 패널 활성화
    public void OnDeleteBtnClick()
    {
        loadDataPanel.SetActive(false);
        deleteMessageBox.SetActive(false);
        deleteDataPanel.SetActive(true);

        DataManager.instance.LoadData();

        // DeleteData 패널 내 playerNameText에 현재 플레이어 정보에 저장된 이름을 대입하여 표시
        playerNameText2.text = DataManager.instance.currentPlayer.name;
    }

    // 데이터 삭제 패널에서 Yes 버튼을 클릭하면 저장된 데이터 삭제 및 삭제 완료 메시지 박스 활성화
    public void OnDeleteYesBtnClick()
    {
        string saveFilePath = Application.persistentDataPath + "/SaveData";
        File.Delete(saveFilePath);

        StartCoroutine(ActivateMessageBox(2f));

        gameStartBtn.SetActive(true);
        loadBtn.SetActive(false);
    }

    // 삭제 완료 메시지 박스를 활성화하는 코루틴 함수
    IEnumerator ActivateMessageBox(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        deleteMessageBox.SetActive(true);
    }
}
