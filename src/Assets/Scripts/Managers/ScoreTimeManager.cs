using System;

public class ScoreTimeManager
{
    public float remainingTime { get; private set; }
    public int currentScore { get; private set; }

    public ScoreTimeManager(float startingTime = 100.0f, int startingScore = 0)
    {
        
    }

    public void RemoveTime(float amount)
    {
        throw new NotImplementedException();
    }

    public void AddScore(int amount)
    {
        throw new NotImplementedException();
    }
}