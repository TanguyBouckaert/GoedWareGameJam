using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenUI : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ReturnToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

}
