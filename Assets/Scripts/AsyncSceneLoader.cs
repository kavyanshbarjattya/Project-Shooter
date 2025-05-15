using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncSceneLoader : MonoBehaviour
{
    [Header("UI References")]
    public GameObject loadingScreen;
    public Slider progressBar;        // Or use Image if you're using FillAmount
    public Text progressText;         // Optional for showing % text

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadAsyncRoutine(sceneName));
    }

    IEnumerator LoadAsyncRoutine(string sceneName)
    {
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Progress is [0.0f, 0.9f] while loading, [0.9f, 1.0f] when ready
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
          /*  if (progressText != null)
                progressText.text = (progress * 100f).ToString("F0") + "%";*/

            // Press a key or auto trigger once complete
            if (progress >= 1f)
            {
                yield return new WaitForSeconds(0.2f); // optional delay
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
