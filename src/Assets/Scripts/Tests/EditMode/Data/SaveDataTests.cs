using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class SaveDataTests
{
    [Test]
    public void SaveData_ShouldSerializeAndDeserializeCorrectly()
    {
        // Arrange
        var original = new SaveData
        {
            currentScore = 1500,
            remainingTime = 120.5f,
            currentPatientId = "P_002",
            currentDiseaseId = "D_COVID",
            patientNotes = "Pacjent skarży się na ból głowy."
        };

        // Act
        string json = JsonUtility.ToJson(original);
        var deserialized = JsonUtility.FromJson<SaveData>(json);

        // Assert
        Assert.IsNotNull(deserialized);
        Assert.AreEqual(original.currentScore, deserialized.currentScore);
        Assert.AreEqual(original.remainingTime, deserialized.remainingTime);
        Assert.AreEqual(original.currentPatientId, deserialized.currentPatientId);
        Assert.AreEqual(original.currentDiseaseId, deserialized.currentDiseaseId);
        Assert.AreEqual(original.patientNotes, deserialized.patientNotes);
    }
    
    [Test]
    public void SaveData_ShouldHandleEmptyNotesGracefully()
    {
        // Arrange: Czasem gracz nie zrobi notatek, sprawdzamy przypadek brzegowy (null/pusty string)
        var original = new SaveData
        {
            patientNotes = ""
        };

        // Act
        string json = JsonUtility.ToJson(original);
        var deserialized = JsonUtility.FromJson<SaveData>(json);

        // Assert
        Assert.AreEqual("", deserialized.patientNotes);
    }
}