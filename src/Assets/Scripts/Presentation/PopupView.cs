using UnityEngine;

public abstract class PopupView : BaseView
{
    // Metoda gotowa do podpięcia pod przycisk "X" lub "Wyjdź"
    public virtual void ClosePopup()
    {
        if (_uiManager != null)
        {
            _uiManager.CloseCurrentPopup();
        }
    }
}