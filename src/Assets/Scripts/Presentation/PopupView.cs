using UnityEngine;

public abstract class PopupView : BaseView
{
    [Header("Ustawienia Popupu")]
    [Tooltip("Czy gracz może zamknąć to okno klikając w tło obok niego?")]
    public bool closeOnOutsideClick = true;

    // Metoda gotowa do podpięcia pod niewidzialny przycisk w tle popupu w Unity
    public void OnBackgroundClicked()
    {
        if (closeOnOutsideClick && _uiManager != null)
        {
            _uiManager.CloseCurrentPopup();
        }
    }
}