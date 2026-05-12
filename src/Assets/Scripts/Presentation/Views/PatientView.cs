using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI; // Do obsługi obrazków/ikon
using System.IO;

public class PatientView : ScreenView
{
    [Header("Referencje UI Pacjenta")]    
    // Załóżmy, że mamy miejsce (np. tekst) gdzie wypisujemy widoczne objawy
    [SerializeField] private TMP_Text _visibleSymptomsText; 


    [Header("Referencje UI - Przyciski Popupów")]
    [SerializeField] private Button _examineButton;
    [SerializeField] private Button _diagnoseButton;
    [SerializeField] private Button _notesButton;
    [SerializeField] private Button _encyclopediaButton;

    // Zmienne przechowujące referencje do okienek
    private MedicalTestView _medicalTestView;
    private SessionResultView _sessionResultView;
    private NotesView _notesView;
    private EncyclopediaView _encyclopediaView;

    // Wstrzykujemy referencje do popupów z Bootstrappera
    public void InjectPopups(MedicalTestView testView, SessionResultView diagnoseView, NotesView notesView, EncyclopediaView encyclopediaView)
    {
        _medicalTestView = testView;
        _sessionResultView = diagnoseView;
        _notesView = notesView;
        _encyclopediaView = encyclopediaView;

        // Podpinamy przyciski pod metodę z UIManager, używając lambdy '() =>'
        _examineButton.onClick.AddListener(() => _uiManager.ShowPopup(_medicalTestView));
        _diagnoseButton.onClick.AddListener(() => _uiManager.ShowPopup(_sessionResultView));
        _notesButton.onClick.AddListener(() => _uiManager.ShowPopup(_notesView));
        _encyclopediaButton.onClick.AddListener(() => _uiManager.ShowPopup(_encyclopediaView));
    }

    // Wywoływane z zewnątrz (np. z Bootstrappera lub eventu z PatientManager)
    // gdy do gabinetu wchodzi nowy pacjent.
    public void DisplayNewPatient(Patient patient)
    {
        if (patient == null) return;

        // 1. Pobieramy TYLKO widoczne objawy
        List<Symptom> visibleSymptoms = patient.GetVisibleSymptoms();

        // 2. Budujemy tekst z objawami dla UI
        if (visibleSymptoms.Count > 0)
        {
            // Łączymy nazwy widocznych objawów po przecinku
            List<string> symptomNames = new List<string>();
            foreach (var sym in visibleSymptoms)
            {
                symptomNames.Add(sym.Name);
            }
            
            _visibleSymptomsText.text = "Widoczne objawy:\n" + string.Join(", ", symptomNames);
        }
        else
        {
            _visibleSymptomsText.text = "Pacjent nie wykazuje żadnych widocznych objawów.";
        }
    }
}