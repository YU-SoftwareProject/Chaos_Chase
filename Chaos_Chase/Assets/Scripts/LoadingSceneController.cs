using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image progressbar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        
        while(!op.isDone)
        {
            yield return null;

            if (op.progress < 0.1f)
            {
                progressbar.fillAmount = op.progress;
            }
            else
            {
                progressbar.fillAmount += 0.01f;
                if (progressbar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
