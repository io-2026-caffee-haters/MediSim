using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class PatientManagerTests
{
    private Disease _flu;
    private Disease _cold;
    private IDataRepository _fakeRepository;

    // 1. Zagnieżdżona klasa Fake (udaje bazę danych na potrzeby testów)
    private class FakeDataRepository : IDataRepository
    {
        private readonly List<Disease> _testDiseases;

        // Przekazujemy listę chorób przez konstruktor Fake'a
        public FakeDataRepository(List<Disease> testDiseases)
        {
            _testDiseases = testDiseases;
        }

        public void LoadStaticData() { }
        public Disease GetDiseaseById(string id) => null;
        public IMedicalTest GetTestById(string id) => null;
        public IEnumerable<IMedicalTest> GetAllTests() => new List<IMedicalTest>();
        
        // Ta metoda zwraca teraz nasze choroby testowe!
        public IEnumerable<Disease> GetAllDiseases() => _testDiseases;
    }

    [SetUp]
    public void SetUp()
    {
        // Przygotowanie danych testowych
        Symptom fever = new Symptom("S_FEVER", "Gorączka", false);
        List<Symptom> fluSymptoms = new List<Symptom> { fever };
        
        _flu = new Disease("D_FLU", "Grypa", fluSymptoms);
        _cold = new Disease("D_COLD", "Przeziębienie", new List<Symptom>());

        // 2. Inicjalizujemy Fake'a naszymi chorobami, żeby PatientManager miał co losować
        _fakeRepository = new FakeDataRepository(new List<Disease> { _flu, _cold });
    }

    [Test]
    public void SpawnNewPatient_ShouldIncreaseCurrentPatientReference()
    {
        // Arrange - wstrzykujemy repozytorium!
        var manager = new PatientManager(_fakeRepository);
        Assert.IsNull(manager.CurrentPatient, "Na początku manager nie powinien mieć pacjenta.");

        // Act
        manager.SpawnNewPatient();

        // Assert
        Assert.IsNotNull(manager.CurrentPatient, "Po spawnowaniu CurrentPatient nie powinien być nullem.");
    }

    [Test]
    public void SpawnNewPatient_ShouldClearPlayerNotes()
    {
        // Arrange
        var manager = new PatientManager(_fakeRepository);
        manager.PlayerNotes = "Stare notatki o poprzednim pacjencie.";

        // Act
        manager.SpawnNewPatient();

        // Assert
        Assert.AreEqual(string.Empty, manager.PlayerNotes, "Notatki powinny zostać zresetowane przy nowym pacjencie.");
    }

    [Test]
    public void EvaluateDiagnosis_ShouldReturnTrue_WhenDiseaseMatches()
    {
        // Arrange
        var manager = new PatientManager(_fakeRepository);
        manager.SpawnNewPatient(); 
        
        var actualDisease = manager.CurrentPatient.ActualDisease;

        // Act
        bool result = manager.EvaluateDiagnosis(actualDisease);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void EvaluateDiagnosis_ShouldReturnFalse_WhenDiseaseIsWrong()
    {
        // Arrange
        var manager = new PatientManager(_fakeRepository);
        manager.SpawnNewPatient();
        
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
        var manager = new PatientManager(_fakeRepository);
        manager.SpawnNewPatient();

        // Act
        var patientFromMethod = manager.CurrentPatient;

        // Assert
        Assert.AreSame(manager.CurrentPatient, patientFromMethod, "Metoda GetCurrentPatient powinna zwracać tę samą referencję co właściwość.");
    }
}