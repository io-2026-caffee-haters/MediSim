using System.Collections.Generic;

public class Patient
{
    public Disease ActualDisease { get; private set; }

    public Patient(Disease disease)
    {
        // CELOWO PUSTE - tu musisz przypisać chorobę
    }

    public List<Symptom> GetVisibleSymptoms()
    {
        // CELOWY BŁĄD
        throw new System.NotImplementedException("Metoda GetVisibleSymptoms czeka na implementację.");
    }
}