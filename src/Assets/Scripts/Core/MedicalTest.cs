using System.Collections.Generic;

public class MedicalTest : IMedicalTest
{
    public string Name { get; }
    private float _duration;
    private List<string> _detectableSymptoms;

    public MedicalTest(string name, float duration, List<string> detectableSymptoms)
    {
        // Puste na potrzeby TDD
    }

    public MedicalTestResult PerformOn(Patient patient)
    {
        throw new System.NotImplementedException();
    }
}