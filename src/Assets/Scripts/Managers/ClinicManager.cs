public class ClinicManager
{
    private Patient _patientManager;
    private ScoreTimeManager _scoreTimeManager;

    public ClinicManager(Patient patientManager, ScoreTimeManager scoreTimeManager)
    {
        _patientManager = patientManager;
        _scoreTimeManager = scoreTimeManager;
    }

    public void PerformMedicalTest(IMedicalTest test)
    {
        // CELOWY BŁĄD
        throw new System.NotImplementedException("ClinicManager jeszcze nie potrafi zlecać badań!");
    }
}