using System;

public class ScoreTimeManager
{
    public float RemainingTime { get; private set; }
    public int CurrentScore { get; private set; }

    public ScoreTimeManager(float startingTime = 100.0f, int startingScore = 0)
    {
        RemainingTime = startingTime;
        CurrentScore = startingScore;
    }

    public void AddTime(float amount)
    {
        RemainingTime = Math.Max(0f, RemainingTime + amount);
    }

    public void RemoveTime(float amount)
    {
        RemainingTime = Math.Max(0f, RemainingTime - amount);
    }

    public void AddScore(int amount)
    {
        CurrentScore = Math.Max(0, CurrentScore + amount);
    }

    public void RemoveScore(int amount)
    {        
        CurrentScore = Math.Max(0, CurrentScore - amount);
    }

}