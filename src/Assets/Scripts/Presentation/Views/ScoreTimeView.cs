using UnityEngine;
using TMPro;
using System.IO;

public class ScoreTimeView : HUDView
{
    [Header("Referencje do UI")]
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _scoreText;

    private ScoreTimeManager _scoreTimeManager;

    // Metoda wywoływana przez GameBootstrapper, aby przekazać widokowi odpowiedniego menedżera.
    public void Inject(ScoreTimeManager scoreTimeManager)
    {
        _scoreTimeManager = scoreTimeManager;

        // 1. Podpinamy "słuchawki" pod głośniki menedżera
        _scoreTimeManager.OnTimeChanged += UpdateTimeDisplay;
        _scoreTimeManager.OnScoreChanged += UpdateScoreDisplay;

        // 2. Inicjujemy UI obecnymi wartościami na sam start
        UpdateTimeDisplay(_scoreTimeManager.RemainingTime);
        UpdateScoreDisplay(_scoreTimeManager.CurrentScore);
    }

    private void UpdateTimeDisplay(float currentTime)
    {
        if (_timeText != null)
        {
            // Format "F1" zaokrągla float do jednego miejsca po przecinku (np. "95.5s")
            _timeText.text = $"Czas: {currentTime:F1}s";
        }
    }

    private void UpdateScoreDisplay(int currentScore)
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"Punkty: {currentScore}";
        }
    }

    private void OnDestroy()
    {
        // 3. BARDZO WAŻNE: Odpinamy się, gdy okienko jest niszczone.
        // Jeśli tego nie zrobisz, powstanie wyciek pamięci (Memory Leak).
        if (_scoreTimeManager != null)
        {
            _scoreTimeManager.OnTimeChanged -= UpdateTimeDisplay;
            _scoreTimeManager.OnScoreChanged -= UpdateScoreDisplay;
        }
    }
}