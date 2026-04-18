using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class PatientManagerTests
{
    private Disease _flu;
    private Disease _cold;

    [SetUp]
    public void SetUp()
    {
        // Przygotowanie danych testowych zgodnie z Twoim formatem konstruktorów
        Symptom fever = new Symptom("S_FEVER", "Gorączka", false);
        List<Symptom> fluSymptoms = new List<Symptom> { fever };
        
        _flu = new Disease("D_FLU", "Grypa", fluSymptoms);
        _cold = new Disease("D_COLD", "Przeziębienie", new List<Symptom>());
    }

    [Test]
    public void SpawnNewPatient_ShouldIncreaseCurrentPatientReference()
    {
        // Arrange
        var manager = new PatientManager();
        Assert.IsNull(manager.currentPatient, "Na początku manager nie powinien mieć pacjenta.");

        // Act
        manager.SpawnNewPatient();

        // Assert
        // W fazie RED to rzuci NotImplementedException
        Assert.IsNotNull(manager.currentPatient, "Po spawnowaniu currentPatient nie powinien być nullem.");
    }

    [Test]
    public void SpawnNewPatient_ShouldClearPlayerNotes()
    {
        // Arrange
        var manager = new PatientManager();
        manager.playerNotes = "Stare notatki o poprzednim pacjencie.";

        // Act
        manager.SpawnNewPatient();

        // Assert
        Assert.AreEqual(string.Empty, manager.playerNotes, "Notatki powinny zostać zresetowane przy nowym pacjencie.");
    }

    [Test]
    public void EvaluateDiagnosis_ShouldReturnTrue_WhenDiseaseMatches()
    {
        // Arrange
        var manager = new PatientManager();
        manager.SpawnNewPatient(); 
        
        // Pobieramy chorobę, którą manager przypisał pacjentowi wewnątrz SpawnNewPatient
        var actualDisease = manager.currentPatient.ActualDisease;

        // Act
        bool result = manager.EvaluateDiagnosis(actualDisease);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void EvaluateDiagnosis_ShouldReturnFalse_WhenDiseaseIsWrong()
    {
        // Arrange
        var manager = new PatientManager();
        manager.SpawnNewPatient();
        
        // Tworzymy chorobę, której pacjent na pewno nie ma
        var wrongDisease = new Disease("WRONG", "Inna Choroba", new List<Symptom>());

        // Act
        bool result = manager.EvaluateDiagnosis(wrongDisease);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void GetCurrentPatient_ShouldReturnTheSameReferenceAsProperty()
    {
        // Arrange
        var manager = new PatientManager();
        manager.SpawnNewPatient();

        // Act
        var patientFromMethod = manager.GetCurrentPatient();

        // Assert
        Assert.AreSame(manager.currentPatient, patientFromMethod, "Metoda GetCurrentPatient powinna zwracać tę samą referencję co właściwość.");
    }
}