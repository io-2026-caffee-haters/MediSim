using System;

public class MedicalTestManager
{
    private readonly ScoreTimeManager _scoreTimeManager;

    public MedicalTestManager(ScoreTimeManager scoreTimeManager)
    {
        _scoreTimeManager = scoreTimeManager;
    }

    public MedicalTestResult PerformMedicalTest(IMedicalTest test, Patient patient)
    {
        if (test == null || patient == null)
        {
            return null; 
        }

        _scoreTimeManager.RemoveTime(test.Duration);

        return test.PerformOn(patient);
    }
}