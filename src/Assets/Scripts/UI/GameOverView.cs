using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour
{
    [Header("Referencje UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public Button restartButton;
    public Button exitButton;

    private void Start()
    {
        // Przypięcie akcji do przycisków
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
        
        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);
    }

    /// Otwiera panel i ustawia zdobyte punkty
    public void DisplayResults(int finalScore)
    {
        Debug.Log("Cześć ze środka DisplayResults!");

        if (finalScoreText != null)
            finalScoreText.text = $"Zdobyte punkty: {finalScore}";
        else
            Debug.LogError("GameOverView: Nie przypisano 'finalScoreText' w Inspektorze!");
        
        if (gameOverPanel != null)
        {
            Debug.Log($"[1] Nazwa przypisanego panelu: {gameOverPanel.name}");
            
            gameOverPanel.SetActive(true);
            
            Debug.Log($"[2] Stan po SetActive - activeSelf: {gameOverPanel.activeSelf}");
            Debug.Log($"[3] Stan po SetActive - activeInHierarchy: {gameOverPanel.activeInHierarchy}");
            
            if (gameOverPanel.transform.parent != null)
            {
                Debug.Log($"[4] Rodzic panelu to: {gameOverPanel.transform.parent.name}, czy jest włączony? {gameOverPanel.transform.parent.gameObject.activeInHierarchy}");
            }
        }
        else
            Debug.LogError("GameOverView: Nie przypisano 'gameOverPanel' w Inspektorze!");
    }

    private void RestartGame()
    {
        // Upewniamy się, że gra nie załaduje zapisu
        PlayerPrefs.SetInt("LoadGameFlag", 0);
        PlayerPrefs.Save();
        
        // Ładujemy scenę ponownie (zakładam nazwę "Clinic" zgodnie z wcześniejszym MainMenuView)
        SceneManager.LoadScene("Clinic"); 
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}