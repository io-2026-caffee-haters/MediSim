using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [Header("Referencje UI - Ekrany (Screens)")]
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private PatientView _patientView;

    [Header("Referencje UI - HUD")]
    [SerializeField] private ScoreTimeView _scoreTimeView;

    [Header("Referencje UI - Popupy (Popups)")]
    [SerializeField] private PauseMenuView _pauseMenuView;
    [SerializeField] private MedicalTestView _medicalTestView;
    [SerializeField] private SessionResultView _sessionResultView;
    [SerializeField] private EncyclopediaView _encyclopediaView;
    [SerializeField] private NotesView _notesView;

    private UIManager _uiManager;
    private GameLoopManager _gameLoopManager;
    private bool _isGamePaused = false;

    private void Awake()
    {
        // ==========================================
        // ETAP 1: INFRASTRUKTURA I DANE
        // ==========================================
        ISaveSystem saveSystem = new JsonLocalSaveSystem();
        IDataRepository dataRepository = new JsonDataRepository();
        dataRepository.LoadStaticData(); // Wczytuje JSONy!

        // ==========================================
        // ETAP 2: MENEDŻERY LOGIKI (Mózg Gry)
        // ==========================================
        // Ustawiamy np. 300 sekund (5 minut) na pacjenta
        ScoreTimeManager scoreTimeManager = new ScoreTimeManager(300f); 
        MedicalTestManager medicalTestManager = new MedicalTestManager(scoreTimeManager);
        PatientManager patientManager = new PatientManager(dataRepository);
        
        _gameLoopManager = new GameLoopManager(patientManager, scoreTimeManager, dataRepository, saveSystem);

        // ==========================================
        // ETAP 3: MENEDŻER UI ORAZ WSTRZYKIWANIE ZALEŻNOŚCI
        // ==========================================
        _uiManager = new UIManager();

        // Przekazujemy odpowiednim oknom tylko to, czego naprawdę potrzebują
        _mainMenuView.Inject(_gameLoopManager, saveSystem);
        _scoreTimeView.Inject(scoreTimeManager);
        _pauseMenuView.Inject(_gameLoopManager);
        _medicalTestView.Inject(medicalTestManager, patientManager, dataRepository);
        _sessionResultView.Inject(patientManager, _gameLoopManager, dataRepository);
        _encyclopediaView.Inject(dataRepository);
        _notesView.Inject(patientManager);

        // ==========================================
        // ETAP 4: PODPINANIE ZDARZEŃ (EVENTS)
        // ==========================================
        // Gdy logika wygeneruje pacjenta, UI automatycznie wyświetli jego dane
        patientManager.OnPatientSpawned += _patientView.DisplayNewPatient;

        // ==========================================
        // ETAP 5: URUCHOMIENIE GRY
        // ==========================================
        // Na sam start otwieramy Menu Główne
        _uiManager.ShowScreen(_mainMenuView);
    }

    private void Update()
    {
        // 1. Obsługa wejścia (Klawisz ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // 2. Hybrydowy system czasu (tyka ZAWSZE, nawet gdy otwarte jest Menu Pauzy)
        if (_gameLoopManager != null)
        {
            _gameLoopManager.Tick(Time.deltaTime);
        }
    }

    private void TogglePause()
    {
        _isGamePaused = !_isGamePaused;
        
        if (_isGamePaused)
        {
            _uiManager.ShowPopup(_pauseMenuView);
        }
        else
        {
            _uiManager.CloseCurrentPopup();
        }
    }
}