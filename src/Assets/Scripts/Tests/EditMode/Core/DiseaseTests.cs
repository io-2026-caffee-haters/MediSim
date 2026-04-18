using System.Collections.Generic;
using NUnit.Framework;

public class DiseaseTests
{
    [Test]
    public void Disease_initialization()
    {
        // ARRANGE
        Symptom symptom1 = new Symptom("sym_01", "Fever", true);
        Symptom symptom2 = new Symptom("sym_02", "Cough", false);
        List<Symptom> symptomsList = new List<Symptom> { symptom1, symptom2 };

        // ACT
        Disease disease = new Disease("dis_01", "Flu", symptomsList);

        // ASSERT
        Assert.AreEqual("dis_01", disease.Id);
        Assert.AreEqual("Flu", disease.Name);
        Assert.IsNotNull(disease.Symptoms);
        Assert.AreEqual(2, disease.Symptoms.Count);
    }
}