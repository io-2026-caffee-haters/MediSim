using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PatientTests
{

    [Test]
    public void Patient_Initialize_AssignsDataCorrectly()
    {
        GameObject go = new GameObject();
        Patient patient = go.AddComponent<Patient>();
        Disease disease = new Disease { name = "Grypa" };
        List<Symptom> symptoms = new List<Symptom> { new Symptom { name = "Kaszel" } };

        patient.Initialize(disease, symptoms);

        Assert.AreEqual("Grypa", patient.myDisease.name);
        Assert.AreEqual(1, patient.allPatientSymptoms.Count);
    }
    
    [Test]
    public void EvaluateDiagnosis_ReturnsTrue_WhenDiseaseMatches()
    {
        // Arrange
        var disease = new Disease { id = 1, name = "Grypa" };
        var patient = new GameObject().AddComponent<Patient>();
        patient.Initialize(disease, new List<Symptom>());

        // Act
        // Zakładamy, że metoda EvaluateDiagnosis będzie przyjmować obiekt Disease
        bool result = patient.EvaluateDiagnosis(disease.id);

        // Assert
        Assert.IsTrue(result, "Diagnoza powinna być poprawna dla tej samej choroby.");
    }
}