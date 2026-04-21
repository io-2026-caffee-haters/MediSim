using System;
using System.Collections.Generic;
using System.Linq; // Wymagane do szybkiego formatowania listy (Select)

public class MedicalTestResult
{
    public string TestName { get; }
    public List<Symptom> DetectedSymptoms { get; }

    public MedicalTestResult(string testName, List<Symptom> detectedSymptoms)
    {
        TestName = testName;
        DetectedSymptoms = detectedSymptoms ?? new List<Symptom>(); // Zabezpieczenie przed nullem
    }

    // Generuje sformatowany tekst podsumowujący znalezione symptomy przy badaniu.
    public string GetSummary()
    {
        // Scenariusz 1: Brak wykrytych objawów
        if (DetectedSymptoms.Count == 0)
        {
            return $"Badanie '{TestName}' nie wykryło żadnych ukrytych objawów.";
        }

        // Scenariusz 2: Test coś znalazł
        // Select(s => s.Name) bierze każdy obiekt Symptom z listy i "wyciąga" z niego tylko właściwość Name.
        // string.Join(", ", ...) skleja te nazwy w jeden tekst, oddzielając je przecinkami.
        string symptomNames = string.Join(", ", DetectedSymptoms.Select(s => s.Name));

        return $"{TestName} wykryło: {symptomNames}";
    }
}