using System.Collections.Generic;
using NUnit.Framework;

public class MedicalTestResultTests
{
    [Test]
    public void MedicalTestResult_InitializationAndSummary()
    {
        // Arrange
        Symptom s1 = new Symptom("sym_01", "Gorączka", true);
        Symptom s2 = new Symptom("sym_02", "Kaszel", false);
        List<Symptom> symptomsList = new List<Symptom> { s1, s2 };

        // Act
        MedicalTestResult result = new MedicalTestResult("Badanie Krwi", symptomsList);
        string summary = result.GetSummary(); 

        // Assert
        Assert.AreEqual("Badanie Krwi", result.TestName);
        Assert.IsNotNull(result.DetectedSymptoms);
        Assert.AreEqual(2, result.DetectedSymptoms.Count);

        Assert.AreEqual("Badanie Krwi wykryło: Gorączka, Kaszel", summary); // jeszcze zobaczymy jak to zaimplementujemy
    }
}