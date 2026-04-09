public class ClinicManager
{
    private PatientManager _patientManager;
    private ScoreTimeManager _scoreTimeManager;

    public ClinicManager(PatientManager patientManager, ScoreTimeManager scoreTimeManager)
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