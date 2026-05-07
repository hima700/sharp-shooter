using TMPro;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesLeftText;

    [SerializeField] CinemachineVirtualCamera winVirtualCamera;
    //[SerializeField] GameObject youWinText;

    [SerializeField] GameObject winContainer;
    [SerializeField] GameObject pauseContainer;

    int enemiesLeft = 0;
    bool hasGameEnded;
    bool isPauseMenuOpen;

    int winVirtualCameraPriority = 20;

    const string ENEMIES_LEFT_STRING = "Enemies Left: ";

    void Start()
    {
        Time.timeScale = 1f;

        if (pauseContainer)
        {
            pauseContainer.SetActive(false);
        }
    }

    void Update()
    {
        if (hasGameEnded) return;

        if (IsEscapePressedThisFrame())
        {
            if (isPauseMenuOpen)
            {
                ResumeGameplay();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft +=amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        if (enemiesLeft <= 0)
        {
            if (hasGameEnded) return;

            hasGameEnded = true;
            isPauseMenuOpen = false;
            //youWinText.SetActive(true);
            winContainer.SetActive(true);
            if (pauseContainer)
            {
                pauseContainer.SetActive(false);
            }
            winVirtualCamera.Priority = winVirtualCameraPriority;
            StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
            if (starterAssetsInputs)
            {
                starterAssetsInputs.SetCursorState(false);
                starterAssetsInputs.ResetCombatInputs();
            }
        }
    }

    public void OnPlayerDefeated()
    {
        if (hasGameEnded) return;

        hasGameEnded = true;
        isPauseMenuOpen = false;
        if (pauseContainer)
        {
            pauseContainer.SetActive(false);
        }
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        if (starterAssetsInputs)
        {
            starterAssetsInputs.SetCursorState(false);
            starterAssetsInputs.ResetCombatInputs();
        }
    }

    void OpenPauseMenu()
    {
        isPauseMenuOpen = true;
        Time.timeScale = 0f;
        if (pauseContainer)
        {
            pauseContainer.SetActive(true);
        }

        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        if (starterAssetsInputs)
        {
            starterAssetsInputs.SetCursorState(false);
            starterAssetsInputs.ResetCombatInputs();
        }
    }

    public void ResumeGameplay()
    {
        if (hasGameEnded) return;

        isPauseMenuOpen = false;
        Time.timeScale = 1f;
        if (pauseContainer)
        {
            pauseContainer.SetActive(false);
        }

        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        if (starterAssetsInputs)
        {
            starterAssetsInputs.SetCursorState(true);
            starterAssetsInputs.ResetCombatInputs();
        }
    }

    public void RestartLevelButton()
    {
        Time.timeScale = 1f;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton()
    {
        Time.timeScale = 1f;
        Debug.LogWarning("Does not work in the UNITY EDITOR!!");
        Application.Quit();
    }

    bool IsEscapePressedThisFrame()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame;
#else
        return Input.GetKeyDown(KeyCode.Escape);
#endif
    }
}
