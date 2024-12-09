using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessageFirstTime : MonoBehaviour
{

    [Header("Assign the GameObject that contains the message below")]
    public GameObject messageContainer;
    public MonoBehaviour playerController;
    public MonoBehaviour mouseLook;

    int storedData = 0;

    [Header("Define a unique name to store your data")]
    public string dataStoredName = "GameDevTraum_DisplayMessageFirstTime";
    //Use the name you like for saving data

    [Header("Check the box below to delete stored data")]
    [SerializeField]
    bool deleteStoredDataInEditor = false;

    void Awake()
    {
        storedData = PlayerPrefs.GetInt(dataStoredName, 0);

        if (messageContainer != null)
        {
            bool isFirstTime = (storedData == 0);
            messageContainer.SetActive(isFirstTime);

            if (isFirstTime)
            {
                if (playerController != null)
                    playerController.enabled = false;
                if (mouseLook != null)
                    mouseLook.enabled = false;
            }
        }
        else
        {
            Debug.Log("There is no GameObject assigned in the messageContainer variable in inspector");
        }
        PlayerPrefs.SetInt(dataStoredName, 1);
    }

    public void CloseMessage()
    {
        messageContainer.SetActive(false);

        if (playerController != null)
            playerController.enabled = true;
        if (mouseLook != null)
            mouseLook.enabled = true;
    }


    private void OnValidate()
    {
        if (deleteStoredDataInEditor)
        {
            deleteStoredDataInEditor = false;
            PlayerPrefs.DeleteKey(dataStoredName);
            Debug.Log("Data deleted, message will appear next session");
        }
    }
}
