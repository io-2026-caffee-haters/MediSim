using System;

public class ScoreTimeManager
{
    public event Action<float> OnTimeChanged;
    public event Action<int> OnScoreChanged;

    public float RemainingTime { get; private set; }
    public int CurrentScore { get; private set; }

    public ScoreTimeManager(float startingTime = 100.0f, int startingScore = 0)
    {
        RemainingTime = startingTime;
        CurrentScore = startingScore;
    }

    public void RemoveTime(float amount)
    {
        RemainingTime = Math.Max(0, RemainingTime - amount);

        OnTimeChanged?.Invoke(RemainingTime);
    }

    public void AddScore(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");
        CurrentScore += amount;

        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void RestoreState(float savedTime, int savedScore)
    {
        RemainingTime = savedTime;
        CurrentScore = savedScore;

        // informujemy słuchaczy
        OnTimeChanged?.Invoke(RemainingTime);
        OnScoreChanged?.Invoke(CurrentScore);
    }
}