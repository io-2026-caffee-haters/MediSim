using UnityEngine;
using TMPro;

public class NotesView : PopupView
{
    [Header("Referencje Notatnika")]
    [SerializeField] private TMP_InputField _notesInputField;
    
    private PatientManager _patientManager;

    public void Inject(PatientManager patientManager)
    {
        _patientManager = patientManager;
        
        // Podpinamy się pod wbudowany event w TMP_InputField,
        // który odpala się za każdym razem, gdy gracz zmieni tekst.
        _notesInputField.onValueChanged.AddListener(SaveNotes);
    }

    // Nadpisujemy metodę Show z BaseView, żeby przy otwarciu notatnika
    // od razu wczytać to, co gracz napisał wcześniej.
    public override void Show()
    {
        base.Show(); // Pokazuje okienko i blokuje tło
        
        if (_patientManager != null)
        {
            // Wczytujemy aktualne notatki (wyłącza to też event onValueChanged na ułamek sekundy, 
            // żeby nie zapętlić zapisu przy wczytywaniu - SetTextWithoutNotify)
            _notesInputField.SetTextWithoutNotify(_patientManager.PlayerNotes);
        }
    }

    private void SaveNotes(string newText)
    {
        if (_patientManager != null)
        {
            // Zapisujemy od razu do Menedżera Logiki (Warstwa 2)
            _patientManager.PlayerNotes = newText;
        }
    }
}