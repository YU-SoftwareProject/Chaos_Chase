using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectInfoDisplay : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    private Coroutine hideCoroutine;

    private void Start()
    {
        if (infoText != null)
        {
            infoText.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (infoText != null)
        {
            infoText.gameObject.SetActive(true);

            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }

            hideCoroutine = StartCoroutine(HideTextAfterSeconds(2f));
        }
    }

    private IEnumerator HideTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        infoText.gameObject.SetActive(false);
    }
    
        
}
