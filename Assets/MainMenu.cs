using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Fungsi untuk tombol Play
    public void PlayGame()
    {
        // Ganti "GameScene" dengan nama scene game kamu
        SceneManager.LoadScene(1);
    }

    // Fungsi untuk tombol Exit
    public void ExitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}
