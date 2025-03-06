using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Slider progressBar;
    public TMP_Text progressText;

    void Start()
    {
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadingData.SelectedLevel);
        if (operation != null)
        {
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                progressBar.value = progress;
                progressText.text = (progress * 100).ToString("F0") + "%";

                if (operation.progress >= 0.9f)
                {
                    yield return new WaitForSecondsRealtime(1f);
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}
