using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image progressBar;
    [SerializeField] private Text progressText;
    [SerializeField] private Text tipsText;
    [SerializeField] private Animator fadeAnimator;

    [Header("Loading Settings")]
    [SerializeField] private string[] loadingTips;
    [SerializeField] private string sceneToLoad = "gedung_asrama";
    [SerializeField] private float minimumLoadTime = 2f;

    private float loadStartTime;

    private void Start()
    {
        loadStartTime = Time.time;
        ShowRandomTip();
        StartCoroutine(LoadSceneAsync());
    }

    private void ShowRandomTip()
    {
        if (loadingTips != null && loadingTips.Length > 0)
        {
            int index = Random.Range(0, loadingTips.Length);
            tipsText.text = loadingTips[index];
        }
    }

    private IEnumerator LoadSceneAsync()
    {
        // Pre-load assets jika diperlukan
        yield return StartCoroutine(PreloadAssets());

        // Mulai loading scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            // Update UI
            progressBar.fillAmount = progress;
            progressText.text = $"Loading... {Mathf.RoundToInt(progress * 100)}%";

            // Pastikan minimum loading time terpenuhi
            float elapsedTime = Time.time - loadStartTime;
            bool minLoadTimeMet = elapsedTime >= minimumLoadTime;

            // Ketika loading selesai
            if (progress >= 0.9f && minLoadTimeMet)
            {
                // Jika menggunakan fade transition
                if (fadeAnimator != null)
                {
                    fadeAnimator.SetTrigger("FadeOut");
                    yield return new WaitForSeconds(fadeAnimator.GetCurrentAnimatorStateInfo(0).length);
                }

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private IEnumerator PreloadAssets()
    {
        // Contoh pre-loading resources
        var asyncLoad = Resources.LoadAsync("Prefabs");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    // Method untuk dipanggil dari scene lain
    public static void LoadScene(string sceneName)
    {
        // Simpan nama scene yang akan diload
        PlayerPrefs.SetString("SceneToLoad", sceneName);
        // Load scene loading screen
        SceneManager.LoadScene("LoadingScreen");
    }
}