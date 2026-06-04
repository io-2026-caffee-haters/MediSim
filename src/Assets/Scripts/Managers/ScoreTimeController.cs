using UnityEngine;

public class ScoreTimeController : MonoBehaviour
{
    [Header("Referencje")]
    public ScoreTimeView scoreTimeView;

    // Zmienna blokująca wielokrotne wywołanie Game Over
    private bool _isGameOver = false;
    private ScoreTimeManager _scoreTimeManager;

    void Start()
    {
        // Tworzymy nową instancję managera z czasem początkowym np. 100 sekund
        _scoreTimeManager = new ScoreTimeManager(100f, 0);

        // Upewniamy się, że UI jest zaktualizowane na start
        if (scoreTimeView != null)
        {
            scoreTimeView.RefreshView(_scoreTimeManager.RemainingTime, _scoreTimeManager.CurrentScore);
        }
    }

    void Update()
    {
        // Jeśli gra się skończyła, natychmiast przerywamy Update
        if (_isGameOver) return;

        // Zatrzymujemy odliczanie, gdy czas spadnie do 0
        if (_scoreTimeManager != null)
        {
            // Sprawdzamy czy gra dobiegła końca
            if (_scoreTimeManager.RemainingTime <= 0)
            {
                TriggerGameOver();
            }

            // Odejmujemy czas, który upłynął od ostatniej klatki (Time.deltaTime)
            _scoreTimeManager.RemoveTime(Time.deltaTime);

            // Wysyłamy zaktualizowany czas do widoku
            if (scoreTimeView != null)
            {
                scoreTimeView.UpdateTime(_scoreTimeManager.RemainingTime);
            }
        }
    }

    private void TriggerGameOver()
    {
        // Zamykamy kłódkę - Update() już tu nie wejdzie
        _isGameOver = true;
    
        Debug.Log("Czas minął! Odpalam GameOverView.");

        // === TUTAJ ODPALASZ SWÓJ WIDOK/SCENĘ ===

        // _uiManager.OpenView(gameOverViewPanel);
    }

    /// <summary>
    /// Metoda, którą będziemy wywoływać z DiagnosisView po udanej diagnozie.
    /// </summary>
    public void AddPoints(int points)
    {
        if (_scoreTimeManager != null)
        {
            _scoreTimeManager.AddScore(points);
            
            // Odświeżamy tylko punkty w UI
            if (scoreTimeView != null)
            {
                scoreTimeView.UpdateScore(_scoreTimeManager.CurrentScore);
            }
        }
    }

    public void DeductTimeCost(int cost)
    {
        if (_scoreTimeManager != null && cost > 0)
        {
            _scoreTimeManager.RemoveTime((float)cost);
            
            if (scoreTimeView != null)
            {
                scoreTimeView.UpdateTime(_scoreTimeManager.RemainingTime);
            }
            
            Debug.Log($"<color=orange>ODJĘTO {cost} | ZOSTAŁO {_scoreTimeManager.RemainingTime}s.</color>");
        }
    }

    public int GetCurrentScore() => _scoreTimeManager != null ? _scoreTimeManager.CurrentScore : 0;

    public float GetRemainingTime() => _scoreTimeManager != null ? _scoreTimeManager.RemainingTime : 0f;

    public void RestoreState(float time, int score)
    {
        _scoreTimeManager = new ScoreTimeManager(time, score);
        if (scoreTimeView != null)
        {
            scoreTimeView.RefreshView(time, score);
        }
    }

}