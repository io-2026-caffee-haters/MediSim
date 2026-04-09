using System;
using System.Collections.Generic;

public class MedicalRecord
{
    private List<MedicalTestResult> _testHistory;
    private List<Symptom> _knownSymptoms;

    public MedicalRecord()
    {
        _testHistory = new List<MedicalTestResult>();
        _knownSymptoms = new List<Symptom>();
    }

    public void AddTestResult(MedicalTestResult result)
    {
        throw new NotImplementedException();
    }

    public List<Symptom> GetKnownSymptoms()
    {
        throw new NotImplementedException();
    }

    public List<MedicalTestResult> GetTestHistory()
    {
        throw new NotImplementedException();
    }

    public bool HasPerformedTest(string testName)
    {
        throw new NotImplementedException();
    }

}