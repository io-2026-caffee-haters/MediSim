using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

[TestFixture]
public class JsonDataRepositoryTests
{
    private JsonDataRepository _repository;

    // Przykładowe JSONy, które symulują zawartość plików w grze.
    // Używamy nakładek {"items": [...]}, co jest wymogiem JsonUtility w Unity.
    private readonly string _symptomsJson = @"{
        ""items"": [
            { ""id"": ""S1"", ""name"": ""Gorączka"", ""isVisibleToNakedEye"": false },
            { ""id"": ""S2"", ""name"": ""Katar"", ""isVisibleToNakedEye"": true }
        ]
    }";

    private readonly string _diseasesJson = @"{
        ""items"": [
            { ""id"": ""D1"", ""name"": ""Grypa"", ""symptomIds"": [""S1"", ""S2""] }
        ]
    }";

    private readonly string _testsJson = @"{
        ""items"": [
            { ""id"": ""T1"", ""name"": ""Badanie Krwi"", ""baseDuration"": 15.0, ""detectableSymptomIds"": [""S1""] }
        ]
    }";

    [SetUp]
    public void SetUp()
    {
        _repository = new JsonDataRepository();
    }

    [Test]
    public void LoadFromJson_ShouldPopulateInternalDictionaries()
    {
        // Act
        Assert.DoesNotThrow(() => _repository.LoadFromJson(_symptomsJson, _diseasesJson, _testsJson));
    }

    [Test]
    public void GetDiseaseById_ShouldReturnCorrectlyAssembledDisease()
    {
        // Arrange
        _repository.LoadFromJson(_symptomsJson, _diseasesJson, _testsJson);

        // Act
        Disease flu = _repository.GetDiseaseById("D1");

        // Assert
        Assert.IsNotNull(flu, "Repozytorium powinno zwrócić chorobę, jeśli istnieje w JSONie.");
        Assert.AreEqual("Grypa", flu.Name);
        
        // Kluczowy test: Czy repozytorium zamieniło ID z DTO na prawdziwe obiekty Symptom?
        Assert.IsNotNull(flu.Symptoms, "Lista objawów nie powinna być null.");
        Assert.AreEqual(2, flu.Symptoms.Count, "Choroba powinna mieć przypięte dwa objawy.");
        Assert.AreEqual("Gorączka", flu.Symptoms[0].Name, "Pierwszy objaw powinien zostać prawidłowo zmapowany.");
    }

    [Test]
    public void GetTestById_ShouldReturnCorrectlyAssembledMedicalTest()
    {
        // Arrange
        _repository.LoadFromJson(_symptomsJson, _diseasesJson, _testsJson);

        // Act
        IMedicalTest bloodTest = _repository.GetTestById("T1");

        // Assert
        Assert.IsNotNull(bloodTest, "Repozytorium powinno zwrócić test, jeśli istnieje w JSONie.");
        Assert.AreEqual("Badanie Krwi", bloodTest.Name);
        Assert.AreEqual(15.0f, bloodTest.Duration, "Czas trwania testu powinien być poprawny.");
    }

    [Test]
    public void GetDiseaseById_ShouldReturnNull_WhenIdDoesNotExist()
    {
        // Arrange
        _repository.LoadFromJson(_symptomsJson, _diseasesJson, _testsJson);

        // Act
        Disease unknown = _repository.GetDiseaseById("UNKNOWN_ID");

        // Assert
        Assert.IsNull(unknown, "Pobranie nieistniejącego ID powinno zwrócić null.");
    }
}