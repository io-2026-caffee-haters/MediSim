using NUnit.Framework;
using System.Collections.Generic;

public class MedicalRecordTests
{
    private MedicalRecord _medicalRecord;
    private Symptom _cough;
    private Symptom _fever;

    [SetUp]
    public void Setup()
    {
        _medicalRecord = new MedicalRecord();
        _cough = new Symptom("sym_cough", "Cough", true);
        _fever = new Symptom("sym_fever", "Fever", true);
    }

    [Test]
    public void AddTestResult_StoresResultInHistory()
    {
        // Arrange
        var result = new MedicalTestResult("Basic Checkup", new List<Symptom> { _cough });

        // Act
        _medicalRecord.AddTestResult(result);
        var history = _medicalRecord.GetTestHistory();

        // Assert
        Assert.AreEqual(1, history.Count);
        Assert.AreEqual("Basic Checkup", history[0].TestName);
    }

    [Test]
    public void AddTestResult_AggregatesNewSymptomsWithoutDuplicates()
    {
        // Arrange
        var test1 = new MedicalTestResult("Test 1", new List<Symptom> { _cough });
        var test2 = new MedicalTestResult("Test 2", new List<Symptom> { _cough, _fever });

        // Act
        _medicalRecord.AddTestResult(test1);
        _medicalRecord.AddTestResult(test2);
        
        var knownSymptoms = _medicalRecord.GetKnownSymptoms();

        // Assert
        Assert.AreEqual(2, knownSymptoms.Count);
        Assert.Contains(_cough, knownSymptoms);
        Assert.Contains(_fever, knownSymptoms);
    }
}