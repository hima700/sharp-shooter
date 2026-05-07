using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] string gameplaySceneName = "MainLevel";

    public void StartGameButton()
    {
        if (string.IsNullOrWhiteSpace(gameplaySceneName))
        {
            Debug.LogError("Gameplay scene name is empty.");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void QuitGameButton()
    {
        Time.timeScale = 1f;
        Debug.LogWarning("Quit does not work in the Unity Editor.");
        Application.Quit();
    }
}
