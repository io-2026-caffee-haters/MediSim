using NUnit.Framework;
using System.Collections.Generic;
using System.Linq; // Wymagane dla metody .Any() w sekcji Assert

[TestFixture]
public class MedicalTestTests
{
    private Patient _patient;

    private Symptom _symptomFever;
    private Symptom _symptomCough;
    private Symptom _symptomVirus;
    private Symptom _symptomBacteria;

    [SetUp]
    public void SetUp()
    {
        // Przygotowanie danych testowych (zakładamy, że te klasy już działają i mają konstruktory)
        _symptomFever = new Symptom("S_FEVER", "Gorączka", false);
        _symptomCough = new Symptom("S_COUGH", "Kaszel", true);
        _symptomVirus = new Symptom("S_VIRUS", "Wirus we krwi", false);
        _symptomBacteria = new Symptom("S_BACTERIA", "Bakteria", false);

        List<Symptom> symptoms = new List<Symptom> { _symptomFever, _symptomCough, _symptomVirus };
        var disease = new Disease("D_FLU", "Grypa", symptoms);

        _patient = new Patient(disease); 
    }

    [Test]
    public void PerformOn_ShouldReturnResultWithCorrectTestName()
    {
        // Arrange
        string expectedName = "Badanie Krwi";
        MedicalTest test = new MedicalTest(expectedName, 15f, new List<Symptom>());

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
        MedicalTest test = new MedicalTest("Test Kompleksowy", 30f, new List<Symptom> { _symptomFever, _symptomCough, _symptomBacteria});

        // Act
        MedicalTestResult result = test.PerformOn(_patient);

        // Assert
        Assert.IsNotNull(result.DetectedSymptoms);
        Assert.AreEqual(1, result.DetectedSymptoms.Count, "Powinno wykryć dokładnie 1 ukryte objawy.");
        Assert.IsTrue(result.DetectedSymptoms.Any(s => s.Id == _symptomFever.Id));
    }

    [Test]
    public void PerformOn_ShouldNotDetectVisibleSymptoms()
    {
        // Arrange
        MedicalTest test = new MedicalTest("Test na kaszel", 10f, new List<Symptom> { _symptomCough });

        // Act
        MedicalTestResult result = test.PerformOn(_patient);

        // Assert
        Assert.IsEmpty(result.DetectedSymptoms, "Test nie powinien zwracać objawów widocznych gołym okiem.");
    }

    [Test]
    public void PerformOn_ShouldReturnEmptyList_WhenPatientHasNoMatchingHiddenSymptoms()
    {
        // Arrange
        MedicalTest test = new MedicalTest("Test na bakterie", 20f, new List<Symptom> { _symptomBacteria});

        // Act
        MedicalTestResult result = test.PerformOn(_patient);

        // Assert (not null but empty)
        Assert.IsNotNull(result.DetectedSymptoms);
        Assert.IsEmpty(result.DetectedSymptoms, "Lista wykrytych objawów powinna być pusta.");
    }
}