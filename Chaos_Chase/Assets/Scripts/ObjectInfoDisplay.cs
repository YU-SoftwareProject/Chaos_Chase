using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectInfoDisplay : MonoBehaviour
{
    public GameObject panel; // 패널 오브젝트
    public TextMeshProUGUI infoText; // 패널 내 텍스트



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
    }

}
