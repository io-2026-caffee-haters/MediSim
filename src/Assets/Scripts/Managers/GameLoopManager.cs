using System;

public class GameLoopManager
{
    private readonly PatientManager _patientManager;
    private readonly ScoreTimeManager _scoreTimeManager;
    private readonly IDataRepository _dataRepository;
    private readonly ISaveSystem _saveSystem;
    private bool _isSessionActive = false;


    public GameLoopManager(
        PatientManager patientManager, 
        ScoreTimeManager scoreTimeManager, 
        IDataRepository dataRepository, 
        ISaveSystem saveSystem)
    {
        _patientManager = patientManager;
        _scoreTimeManager = scoreTimeManager;
        _dataRepository = dataRepository;
        _saveSystem = saveSystem;
    }

    public void StartNewSession()
    {
        // 1. Inicjalizacja bazy danych (np. z plików JSON)
        _dataRepository.LoadStaticData();
        
        // 2. Wygenerowanie pierwszego pacjenta
        _patientManager.SpawnNewPatient();

        _isSessionActive = true;
    }

    public void StopAndSaveSession()
    {
        // 1. Zbieramy aktualne dane ze wszystkich menedżerów
        SaveData dataToSave = new SaveData
        {
            currentScore = _scoreTimeManager.CurrentScore,
            remainingTime = _scoreTimeManager.RemainingTime,
            patientNotes = _patientManager.PlayerNotes,
            
            // Zabezpieczamy się przed nullem przy użyciu operatora '?.' oraz '??'
            currentDiseaseId = _patientManager.CurrentPatient?.ActualDisease?.Id ?? "",
            currentPatientId = "P_001" // Tymczasowe, dopóki pacjent nie ma własnego ID
        };

        // 2. Przekazujemy paczkę danych do systemu zapisu
        _saveSystem.SaveCurrentGame(dataToSave);
        _isSessionActive = false;
    }

    public void ResumeSession()
    {
        // Jeśli plik nie istnieje, przerywamy wczytywanie (zgodnie z testem)
        if (!_saveSystem.HasSaveFile())
        {
            return;
        }

        // 1. Pobieramy dane z dysku
        SaveData loadedData = _saveSystem.LoadSavedGame();

        // 2. Rozsyłamy dane do odpowiednich menedżerów
        _patientManager.PlayerNotes = loadedData.patientNotes;
        _scoreTimeManager.RestoreState(loadedData.remainingTime, loadedData.currentScore);
        
        /* * W przyszłości (gdy dodamy logikę ładowania konkretnego pacjenta), 
         * będziemy musieli tu zrobić coś w stylu:
         * * Disease savedDisease = _dataRepository.GetDiseaseById(loadedData.currentDiseaseId);
         * _patientManager.LoadPatient(loadedData.currentPatientId, savedDisease);
         */
        _isSessionActive = true;
    }

    /// Metoda wywoływana co klatkę przez GameBootstrapper.
    /// </summary>
    public void Tick(float deltaTime)
    {
        // 1. Zabezpieczenie: Czas ucieka TYLKO, gdy gra jest aktywna 
        // (nie chcemy, by czas leciał np. gdy gracz jest w Main Menu)
        if (!_isSessionActive) return;

        // 2. Odejmujemy ułamek sekundy od głównego licznika
        _scoreTimeManager.RemoveTime(deltaTime);

        // 3. Sprawdzamy, czy czas właśnie się skończył
        if (_scoreTimeManager.RemainingTime <= 0)
        {
            EndSessionDueToTimeOut();
        }
    }

    private void EndSessionDueToTimeOut()
    {
        _isSessionActive = false;
        
        // Na ten moment dajemy prosty komunikat do konsoli.
        // Docelowo możesz tu np. wyrzucić pacjenta z gabinetu albo wymusić otwarcie okna diagnozy!
        UnityEngine.Debug.Log("<color=orange>Czas minął! Pacjent opuszcza gabinet.</color>");
    }
}