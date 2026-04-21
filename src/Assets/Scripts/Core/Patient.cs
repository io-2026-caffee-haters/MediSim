using System.Collections.Generic;
using System.Linq; // Niezbędne do użycia .Where() i .ToList()

public class Patient
{
    public Disease ActualDisease { get; }

    public Patient(Disease actualDisease)
    {
        ActualDisease = actualDisease;
    }

    public List<Symptom> GetVisibleSymptoms()
    {
        // Dobra praktyka: Zabezpieczenie przed błędem NullReferenceException, 
        // gdyby z jakiegoś powodu pacjent nie miał przypisanej choroby lub listy objawów.
        if (ActualDisease?.Symptoms == null) 
            return new List<Symptom>();

        // Zwracamy tylko te objawy, w których IsVisibleToNakedEye to TRUE
        return ActualDisease.Symptoms
            .Where(s => s.IsVisibleToNakedEye)
            .ToList();
    }

    public List<Symptom> GetHiddenSymptoms()
    {
        if (ActualDisease?.Symptoms == null) 
            return new List<Symptom>();

        // Zwracamy tylko te objawy, w których IsVisibleToNakedEye to FALSE
        return ActualDisease.Symptoms
            .Where(s => !s.IsVisibleToNakedEye)
            .ToList();
    }
}