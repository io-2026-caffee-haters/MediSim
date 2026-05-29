using UnityEngine;
using TMPro;

public class ScoreTimeView : MonoBehaviour
{
    [Header("Referencje UI")]
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    /// <summary>
    /// Aktualizuje wyświetlany czas.
    /// </summary>
    public void UpdateTime(float remainingTime)
    {
        if (_timeText != null)
        {
            // Mathf.CeilToInt zaokrągla ułamki w górę (np. 99.1 wyświetli jako 100), 
            // dzięki czemu unikamy migających po przecinku liczb na ekranie.
            _timeText.text = $"Czas: {Mathf.CeilToInt(remainingTime)}";
        }
    }

    /// <summary>
    /// Aktualizuje wyświetlany wynik.
    /// </summary>
    public void UpdateScore(int currentScore)
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"Wynik: {currentScore}";
        }
    }

    /// <summary>
    /// Odświeża oba parametry naraz (przydatne np. podczas inicjalizacji po załadowaniu sceny).
    /// </summary>
    public void RefreshView(float remainingTime, int currentScore)
    {
        UpdateTime(remainingTime);
        UpdateScore(currentScore);
    }
}