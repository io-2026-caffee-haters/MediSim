using System.Collections.Generic;

public class MedicalRecord
{
    private List<string> _performedTestNames;
    private List<Symptom> _knownSymptoms;

    public MedicalRecord()
    {
        // Inicjujemy puste listy, żeby uniknąć błędów NullReferenceException
        _performedTestNames = new List<string>();
        _knownSymptoms = new List<Symptom>();
    }

    public void AddTestResult(MedicalTestResult result)
    {
        // CELOWO PUSTE - Czekamy na fazę GREEN.
        // Nic nie dodajemy do list.
    }

    public List<string> GetPerformedTestNames()
    {
        return _performedTestNames;
    }

    public List<Symptom> GetKnownSymptoms()
    {
        return _knownSymptoms;
    }

    public bool HasPerformedTest(string testName)
    {
        // CELOWY BŁĄD LOGICZNY - wymusi to oblanie testów
        return false;
    }
}