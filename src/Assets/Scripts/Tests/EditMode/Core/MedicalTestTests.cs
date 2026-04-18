using NUnit.Framework;
using System.Collections.Generic;
using System.Linq; // Wymagane dla metody .Any() w sekcji Assert

[TestFixture]
public class MedicalTestTests
{
    private Patient _patient;

    [SetUp]
    public void SetUp()
    {
        // Przygotowanie danych testowych (zakładamy, że te klasy już działają i mają konstruktory)
        var symptomFever = new Symptom("S_FEVER", "Gorączka", false);
        var symptomCough = new Symptom("S_COUGH", "Kaszel", true);
        var symptomVirus = new Symptom("S_VIRUS", "Wirus we krwi", false);

        List<Symptom> symptoms = new List<Symptom> { symptomFever, symptomCough, symptomVirus };

        var disease = new Disease("D_FLU", "Grypa", symptoms);

        var _patient = new Patient(disease); 
    }

    [Test]
    public void PerformOn_ShouldReturnResultWithCorrectTestName()
    {
        // Arrange
        string expectedName = "Badanie Krwi";
        MedicalTest test = new MedicalTest(expectedName, 15f, new List<string>());

        // Act
        MedicalTestResult result = test.PerformOn(_patient);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedName, result.TestName);
    }

    [Test]
    public void PerformOn_ShouldDetectOnlyHiddenSymptomsThatAreInDetectableList()
    {
        // Arrange
        MedicalTest test = new MedicalTest("Test Kompleksowy", 30f, new List<string> { "S_FEVER", "S_VIRUS", "S_UNKNOWN" });

        // Act
        MedicalTestResult result = test.PerformOn(_patient);

        // Assert
        Assert.IsNotNull(result.DetectedSymptoms);
        Assert.AreEqual(2, result.DetectedSymptoms.Count, "Powinno wykryć dokładnie 2 ukryte objawy.");
        Assert.IsTrue(result.DetectedSymptoms.Any(s => s.Id == "S_FEVER"));
        Assert.IsTrue(result.DetectedSymptoms.Any(s => s.Id == "S_VIRUS"));
    }

    [Test]
    public void PerformOn_ShouldNotDetectVisibleSymptoms()
    {
        // Arrange
        MedicalTest test = new MedicalTest("Test na kaszel", 10f, new List<string> { "S_COUGH" });

        // Act
        MedicalTestResult result = test.PerformOn(_patient);

        // Assert
        Assert.IsEmpty(result.DetectedSymptoms, "Test nie powinien zwracać objawów widocznych gołym okiem.");
    }

    [Test]
    public void PerformOn_ShouldReturnEmptyList_WhenPatientHasNoMatchingHiddenSymptoms()
    {
        // Arrange
        MedicalTest test = new MedicalTest("Test na bakterie", 20f, new List<string> { "S_BACTERIA", "S_PARASITE" });

        // Act
        MedicalTestResult result = test.PerformOn(_patient);

        // Assert (not null but empty)
        Assert.IsNotNull(result.DetectedSymptoms);
        Assert.IsEmpty(result.DetectedSymptoms, "Lista wykrytych objawów powinna być pusta.");
    }
}