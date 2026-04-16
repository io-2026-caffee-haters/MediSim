using NUnit.Framework;
using UnityEngine;
using System.IO;

public class SaveSystemTests
{
    private SaveSystem saveSystem;
    private string expectedFilePath;

    [SetUp]
    public void Setup()
    {
        GameObject go = new GameObject("TestSaveSystem");
        saveSystem = go.AddComponent<SaveSystem>();

        expectedFilePath = Path.Combine(Application.persistentDataPath, "savegame.json");

        if (File.Exists(expectedFilePath)) File.Delete(expectedFilePath);
    }

    [Test]
    public void SaveCurrentGame_ShouldCreatePhysicalFile()
    {
        SaveData mySave = new SaveData
        {
            currentScore = 100,
            remainingTime = 120.5f,
            currentDiseaseId = "1"
        };

        saveSystem.SaveCurrentGame(mySave);

        bool fileCreated = File.Exists(expectedFilePath);
        Assert.IsTrue(fileCreated, "Plik zapisu nie został utworzony w expectedFilePath");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(saveSystem.gameObject);
    }
}