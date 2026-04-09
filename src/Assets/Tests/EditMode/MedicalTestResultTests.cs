using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MedicalTestResultTests
{
    [Test]
    public void Constructor_AssignsValuesCorrectly()
    {
        // Przygotowanie danych
        string expectedName = "Badanie Krwi";
        List<Symptom> expectedSymptoms = new List<Symptom>
        {
            new Symptom("s1", "Kaszel", true)
        };

        // Wywołanie logiki
        MedicalTestResult result = new MedicalTestResult(expectedName, expectedSymptoms);

        // Sprawdzenie poprawności konstruktora
        Assert.AreEqual(expectedName, result.TestName, "TestName nie zostało przypisane poprawnie");
        Assert.AreEqual(expectedSymptoms, result.DetectedSymptoms, "DetectedSymptoms nie zostały przypisane poprawnie");
    }

    [Test]
    public void GetSummary_ReturnsFormatedString_WithSymptoms()
    {
        // Przygotowanie danych (ARRANGE)
        string testName = "Wywiad";
        List<Symptom> detectedSymptoms = new List<Symptom>
        {
            new Symptom("s1", "Kaszel", true),
            new Symptom("s2", "Katar", true)
        };
        MedicalTestResult result = new MedicalTestResult(testName, detectedSymptoms);

        // Wywołanie logiki (ACT)
        string summary = result.GetSummary();


        // Sprawdzenie logiki (ASSERT)
        StringAssert.Contains(testName, summary, "Brak nazwy testu w GetSummary()");

        foreach(var symptom in detectedSymptoms)
        {
            StringAssert.Contains(symptom.Name, summary, $"Brak wykrytego symptomu ({symptom.Name}) w GetSummary()");
        }
    }

}
