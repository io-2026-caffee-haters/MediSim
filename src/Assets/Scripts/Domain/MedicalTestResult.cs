using System.Collections.Generic;

public class MedicalTestResult
{
    public string TestName { get; private set; }
    public List<Symptom> DetectedSymptoms { get; private set; }

    public MedicalTestResult(string testName, List<Symptom> detectedSymptoms)
    {
        // CELOWO PUSTE - Czekamy na fazę GREEN.
    }

    public string GetSummary()
    {
        // CELOWY BŁĄD - Wymusza oblany test logiczny
        throw new System.NotImplementedException("Metoda GetSummary nie jest jeszcze zaimplementowana.");
    }
}