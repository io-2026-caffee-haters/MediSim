using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MedicalTestTests
{
    
    [Test]
    public void PerformOn_DetectsCorrectSymptoms()
    {

        GameObject go = new GameObject();
        Patient patient = go.AddComponent<Patient>();
        Symptom s1 = new Symptom { id = 1, name = "Gorączka" };
        Symptom s2 = new Symptom { id = 2, name = "Kaszel" };
        patient.allPatientSymptoms = new List<Symptom> { s1, s2 };

        MedicalTest test = new MedicalTest();
        test.name = "Termometr";
        test.detectableSymptomIds = new List<int> { 1 };

        MedicalTestResult result = test.PerformOn(patient);

        Assert.AreEqual(1, result.detectedSymptoms.Count);
        Assert.AreEqual("Gorączka", result.detectedSymptoms[0].name);
    
    }

}