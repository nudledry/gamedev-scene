using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;

    // Fungsi untuk tombol Play
    public void PlayGame()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadGameAsync());
    }

    IEnumerator LoadGameAsync()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(1);
        
        while (!loading.isDone)
        {
            float progress = Mathf.Clamp01(loading.progress / 0.9f);
            loadingBar.value = progress;
            yield return null;
        }
    }

    // Fungsi untuk tombol Exit
    public void ExitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}
