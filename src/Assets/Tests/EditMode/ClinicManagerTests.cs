using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ClinicManagerTests
{
    [Test]
    public void PerformMedicalTest_PerformsTest_UpdatesRecord_AndDeductsTime()
    {
        // 1. ARRANGE (Przygotowanie sceny)
        var patientManager = new PatientManager();
        var scoreManager = new ScoreTimeManager();
        var clinicManager = new ClinicManager(patientManager, scoreManager);

        // Symulujemy, że mamy w gabinecie pacjenta i jego pustą kartę
        Disease dummyDisease = new Disease("dis_01", "Katar", new List<Symptom>());
        patientManager.CurrentPatient = new Patient(dummyDisease);
        patientManager.CurrentRecord = new MedicalRecord();

        // Tworzymy nasze "fałszywe" badanie, które trwa 15 minut
        var fakeResult = new MedicalTestResult("Fake Test", new List<Symptom>());
        var fakeTest = new FakeMedicalTest("Fake Test", 15f, fakeResult);

        float initialTime = scoreManager.RemainingTime;

        // 2. ACT (Wykonanie)
        clinicManager.PerformMedicalTest(fakeTest);

        // 3. ASSERT (Sprawdzenie)
        // Sprawdzamy, czy badanie zostało w ogóle uruchomione
        Assert.IsTrue(fakeTest.WasPerformed, "Badanie nie zostało uruchomione na pacjencie (PerformOn).");
        
        // Sprawdzamy komunikację z MedicalRecord
        Assert.IsTrue(patientManager.CurrentRecord.HasPerformedTest("Fake Test"), "Karta pacjenta nie zarejestrowała wykonania testu.");
        
        // Sprawdzamy komunikację ze ScoreTimeManager
        Assert.AreEqual(initialTime - 15f, scoreManager.RemainingTime, "Czas za badanie nie został poprawnie odjęty!");
    }

    // ==========================================
    // KLASA POMOCNICZA TYLKO DLA TESTÓW (Fake)
    // ==========================================
    private class FakeMedicalTest : IMedicalTest
    {
        public string Name { get; private set; }
        public float TimeCost { get; private set; }
        public bool WasPerformed { get; private set; } // Flaga sprawdzająca, czy Manager wywołał tę metodę
        
        private MedicalTestResult _resultToReturn;

        public FakeMedicalTest(string name, float timeCost, MedicalTestResult resultToReturn)
        {
            Name = name;
            TimeCost = timeCost;
            _resultToReturn = resultToReturn;
            WasPerformed = false;
        }

        public MedicalTestResult PerformOn(Patient patient)
        {
            WasPerformed = true;
            return _resultToReturn;
        }
    }
}