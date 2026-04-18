using System;

public class PatientManager
{
    public Patient currentPatient { get; }
    public string playerNotes { get; set; }

    public PatientManager()
    {
        
    }

    public void SpawnNewPatient()
    {
        throw new NotImplementedException();
    }

    public bool EvaluateDiagnosis(Disease disease)
    {
        throw new NotImplementedException();
    }

    public Patient GetCurrentPatient()
    {
        throw new NotImplementedException();
    }
}