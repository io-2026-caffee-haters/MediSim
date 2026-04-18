using NUnit.Framework;
using System.IO;
using UnityEngine;

[TestFixture]
public class JsonLocalSaveSystemTests
{
    private JsonLocalSaveSystem _saveSystem;
    private readonly string _testFileName = "test_savegame.json";
    private string _fullPath;

    [SetUp]
    public void SetUp()
    {
        // Unity przechowuje pliki zapisu w persistentDataPath (działa na PC, Androidzie, iOS)
        _fullPath = Path.Combine(Application.persistentDataPath, _testFileName);
        
        // Upewniamy się, że przed testem plik nie istnieje (czysta karta)
        if (File.Exists(_fullPath))
        {
            File.Delete(_fullPath);
        }

        _saveSystem = new JsonLocalSaveSystem(_testFileName);
    }

    [TearDown]
    public void TearDown()
    {
        // Sprzątanie po teście - usuwamy plik testowy, żeby nie śmiecić na dysku
        if (File.Exists(_fullPath))
        {
            File.Delete(_fullPath);
        }
    }

    [Test]
    public void HasSaveFile_ShouldReturnFalse_WhenNoFileExists()
    {
        // Act
        bool hasFile = _saveSystem.HasSaveFile();

        // Assert
        Assert.IsFalse(hasFile, "Powinno zwrócić false, gdy plik nie istnieje.");
    }

    [Test]
    public void SaveCurrentGame_ShouldCreateAFileOnDisk()
    {
        // Arrange
        var dummyData = new SaveData();

        // Act
        _saveSystem.SaveCurrentGame(dummyData);

        // Assert
        Assert.IsTrue(File.Exists(_fullPath), "Metoda Save powinna stworzyć fizyczny plik na dysku.");
    }

    [Test]
    public void HasSaveFile_ShouldReturnTrue_AfterSaving()
    {
        // Arrange
        var dummyData = new SaveData();
        
        // Zabezpieczenie przed błędem z SaveCurrentGame w tym teście
        try { _saveSystem.SaveCurrentGame(dummyData); } catch (System.NotImplementedException) { }
        
        // Na potrzeby fazy RED sztucznie tworzymy plik, żeby przetestować samo HasSaveFile
        if (!File.Exists(_fullPath)) File.WriteAllText(_fullPath, "{}");

        // Act
        bool hasFile = _saveSystem.HasSaveFile();

        // Assert
        Assert.IsTrue(hasFile, "Powinno zwrócić true po utworzeniu pliku zapisu.");
    }

    [Test]
    public void LoadSavedGame_ShouldReturnCorrectData()
    {
        // Arrange
        var originalData = new SaveData
        {
            currentScore = 999,
            remainingTime = 45.5f,
            currentPatientId = "P_005",
            currentDiseaseId = "D_COVID",
            userNotes = "Hello World!"
        };
        
        // Act
        _saveSystem.SaveCurrentGame(originalData);
        var loadedData = _saveSystem.LoadSavedGame();

        // Assert
        Assert.IsNotNull(loadedData, "Wczytane dane nie powinny być nullem.");
        Assert.AreEqual(999, loadedData.currentScore);
        Assert.AreEqual(45.5f, loadedData.remainingTime);
        Assert.AreEqual("P_005", loadedData.currentPatientId);
        Assert.AreEqual("D_COVID", loadedData.currentDiseaseId);
        Assert.AreEqual("Hello World!", loadedData.userNotes);
    }

    [Test]
    public void LoadSavedGame_ShouldReturnNull_WhenNoFileExists()
    {
        // Act
        var loadedData = _saveSystem.LoadSavedGame();

        // Assert
        // Zabezpieczenie przed crashem gry, gdy gracz wciśnie "Load", a nie ma zapisu.
        Assert.IsNull(loadedData, "Próba wczytania nieistniejącego zapisu powinna zwrócić null.");
    }
}