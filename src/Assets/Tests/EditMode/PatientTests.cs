using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PatientTests
{
    [Test]
    public void Constructor_AssignsDiseaseCorrectly()
    {
        // ARRANGE
        Disease mockDisease = new Disease("dis_01", "Przeziębienie", new List<Symptom>());

        // ACT
        Patient patient = new Patient(mockDisease);

        // ASSERT
        Assert.AreEqual(mockDisease, patient.ActualDisease, "Choroba nie została poprawnie przypisana pacjentowi.");
    }

    [Test]
    public void GetVisibleSymptoms_ReturnsOnlyVisibleSymptoms()
    {
        // ARRANGE
        List<Symptom> visibleSymptoms = new List<Symptom>
        {
            new Symptom("sym_01", "Wysypka", true),
            new Symptom("sym_02", "Kaszel", true)
        };

        List<Symptom> hiddenSymptoms = new List<Symptom>
        {
            new Symptom("sym_03", "Wysokie ciśnienie", false)
        };

        Symptom visibleSymptom1 = new Symptom("sym_01", "Wysypka", true);
        Symptom visibleSymptom2 = new Symptom("sym_02", "Kaszel", true);
        Symptom hiddenSymptom   = new Symptom("sym_03", "Wysokie ciśnienie", false);

        List<Symptom> allSymptoms = visibleSymptoms + hiddenSymptoms;
        Disease disease = new Disease("dis_01", "Choroba X", allSymptoms);
        Patient patient = new Patient(disease);

        // ACT
        List<Symptom> visibleResult = patient.GetVisibleSymptoms();

        // ASSERT
        Assert.AreEqual(visibleSymptoms, visibleResult, "Lista nie zwróciła dokładnie visibleSymptoms ale coś innego.");
    }
}