using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SymptomTests
{
    [Test]
    public void Constructor_AssignsValuesCorrectly()
    {
        // Przygotowanie danych (ARRANGE)
        string expectedId = "s1";
        string expectedName = "Gorączka";
        bool expectedVisibility = false;

        // Wywołanie logiki (ACT)
        Symptom symptom = new Symptom(expectedId, expectedName, expectedVisibility);

        // Sprawdzenie logiki (ASSERT)
        Assert.AreEqual(expectedId, symptom.Id, "Id nie zostało przypisane poprawnie");
        Assert.AreEqual(expectedName, symptom.Name, "Name nie zostało przypisane poprawnie");
        Assert.AreEqual(expectedVisibility, symptom.IsVisibleToNakedEye, "IsVisible nie zostało przypisane poprawnie");
    }

}