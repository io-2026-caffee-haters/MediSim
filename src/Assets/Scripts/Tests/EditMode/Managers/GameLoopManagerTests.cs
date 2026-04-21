using NUnit.Framework;

[TestFixture]
public class GameLoopManagerTests
{
    private PatientManager _patientManager;
    private ScoreTimeManager _scoreTimeManager;
    private SpyDataRepository _spyDataRepository; 
    private SpySaveSystem _spySaveSystem;         
    private GameLoopManager _gameLoop;

    [SetUp]
    public void SetUp()
    {
        _patientManager = new PatientManager();
        _scoreTimeManager = new ScoreTimeManager(100f, 0);
        
        // Używamy "Szpiegów" zamiast prawdziwych systemów plików
        _spyDataRepository = new SpyDataRepository();
        _spySaveSystem = new SpySaveSystem();

        _gameLoop = new GameLoopManager(
            _patientManager, 
            _scoreTimeManager, 
            _spyDataRepository, 
            _spySaveSystem
        );
    }

    [Test]
    public void StartNewSession_ShouldLoadStaticData_AndSpawnPatient()
    {
        // Act
        // Wywołanie rzuci wyjątek w fazie RED
        _gameLoop.StartNewSession();

        // Assert: Sprawdzamy BEHAWIORALNIE (czy GameLoop zarządził odpowiednimi elementami)
        Assert.IsTrue(_spyDataRepository.WasLoadStaticDataCalled, "GameLoop powinien wymusić załadowanie bazy danych na starcie.");
        Assert.IsNotNull(_patientManager.CurrentPatient, "GameLoop powinien nakazać spawnowanie pierwszego pacjenta.");
    }

    [Test]
    public void StopAndSaveSession_ShouldPassCorrectDataToSaveSystem()
    {
        // Arrange
        // Zmuszamy menedżera pacjentów do posiadania konkretnego pacjenta
        _patientManager.SpawnNewPatient();
        
        // Symulujemy stan gry w trakcie sesji
        _scoreTimeManager.AddScore(500);
        _scoreTimeManager.RemoveTime(25f);
        _patientManager.PlayerNotes = "Moje cenne notatki z wywiadu.";

        // Act
        _gameLoop.StopAndSaveSession();

        // Assert
        Assert.IsTrue(_spySaveSystem.WasSaveCalled, "GameLoop powinien wywołać metodę zapisu.");
        
        var capturedData = _spySaveSystem.CapturedSaveData;
        Assert.IsNotNull(capturedData, "GameLoop powinien przekazać obiekt SaveData do systemu zapisu.");
        Assert.AreEqual(500, capturedData.currentScore, "Punkty nie zgadzają się z tymi w ScoreTimeManager.");
        Assert.AreEqual(75f, capturedData.remainingTime, "Czas nie zgadza się z tym w ScoreTimeManager.");
        Assert.AreEqual("Moje cenne notatki z wywiadu.", capturedData.userNotes, "Notatki nie zostały przekazane do zapisu.");
    }

    [Test]
    public void ResumeSession_ShouldNotDoAnything_IfNoSaveFileExists()
    {
        // Arrange
        _spySaveSystem.SetHasSaveFile(false);
        var initialScore = _scoreTimeManager.CurrentScore;

        // Act
        _gameLoop.ResumeSession();

        // Assert
        Assert.IsFalse(_spySaveSystem.WasLoadCalled, "GameLoop nie powinien próbować wczytywać gry, jeśli plik nie istnieje.");
        Assert.AreEqual(initialScore, _scoreTimeManager.CurrentScore, "Stan gry nie powinien się zmienić.");
    }

    [Test]
    public void ResumeSession_ShouldRestoreState_IfSaveFileExists()
    {
        // Arrange
        var simulatedSave = new SaveData 
        { 
            currentScore = 1234, 
            remainingTime = 45f,
            userNotes = "Odzyskane notatki" 
        };
        _spySaveSystem.SetSaveDataToReturn(simulatedSave);

        // Act
        _gameLoop.ResumeSession();

        // Assert
        Assert.IsTrue(_spySaveSystem.WasLoadCalled, "GameLoop powinien wywołać wczytywanie.");
        Assert.AreEqual(1234, _scoreTimeManager.CurrentScore, "Punkty powinny zostać przywrócone.");
        Assert.AreEqual(45f, _scoreTimeManager.RemainingTime, "Czas powinien zostać przywrócony.");
        Assert.AreEqual("Odzyskane notatki", _patientManager.PlayerNotes, "Notatki powinny zostać przywrócone.");
    }

    // ==========================================
    // KLASY POMOCNICZE (Spy / Fakes)
    // ==========================================

    private class SpyDataRepository : IDataRepository
    {
        public bool WasLoadStaticDataCalled { get; private set; }

        public void LoadStaticData()
        {
            WasLoadStaticDataCalled = true;
        }

        public Disease GetDiseaseById(string id) { return null; }
        public IMedicalTest GetTestById(string id) { return null; }
    }

    private class SpySaveSystem : ISaveSystem
    {
        public bool WasSaveCalled { get; private set; }
        public bool WasLoadCalled { get; private set; }
        public SaveData CapturedSaveData { get; private set; }
        
        private bool _hasSaveFile = true;
        private SaveData _dataToReturn = new SaveData();

        public void SetHasSaveFile(bool hasFile) { _hasSaveFile = hasFile; }
        public void SetSaveDataToReturn(SaveData data) { _dataToReturn = data; }

        public void SaveCurrentGame(SaveData data)
        {
            WasSaveCalled = true;
            CapturedSaveData = data;
        }

        public SaveData LoadSavedGame()
        {
            WasLoadCalled = true;
            return _dataToReturn;
        }

        public bool HasSaveFile() => _hasSaveFile;
    }
}