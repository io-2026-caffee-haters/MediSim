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
    
}