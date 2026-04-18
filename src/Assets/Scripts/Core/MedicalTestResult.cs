using System.Collections.Generic;

public class MedicalTestResult
{
    public string TestName { get; }
    public List<Symptom> DetectedSymptoms { get; }

    public MedicalTestResult(string testName, List<Symptom> detectedSymptoms)
    {
        
    }

    /// Generuje sformatowany tekst podsumowujący znalezione symptomy przy badaniu.
    public string GetSummary()
    {
        throw new System.NotImplementedException();
    }

}