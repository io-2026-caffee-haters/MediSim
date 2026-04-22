using UnityEngine;
using UnityEngine.UI; // Wymagane do obsługi przycisków

public class MainMenuView : ScreenView
{
    [Header("Przyciski Menu")]
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _quitButton;

    private GameLoopManager _gameLoopManager;
    private ISaveSystem _saveSystem;

    // Bootstrapper wstrzykuje tu menedżera pętli gry (do uruchamiania) i system zapisu (do sprawdzania stanu).
    public void Inject(GameLoopManager gameLoopManager, ISaveSystem saveSystem)
    {
        _gameLoopManager = gameLoopManager;
        _saveSystem = saveSystem;

        // Podpinamy akcje pod przyciski prosto z kodu!
        _newGameButton.onClick.AddListener(StartNewGame);
        _continueButton.onClick.AddListener(ContinueGame);
        _quitButton.onClick.AddListener(QuitGame);
    }

    // Nadpisujemy metodę Show, aby za każdym razem, gdy wracamy do menu głównego,
    // gra od nowa sprawdzała, czy pojawił się plik zapisu.
    public override void Show()
    {
        base.Show();
        RefreshContinueButton();
    }

    private void RefreshContinueButton()
    {
        if (_continueButton != null && _saveSystem != null)
        {
            // Jeśli HasSaveFile() zwróci false, przycisk będzie szary i nieklikalny
            _continueButton.interactable = _saveSystem.HasSaveFile();
        }
    }

    private void StartNewGame()
    {
        // Wyłączamy klikalność na ułamek sekundy, żeby gracz nie "zaklikał" przycisku
        SetInteractable(false); 
        
        // Zlecamy Menedżerowi rozpoczęcie nowej, czystej sesji
        _gameLoopManager.StartNewSession();
    }

    private void ContinueGame()
    {
        SetInteractable(false);
        
        // Zlecamy Menedżerowi wczytanie gry z JSONa
        _gameLoopManager.ResumeSession();
    }

    private void QuitGame()
    {
        // Debug.Log jest super ważne, bo Application.Quit() nie zamyka edytora Unity!
        // Działa dopiero po zbudowaniu gry do pliku .exe / .apk.
        Debug.Log("Zamykanie aplikacji...");
        Application.Quit();
    }
}