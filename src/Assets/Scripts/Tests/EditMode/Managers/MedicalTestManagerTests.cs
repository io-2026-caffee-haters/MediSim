using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class MedicalTestManagerTests
{
    private ScoreTimeManager _scoreTimeManager;
    private MedicalTestManager _testManager;
    private Patient _patient;
    private MedicalTest _sampleTest;

    [SetUp]
    public void SetUp()
    {
        // 1. Setup Czasu (100 jednostek)
        _scoreTimeManager = new ScoreTimeManager(100f, 0);
        
        // 2. Setup Menedżera
        _testManager = new MedicalTestManager(_scoreTimeManager);

        // 3. Przygotowanie pacjenta (Grypa)
        Symptom s1 = new Symptom("S1", "Gorączka", false);
        Disease d = new Disease("D1", "Grypa", new List<Symptom> { s1 });
        _patient = new Patient(d);

        // 4. Przygotowanie testu (Trwa 20 jednostek czasu, wykrywa S1)
        _sampleTest = new MedicalTest("Badanie Krwi", 20f, new List<string> { "S1" });
    }

    [Test]
    public void PerformMedicalTest_ShouldReturnValidResult()
    {
        // Act
        MedicalTestResult result = _testManager.PerformMedicalTest(_sampleTest, _patient);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Badanie Krwi", result.TestName);
        Assert.AreEqual(1, result.DetectedSymptoms.Count);
    }

    [Test]
    public void PerformMedicalTest_ShouldDeductTimeFromScoreTimeManager()
    {
        // Arrange
        float initialTime = _scoreTimeManager.remainingTime; // 100
        float testDuration = 20f;

        // Act
        _testManager.PerformMedicalTest(_sampleTest, _patient);

        // Assert
        float expectedTime = initialTime - testDuration;
        Assert.AreEqual(expectedTime, _scoreTimeManager.remainingTime, "Czas powinien zostać pomniejszony o czas trwania testu.");
    }

    [Test]
    public void PerformMedicalTest_ShouldWorkWithDifferentTestsAndTimes()
    {
        // Arrange
        var quickTest = new MedicalTest("Szybki test", 5f, new List<string>());
        float initialTime = _scoreTimeManager.remainingTime;

        // Act
        _testManager.PerformMedicalTest(quickTest, _patient);

        // Assert
        Assert.AreEqual(initialTime - 5f, _scoreTimeManager.remainingTime);
    }
}