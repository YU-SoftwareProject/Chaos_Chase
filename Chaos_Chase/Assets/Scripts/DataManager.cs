using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using HeathenEngineering.Events;

public class PlayerData
{
    // 플레이어 이름, 성별, 나이
    public string name;
    public string gender;
    public int age;
    
    // 플레이어 캐릭터, 선택한 스테이지
    public string character;
    public int stage;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData currentPlayer = new PlayerData();

    string savePath;
    string fileName = "SaveData";

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        savePath = Application.persistentDataPath + "/";
    }

    public void SaveData()
    {
        string saveData = JsonUtility.ToJson(currentPlayer);

        File.WriteAllText(savePath + fileName, saveData);
    }

    public void LoadData()
    {
        string loadData = File.ReadAllText(savePath + fileName);

        currentPlayer = JsonUtility.FromJson<PlayerData>(loadData);
    }
}
