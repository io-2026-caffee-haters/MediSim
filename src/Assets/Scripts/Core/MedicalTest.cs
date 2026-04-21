using System.Collections.Generic;
using System.Linq; // Niezbędne do .Where()

public class MedicalTest : IMedicalTest
{
    public string Name { get; }
    public float Duration { get; }
    
    private readonly List<Symptom> _detectableSymptoms;

    public MedicalTest(string name, float duration, List<Symptom> detectableSymptoms)
    {
        Name = name;
        Duration = duration;
        // Zabezpieczenie przed nullem, pusta lista jest bezpieczniejsza niż brak listy
        _detectableSymptoms = detectableSymptoms ?? new List<Symptom>(); 
    }

    public MedicalTestResult PerformOn(Patient patient)
    {
        // Zabezpieczenie przed wywołaniem testu bez pacjenta
        if (patient == null)
        {
            return new MedicalTestResult(Name, new List<Symptom>());
        }

        // 1. Pobieramy wszystkie UKRYTE objawy pacjenta
        var hiddenSymptoms = patient.GetHiddenSymptoms();

        // 2. Filtrujemy je: zostawiamy tylko te, których ID znajduje się na liście _detectableSymptoms
        var detected = hiddenSymptoms
        .Where(hidden => _detectableSymptoms.Any(detectable => detectable.Id == hidden.Id))
        .ToList();

        // 3. Zwracamy wynik
        return new MedicalTestResult(Name, detected);
    }
}