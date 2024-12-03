using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ObjectInfoDisplay : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI infoText;

    private void Start()
    {
        if (infoText != null)
        {
            panel.SetActive(false);
            infoText.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {

        if (panel != null && infoText != null)
        {
            panel.SetActive(true);
            infoText.gameObject.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
            infoText.gameObject.SetActive(false);
        }

        Debug.Log("Button clicked!");

        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Pointer is over a UI element.");
        }

        else
        {
            Debug.Log("Pointer is not over a UI element.");
        }
    }

}
