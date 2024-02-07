using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField]
    private TextMeshProUGUI progressText;

    private void Start()
    {
        StartCoroutine("LoadScene");
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;

            progressText.text = string.Format("{0}", op.progress * 110);

            if (op.progress == 0.9f)
            {
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
