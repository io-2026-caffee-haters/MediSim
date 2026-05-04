using System.Collections.Generic;

public class UIManager
{
    private ScreenView _currentScreen;
    private HUDView _currentHUD;
    private readonly Stack<PopupView> _popupStack = new Stack<PopupView>();

    // ==========================================
    // ZARZĄDZANIE EKRANAMI
    // ==========================================
    public void ShowScreen(ScreenView screenView)
    {
        if (screenView == null) return;

        ClearAllPopups();

        if (_currentScreen != null)
            _currentScreen.Hide();

        _currentScreen = screenView;
        _currentScreen.Initialize(this);
        _currentScreen.Show();
        _currentScreen.SetInteractable(true);
    }

    // ==========================================
    // ZARZĄDZANIE HUDem
    // ==========================================
    public void ShowHUD(HUDView hudView)
    {
        if (hudView == null) return;

        // Jeśli był jakiś inny HUD, po prostu go chowamy
        if (_currentHUD != null)
        {
            _currentHUD.Hide();
        }

        _currentHUD = hudView;
        _currentHUD.Initialize(this);
        _currentHUD.Show();
    }

    public void HideHUD()
    {
        if (_currentHUD != null)
        {
            _currentHUD.Hide();
            _currentHUD = null; // Czyścimy referencję, ponieważ HUD zostaje ukryty
        }
    }

    // ==========================================
    // ZARZĄDZANIE POPUPAMI
    // ==========================================
    public void ShowPopup(PopupView popupView)
    {
        if (popupView == null) return;

        // Zamrażamy popup pod spodem
        if (_popupStack.Count > 0)
        {
            _popupStack.Peek().SetInteractable(false);
        }

        popupView.Initialize(this);
        _popupStack.Push(popupView);
        popupView.Show();
        popupView.SetInteractable(true);
    }

    public void CloseCurrentPopup()
    {
        if (_popupStack.Count > 0)
        {
            PopupView popupToClose = _popupStack.Pop();
            popupToClose.Hide();

            // Odmrażamy popup pod spodem
            if (_popupStack.Count > 0)
            {
                _popupStack.Peek().SetInteractable(true);
            }
        }
    }

    private void ClearAllPopups()
    {
        while (_popupStack.Count > 0)
        {
            PopupView popup = _popupStack.Pop();
            popup.Hide();
        }
    }
}