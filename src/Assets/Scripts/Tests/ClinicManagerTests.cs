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

        // Patient to MonoBehaviour, więc tworzymy go przez GameObject
        var patientObject = new GameObject();
        var patient = patientObject.AddComponent<Patient>();

        // ScoreTimeManager to zwykła klasa, więc używamy operatora 'new'
        // UWAGA: Twój konstruktor rzuca NotImplementedException, 
        // więc test wywali się w tym miejscu, dopóki go nie napiszesz.
        var scoreManager = new ScoreTimeManager(100f, 0); 

        var clinicManager = new ClinicManager(patient, scoreManager);

        // POPRAWKA: Disease nie ma konstruktora Disease(string, string, List). 
        // Ustawiamy pola ręcznie.
        Disease dummyDisease = new Disease();
        dummyDisease.id = 1;
        dummyDisease.name = "Katar";
        dummyDisease.symptomIds = new List<int>();

        // POPRAWKA: Patient nie ma właściwości CurrentPatient ani konstruktora.
        // Inicjalizujemy go metodą Initialize, którą masz w Patient.cs.
        patient.Initialize(dummyDisease, new List<Symptom>());

        // POPRAWKA: MedicalTestResult nie ma konstruktora z 2 argumentami.
        var fakeResult = new MedicalTestResult();
        fakeResult.testName = "Fake Test";
        fakeResult.detectedSymptoms = new List<Symptom>();

        var fakeTest = new FakeMedicalTest("Fake Test", 15f, fakeResult);

        // Założenie: ScoreTimeManager ma pole/właściwość RemainingTime
        float initialTime = scoreManager.RemainingTime;

        // 2. ACT (Wykonanie)
        clinicManager.PerformMedicalTest(fakeTest);

        // 3. ASSERT (Sprawdzenie)
        Assert.IsTrue(fakeTest.WasPerformed, "Badanie nie zostało uruchomione.");
        Assert.AreEqual(initialTime - 15f, scoreManager.RemainingTime, "Czas nie został odjęty.");

        // SPRZĄTANIE
        // Usuwamy tylko to, co jest MonoBehaviour/GameObject
        Object.DestroyImmediate(patientObject); 

        // scoreManager to zwykła klasa, nie musisz (i nie możesz) 
        // niszczyć jej przez DestroyImmediate ani używać scoreObject.
    }

    private class FakeMedicalTest : IMedicalTest
    {
        public string Name { get; set; }
        public float TimeCost { get; private set; }
        public bool WasPerformed { get; private set; }
        
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