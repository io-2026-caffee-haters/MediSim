using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SymptomTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Symptom_initialization()
    {
        Symptom symptom = new Symptom("id", "name", true);

        Assert.AreEqual("id", symptom.Id);
        Assert.AreEqual("name", symptom.Name);
        Assert.IsTrue(symptom.IsVisibleToNakedEye);
    }
}
