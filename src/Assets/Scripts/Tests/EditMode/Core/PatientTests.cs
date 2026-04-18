using System.Collections.Generic;
using NUnit.Framework;

public class PatientTests
{
    [Test]
    public void Patient_FiltersVisibleAndHiddenSymptomsCorrectly()
    {
        // Arrange: Tworzymy 3 symptomy (1 widoczny, 2 ukryte)
        Symptom visibleSymptom = new Symptom("sym_01", "Wysypka", true);
        Symptom hiddenSymptom1 = new Symptom("sym_02", "Wysokie CRP", false);
        Symptom hiddenSymptom2 = new Symptom("sym_03", "Niskie żelazo", false);
        
        // Arrange: Tworzymy testową chorobę i ładujemy do niej symptomy
        List<Symptom> diseaseSymptoms = new List<Symptom> { visibleSymptom, hiddenSymptom1, hiddenSymptom2 };
        Disease testDisease = new Disease("dis_test", "Choroba Testowa", diseaseSymptoms);

        // Act: Tworzymy pacjenta i każemy mu przefiltrować objawy
        Patient patient = new Patient(testDisease);
        List<Symptom> visibleResult = patient.GetVisibleSymptoms();
        List<Symptom> hiddenResult = patient.GetHiddenSymptoms();

        // Assert: Sprawdzamy przypisanie choroby
        Assert.AreEqual(testDisease, patient.ActualDisease);

        // Assert: Sprawdzamy, czy pacjent dobrze rozdzielił objawy
        Assert.IsNotNull(visibleResult);
        Assert.AreEqual(1, visibleResult.Count); // Oczekujemy tylko 1 widocznego
        Assert.AreEqual("Wysypka", visibleResult[0].Name);

        Assert.IsNotNull(hiddenResult);
        Assert.AreEqual(2, hiddenResult.Count); // Oczekujemy 2 ukrytych
    }
}