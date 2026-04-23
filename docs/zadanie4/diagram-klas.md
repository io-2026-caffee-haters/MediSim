```mermaid
---
title: MediSim – Diagram klas
---
classDiagram
    %% Główny manager rozgrywki
    class GameManager {
        -int playerScore
        -float clinicalTime
        -bool isGameActive
        -Patient currentPatient
        -List~MedicalTest~ availableTests
        -Dictionary~MedicalTest, float~ testCooldowns
        +static bool StartNewGameFlag
        +static bool IsTimedMode
        +static SaveData LoadedSaveData
        +void StartNewGame(bool timedMode)
        +void LoadGame(SaveData data)
        +void EvaluateDiagnosis(string diseaseName)
        +void PerformTest(MedicalTest test)
        +float GetRemainingCooldown(MedicalTest test)
        +void AddTime(float amount)
        +void AddScore(int amount)
        -void EndGame()
        -void SpawnNewPatient()
    }

    class DatabaseManager {
        -string savePath
        -List~Disease~ diseasePool
        -List~Symptom~ symptomPool
        -List~MedicalTest~ testPool
        -List~Sprite~ baseSprites
        +void LoadDataFromJSON()
        +void SaveGameState(SaveData data)
        +SaveData LoadGameState()
        +bool DoesSaveExist()
        +Disease GetRandomDisease()
        +Sprite GetRandomBaseSprite()
        +Disease GetDiseaseById(int id)
        +Symptom GetSymptomById(int id)
        +MedicalTest GetTestById(int id)
    }

    class UIManager {
        -List~TestButton~ testButtons
        +void UpdateHUD(float time, int score)
        +void ToggleJournal()
        +void ShowDialogue(string text)
        +void ShowTestResult(string message)
        +void ShowMessage(string message)
        +void ShowGameOver()
        +void UpdateTestButtons()
    }

    class MenuManager {
        +void Awake()
        +void OnNewGameClicked()
        +void OnFreePlayClicked()
        +void OnLoadGameClicked()
        +void OnQuitClicked()
        -void CheckLoadButtonInteractable()
    }

    %% Modele danych
    class Patient {
        -Disease actualDisease
        -SpriteRenderer spriteRenderer
        -List~SpriteRenderer~ overlayRenderers
        +void Initialize(Disease disease)
        +string GetInterviewText()
        +List~Symptom~ GetSymptomsDetectableByTest(MedicalTest test)
        -void ApplyOverlays(DatabaseManager db)
        -void ClearOverlays()
    }

    class Disease {
        +int id
        +string nazwa
        +List~int~ symptomIds
        +List~Symptom~ GetSymptoms(DatabaseManager db)
    }

    class Symptom {
        +int id
        +string nazwa
        +string opis
        +List~int~ wykrywanyPrzezBadania
        +string interviewLine
        +bool widocznyNaWygladzie
        +string overlayPath
    }

    class MedicalTest {
        +int id
        +string nazwa
        +float timeCost
        +float cooldown
        +string Execute(Patient patient, DatabaseManager db)
    }

    class Journal {
        -List~Disease~ allDiseases
        -List~Symptom~ allSymptoms
        -string userNotes
        +void UpdateNote(string text)
        +void RefreshFromDatabase(DatabaseManager db)
    }

    class SaveData {
        +int savedScore
        +float savedTime
        +string currentPatientDiseaseId
        +string playerNotes
        +Dictionary~int, float~ testCooldowns
    }

    %% Relacje
    GameManager --> DatabaseManager : używa do pobierania danych / zapisu
    GameManager --> UIManager : aktualizuje interfejs, odbiera komunikaty
    GameManager o-- Patient : zarządza bieżącym pacjentem
    GameManager --> MedicalTest : wywołuje Execute
    GameManager *-- Journal : posiada

    DatabaseManager ..> SaveData : serializuje/deserializuje JSON
    DatabaseManager ..> Disease : ładuje z diseases.json
    DatabaseManager ..> Symptom : ładuje z symptoms.json
    DatabaseManager ..> MedicalTest : ładuje z medicalTests.json
    DatabaseManager ..> Patient : dostarcza bazowy sprite

    Patient --> Disease : cierpi na (jedna choroba)
    Patient --> Symptom : używa do nakładek
    Disease o-- Symptom : posiada zestaw (przez symptomIds)
    MedicalTest ..> Symptom : może wykryć (wykrywanyPrzezBadania)

    UIManager --> GameManager : odczytuje cooldowny przez GetRemainingCooldown
    UIManager --> Patient : wyświetla informacje
    UIManager --> Journal : otwiera / aktualizuje

    MenuManager --> GameManager : wywołuje StartNewGame / LoadGame
    MenuManager --> DatabaseManager : sprawdza istnienie zapisu
    MenuManager --> UIManager : przełącza sceny (nie bezpośrednio panele)
classDiagram
    %% ==========================================
    %% WARSTWA 1: DOMENA (Modele i Dane)
    %% ==========================================
    class Symptom {
        +string Id
        +string Name
        +bool IsVisibleToNakedEye
    }

    class Disease {
        +string Id
        +string Name
        +List~Symptom~ Symptoms
    }

    class Patient {
        +Disease ActualDisease
        +GetVisibleSymptoms() List~Symptom~
        +GetHiddenSymptoms() List~Symptom~
    }

    class IMedicalTest {
        <<interface>>
        +string Name
        +float Duration
        +PerformOn(Patient patient) MedicalTestResult
    }

    class MedicalTest {
        -List~Symptom~ _detectableSymptoms
        +string Name
        +float Duration
        +PerformOn(Patient patient) MedicalTestResult
    }

    class MedicalTestResult {
        +string TestName
        +List~Symptom~ DetectedSymptoms
        +GetSummary() string
    }

    Patient --> Disease : choruje na
    Disease o-- Symptom : definiuje objawy
    IMedicalTest ..> MedicalTestResult : zwraca wynik
    MedicalTestResult o-- Symptom : zawiera wykryte
    
    MedicalTest ..|> IMedicalTest 

    %% ==========================================
    %% WARSTWA 2: MECHANIKA GRY ("Mózg")
    %% ==========================================
    class GameLoopManager {
        -bool _isSessionActive
        +event Action OnSessionStarted
        +event Action OnSessionEnded
        +StartNewSession()
        +StopAndSaveSession()
        +ResumeSession()
        +Tick(float deltaTime)
    }

    class PatientManager {
        +event Action~Patient~ OnPatientSpawned
        -Patient currentPatient
        +String playerNotes
        +SpawnNewPatient()
        +RestorePatientState(string diseaseId, string notes)
        +EvaluateDiagnosis(Disease disease) bool
    }

    class MedicalTestManager {
        -ScoreTimeManager _scoreTimeManager
        +PerformMedicalTest(IMedicalTest test, Patient patient)
    }

    class ScoreTimeManager {
        +event Action~float~ OnTimeChanged
        +event Action~int~ OnScoreChanged
        +float RemainingTime
        +int CurrentScore
        +RemoveTime(float amount)
        +AddScore(int amount)
        +RestoreState(float time, int score)
    }

    GameLoopManager --> PatientManager : inicjuje cykl pacjentów
    GameLoopManager --> ScoreTimeManager : zarządza czasem (Tick)
    PatientManager --> Patient : posiada bieżącego
    MedicalTestManager --> IMedicalTest : zleca wykonanie
    MedicalTestManager --> ScoreTimeManager : zmniejsza czas za test

    %% ==========================================
    %% WARSTWA 3: INFRASTRUKTURA I DANE (Implementacje, Pliki i Zapis)
    %% ==========================================
    class IDataRepository {
        <<interface>>
        +LoadStaticData()
        +GetDiseaseById(string id) Disease
        +GetTestById(string id) IMedicalTest
        +GetAllTests() IEnumerable
        +GetAllDiseases() IEnumerable
    }

    class JsonDataRepository {
        -Dictionary _symptoms
        -Dictionary _diseases
        -Dictionary _tests
        +LoadStaticData()
        +GetDiseaseById(string id) Disease
        +GetTestById(string id) IMedicalTest
    }

    class MedicalTestDTO {
        <<DTO>>
        +string id
        +string name
        +float baseDuration
        +List~string~ detectableSymptomIds
    }

    class SymptomDTO {
        <<DTO>>
        +string id
        +string name
        +bool isVisibleToNakedEye
    }

    class DiseaseDTO {
        <<DTO>>
        +string id
        +string name
        +List~string~ symptomIds
    }

    class PatientDTO {
        <<DTO>>
        +string id
        +string name
        +string diseaseId
    }

    class ISaveSystem {
        <<interface>>
        +SaveCurrentGame(SaveData data)
        +LoadSavedGame() SaveData
        +HasSaveFile() bool
    }

    class JsonLocalSaveSystem {
        +SaveCurrentGame(SaveData data)
        +LoadSavedGame() SaveData
        +HasSaveFile() bool
    }

    class SaveData {
        +int currentScore
        +float remainingTime
        +string currentPatientId
        +string currentDiseaseId
        +string patientNotes
    }

    JsonDataRepository ..|> IDataRepository
    JsonLocalSaveSystem ..|> ISaveSystem
    ISaveSystem ..> SaveData : serializuje / deserializuje
    GameLoopManager --> IDataRepository
    GameLoopManager --> ISaveSystem : wysyła dane do zapisu

    %% Repozytorium czyta DTO i tworzy modele
    JsonDataRepository ..> MedicalTestDTO : parsuje
    JsonDataRepository ..> SymptomDTO : parsuje
    JsonDataRepository ..> DiseaseDTO : parsuje
    JsonDataRepository ..> PatientDTO : parsuje
    
    %% ==========================================
    %% WARSTWA 4: PREZENTACJA (Widoki, Interfejs Unity, Bootstrapper)
    %% ==========================================
    
    class UIManager {
        -ScreenView _currentScreen
        -HUDView _currentHUD
        -Stack~PopupView~ _popupStack
        +ShowScreen(ScreenView view)
        +ShowHUD(HUDView view)
        +HideHUD()
        +ShowPopup(PopupView view)
        +CloseCurrentPopup()
    }

    class BaseView {
        <<abstract>>
        +bool isPopup
        #UIManager _uiManager
        #CanvasGroup _canvasGroup
        +Initialize(UIManager manager)
        +Show()
        +Hide()
        +SetInteractable(bool state)
    }

    class ScreenView { <<abstract>> }
    class PopupView { 
        <<abstract>> 
        +bool closeOnOutsideClick
        +OnBackgroundClicked()
    }
    class HUDView { <<abstract>> }

    class MainMenuView {
        -Button _newGameButton
        -Button _continueButton
        -Button _quitButton
        +Inject(GameLoopManager, ISaveSystem)
        -StartNewGame()
        -ContinueGame()
        -QuitGame()
    }

    class PauseMenuView {
        -Button _resumeButton
        -Button _saveAndQuitButton
        +Inject(GameLoopManager)
        -ResumeGame()
        -SaveAndQuit()
    }

    class MedicalTestView {
        -Transform _buttonContainer
        -Button _testButtonPrefab
        -TMP_Text _resultText
        +Inject(MedicalTestManager, PatientManager, IDataRepository)
        -GenerateTestMenu()
        -OnTestButtonClicked(IMedicalTest test)
        -DisplayResult(MedicalTestResult result)
    }

    class PatientView {
        -TMP_Text _patientDescriptionText
        -TMP_Text _visibleSymptomsText
        -Image _patientPortrait
        +DisplayNewPatient(Patient patient)
    }

    class ScoreTimeView {
        -TMP_Text _timeText
        -TMP_Text _scoreText
        +Inject(ScoreTimeManager)
        -UpdateTimeDisplay(float time)
        -UpdateScoreDisplay(int score)
    }

    class SessionResultView {
        -TMP_Dropdown _diseaseDropdown
        -Button _submitButton
        -GameObject _resultPanel
        -TMP_Text _resultText
        -Button _nextPatientButton
        +Inject(PatientManager, GameLoopManager, IDataRepository)
        -EvaluateDiagnosis()
        -NextPatient()
    }

    class NotesView {
        -TMP_InputField _notesInputField
        +Inject(PatientManager)
        -SaveNotes(string text)
    }

    class EncyclopediaView {
        -Transform _diseaseListContainer
        -Button _diseaseButtonPrefab
        -TMP_Text _diseaseNameText
        -TMP_Text _symptomsText
        +Inject(IDataRepository)
        -GenerateDiseaseList()
        -DisplayDiseaseDetails(Disease disease)
    }

    class GameBootstrapper {
        <<Unity Component>>
        -UIManager _uiManager
        -GameLoopManager _gameLoopManager
        -bool _isGamePaused
        +Awake()
        +Update()
        -TogglePause()
    }

    %% Dziedziczenie (Wszystkie widoki to BaseView -> typ docelowy)
    BaseView <|-- ScreenView
    BaseView <|-- PopupView
    BaseView <|-- HUDView

    ScreenView <|-- MainMenuView
    ScreenView <|-- PatientView
    
    PopupView <|-- PauseMenuView
    PopupView <|-- MedicalTestView
    PopupView <|-- SessionResultView
    PopupView <|-- NotesView
    PopupView <|-- EncyclopediaView
    
    HUDView <|-- ScoreTimeView

    %% Relacje zarządzania
    UIManager --> BaseView : zarządza i przechowuje w stosie
    BaseView --> UIManager : prosi o otwarcie/zamknięcie widoków (Command)

    %% Bootstrapper składa wszystko w całość
    GameBootstrapper --> UIManager : inicjuje
    GameBootstrapper --> ISaveSystem : tworzy
    GameBootstrapper --> IDataRepository : tworzy
    GameBootstrapper --> GameLoopManager : wywołuje Tick()

    %% Akcje z UI w dół do Menedżerów (Commands)
    MainMenuView ..> GameLoopManager : uruchamia sesję
    PauseMenuView ..> GameLoopManager : zatrzymuje i zapisuje
    MedicalTestView ..> MedicalTestManager : zleca test
    NotesView ..> PatientManager : zapisuje notatki
    SessionResultView ..> PatientManager : wysyła diagnozę
    EncyclopediaView ..> IDataRepository : czyta bazę

    %% Reakcje UI na zmiany w Menedżerach (Events / Observers)
    ScoreTimeManager ..> ScoreTimeView : [Event] zmiana czasu/punktów
    PatientManager ..> PatientView : [Event] nowy pacjent wygenerowany
```
