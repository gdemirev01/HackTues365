using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    private void Update()
    {
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
    }

    public void PauseGame(bool pause)
    {
        ToggleMenu("PauseMenu", pause);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void Back()
    {
        ToggleMenu("SettingsMenu", false);
        ToggleMenu("MainMenu", true);
    }

    public void Settings()
    {
        ToggleMenu("SettingsMenu", true);
        ToggleMenu("MainMenu", false);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    private void ToggleMenu(string name, bool enabled)
    {
        if (GameObject.Find(name))
        {
            CanvasGroup canvas = GameObject.Find(name).GetComponent<CanvasGroup>();
            if (enabled)
            {
                canvas.alpha = 1;
                canvas.interactable = true;
                canvas.blocksRaycasts = true;
            }
            else
            {
                canvas.alpha = 0;
                canvas.interactable = false;
                canvas.blocksRaycasts = false;
            }
        }
    }
}