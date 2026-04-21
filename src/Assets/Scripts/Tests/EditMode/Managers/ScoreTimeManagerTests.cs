using NUnit.Framework;
using System;

public class ScoreTimeManagerTests
{
    private ScoreTimeManager _scoreTimeManager;

    [SetUp]
    public void Setup()
    {
        _scoreTimeManager = new ScoreTimeManager(100f, 0);
    }

    [Test]
    public void Constructor_SetsInitialValuesCorrectly()
    {
        // Act & Assert
        Assert.AreEqual(100f, _scoreTimeManager.RemainingTime);
        Assert.AreEqual(0, _scoreTimeManager.CurrentScore);
    }

    [Test]
    public void RemoveTime_DecreasesRemainingTime()
    {
        // Act
        _scoreTimeManager.RemoveTime(15.5f);

        // Assert
        Assert.AreEqual(84.5f, _scoreTimeManager.RemainingTime);
    }

    [Test]
    public void RemoveTime_DoesNotDropBelowZero()
    {
        // Act
        _scoreTimeManager.RemoveTime(150f); // More than the starting 100f

        // Assert
        Assert.AreEqual(0f, _scoreTimeManager.RemainingTime);
    }

    [Test]
    public void AddScore_IncreasesCurrentScore()
    {
        // Act
        _scoreTimeManager.AddScore(50);
        _scoreTimeManager.AddScore(25);

        // Assert
        Assert.AreEqual(75, _scoreTimeManager.CurrentScore);
    }
    
    [Test]
    public void AddScore_DoesNotAcceptNegativeValues()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _scoreTimeManager.AddScore(-10));
    }
}