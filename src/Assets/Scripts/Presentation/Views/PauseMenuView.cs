using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : PopupView
{
    [Header("Przyciski Pauzy")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _saveAndQuitButton;

    private GameLoopManager _gameLoopManager;

    public void Inject(GameLoopManager gameLoopManager)
    {
        _gameLoopManager = gameLoopManager;

        // Tradycyjnie, podpinamy akcje bezpośrednio z kodu
        _resumeButton.onClick.AddListener(ResumeGame);
        _saveAndQuitButton.onClick.AddListener(SaveAndQuit);
    }

    private void ResumeGame()
    {
        // Nasz UIManager zadba o odblokowanie tła po zamknięciu popupu
        _uiManager.CloseCurrentPopup();
    }

    private void SaveAndQuit()
    {
        // 1. Zlecamy menedżerowi logiki zapisanie stanu gry do pliku JSON
        _gameLoopManager.StopAndSaveSession();
        
        // 2. Tutaj UI nie robi nic więcej! 
        // Twój system jest na tyle mądry, że GameLoopManager powinien 
        // wywołać event (lub Bootstrapper powinien to wychwycić), 
        // aby otworzyć MainMenuView. Gdy to się stanie, UIManager automatycznie
        // usunie ten popup ze stosu dzięki metodzie ClearAllPopups() w ShowScreen().
    }
}