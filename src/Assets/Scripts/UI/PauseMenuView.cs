using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : MonoBehaviour
{
    [Header("Referencje UI")]
    public GameObject pausePanel;
    public Button resumeButton;
    public Button saveGameButton;
    public Button exitButton;

    [Header("Referencje logiki")]
    public SaveLoadController saveLoadController;

    // Przechowuje informację, czy gra jest obecnie zapauzowana
    private bool _isPaused = false;

    private void Start()
    {
        // Upewniamy się, że panel jest ukryty na starcie
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Upewniamy się, że czas płynie normalnie
        Time.timeScale = 1f;

        // Podpięcie akcji do przycisków
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        // Podpięcie nowej akcji zapisu
        if (saveGameButton != null)
            saveGameButton.onClick.AddListener(SaveGame);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        _isPaused = true;
        
        if (pausePanel != null)
            pausePanel.SetActive(true);

        // Zatrzymanie czasu w grze
        Time.timeScale = 0f; 
    }

    private void ResumeGame()
    {
        _isPaused = false;
        
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Przywrócenie normalnego biegu czasu
        Time.timeScale = 1f; 
    }

    private void SaveGame()
    {
        if (saveLoadController != null)
        {
            saveLoadController.ExecuteSaveGame();
            Debug.Log("Gra została zapisana z menu pauzy!");
        }
        else
        {
            Debug.Log("PauseMenuView: Brak referencji do SaveLoadController!");
        }
    }

    private void ExitGame()
    {
        // Przywrócenie czasu przed wyjściem
        Time.timeScale = 1f;
        
        Debug.Log("Zamykanie gry z menu pauzy...");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}