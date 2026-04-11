using TMPro;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesLeftText;

    [SerializeField] CinemachineVirtualCamera winVirtualCamera;
    //[SerializeField] GameObject youWinText;

    [SerializeField] GameObject winContainer;

    int enemiesLeft = 0;

    int winVirtualCameraPriority = 20;

    const string ENEMIES_LEFT_STRING = "Enemies Left: ";

    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft +=amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        if (enemiesLeft <= 0)
        {
            //youWinText.SetActive(true);
            winContainer.SetActive(true);
            winVirtualCamera.Priority = winVirtualCameraPriority;
            StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
            starterAssetsInputs.SetCursorState(false);
        }
    }
    public void RestartLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton()
    {
        Debug.LogWarning("Does not work in the UNITY EDITOR!!");
        Application.Quit();
    }
}
