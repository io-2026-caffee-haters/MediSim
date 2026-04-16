using NUnit.Framework;

public class SymptomTests
{

    [Test]
    public void Symptom_Properties_WorkCorrectly()
    {
        Symptom symptom = new Symptom { id = 10, name = "Wysypka", isVisible = true };

        Assert.AreEqual(10, symptom.id);
        Assert.IsTrue(symptom.isVisible);
    }
    
}