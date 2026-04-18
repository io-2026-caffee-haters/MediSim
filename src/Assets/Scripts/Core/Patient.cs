using System.Collections.Generic;

public class Patient
{
    public Disease ActualDisease { get; }

    public Patient(Disease actualDisease)
    {
        
    }

    public List<Symptom> GetVisibleSymptoms()
    {
        throw new System.NotImplementedException();
    }

    public List<Symptom> GetHiddenSymptoms()
    {
        throw new System.NotImplementedException();
    }
}