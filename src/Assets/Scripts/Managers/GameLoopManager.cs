using System;

public class GameLoopManager
{
    private readonly PatientManager _patientManager;
    private readonly ScoreTimeManager _scoreTimeManager;
    private readonly IDataRepository _dataRepository;
    private readonly ISaveSystem _saveSystem;

    public GameLoopManager(
        PatientManager patientManager, 
        ScoreTimeManager scoreTimeManager, 
        IDataRepository dataRepository, 
        ISaveSystem saveSystem)
    {
        // _patientManager = patientManager;
        // _scoreTimeManager = scoreTimeManager;
        // _dataRepository = dataRepository;
        // _saveSystem = saveSystem;
    }

    public void StartNewSession()
    {
        throw new NotImplementedException();
    }

    public void StopAndSaveSession()
    {
        throw new NotImplementedException();
    }

    public void ResumeSession()
    {
        throw new NotImplementedException();
    }
}