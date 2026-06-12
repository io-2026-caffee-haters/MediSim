using NUnit.Framework;
using System.Collections.Generic;

public class DiseaseTests
{

    [Test]
    public void Disease_StoresDataCorrectly()
    {

        Disease disease = new Disease { id = 99, name = "Testowa", symptomIds = new List<int> { 1, 2, 3 } };

        Assert.AreEqual(99, disease.id);
        Assert.AreEqual("Testowa", disease.name);
        Assert.Contains(2, disease.symptomIds);
    
    }
    
}