using UnityEngine;

// Wymusza na Unity dodanie CanvasGroup do obiektu, jeśli go tam nie ma
[RequireComponent(typeof(CanvasGroup))]
public abstract class BaseView : MonoBehaviour
{
    [Header("Ustawienia Widoku")]
    [Tooltip("Jeśli true, okienko nałoży się na poprzedni widok bez ukrywania go (np. menu pauzy).")]
    public bool isPopup = false;

    // Zmienne protected – ukryte dla innych klas, ale widoczne dla klas dziedziczących (np. MainMenuView)
    protected UIManager _uiManager;
    protected CanvasGroup _canvasGroup;

    // Metoda wywoływana przy pierwszym tworzeniu/podpinaniu widoku.
    // Słówko 'virtual' pozwala klasom potomnym na rozszerzenie tej metody (używając 'override').
    public virtual void Initialize(UIManager manager)
    {
        _uiManager = manager;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    // Wyświetla widok.
    public virtual void Show()
    {
        gameObject.SetActive(true);
        
        // Zabezpieczenie CanvasGroup (żeby przyciski działały i okno było widoczne)
        if (_canvasGroup != null)
        {
            _canvasGroup.alpha = 1f;
            SetInteractable(true);
        }
    }

    // Ukrywa widok.
    public virtual void Hide()
    {
        gameObject.SetActive(false);
        
        // Zabezpieczenie CanvasGroup (okno znika i nie blokuje kliknięć pod spodem)
        if (_canvasGroup != null)
        {
            _canvasGroup.alpha = 0f;
            SetInteractable(false);
        }
    }

    public void SetInteractable(bool state)
    {
        if (_canvasGroup != null)
        {
            _canvasGroup.interactable = state;
            _canvasGroup.blocksRaycasts = state;
        }
    }
}