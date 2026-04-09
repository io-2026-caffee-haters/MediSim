using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DiseaseTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Constructor_AssignsValuesCorrectly()
    {
        // Przygotowanie danych (ARRANGE)
        string expectedId = "s1";
        string expectedName = "Gorączka";
        List<Symptom> expectedSymptoms = new List<Symptom>
        {
            new Symptom("s1", "Kaszel", true)
        };

        // Wywołanie logiki (ACT)
        Disease disease = new Disease(expectedId, expectedName, expectedSymptoms);

        // Sprawdzenie logiki (ASSERT)
        Assert.AreEqual(expectedId, disease.Id, "Id nie zostało przypisane poprawnie");
        Assert.AreEqual(expectedName, disease.Name, "Name nie zostało przypisane poprawnie");
        Assert.AreEqual(expectedSymptoms, disease.Symptoms, "Symptoms nie zostały przypisane poprawnie");
    }

}
