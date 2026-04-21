using System;
using System.Collections.Generic;

public class PatientManager
{
    public Patient CurrentPatient { get; private set; }
    public string PlayerNotes { get; set; } = string.Empty;

    public PatientManager()
    {
    }

    public void SpawnNewPatient()
    {
        Disease dummyDisease = new Disease("D_DUMMY", "Nieznana Choroba", new List<Symptom>());
        CurrentPatient = new Patient(dummyDisease);

        PlayerNotes = string.Empty;
    }

    public bool EvaluateDiagnosis(Disease disease)
    {
        // Edge Cases
        if (CurrentPatient == null || CurrentPatient.ActualDisease == null || disease == null)
        {
            return false;
        }

        return CurrentPatient.ActualDisease.Id == disease.Id;
    }

    public Patient GetCurrentPatient()
    {
        return CurrentPatient;
    }
}