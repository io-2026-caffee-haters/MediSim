using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI; // Do obsługi obrazków/ikon
using System.IO;

public class PatientView : ScreenView
{
    [Header("Referencje UI Pacjenta")]
    [SerializeField] private TMP_Text _patientDescriptionText;
    
    // Załóżmy, że mamy miejsce (np. tekst) gdzie wypisujemy widoczne objawy
    [SerializeField] private TMP_Text _visibleSymptomsText; 
    
    // (Opcjonalnie) Miejsce na wyświetlenie sprite'a pacjenta
    [SerializeField] private Image _patientPortrait;

    // Wywoływane z zewnątrz (np. z Bootstrappera lub eventu z PatientManager)
    // gdy do gabinetu wchodzi nowy pacjent.
    public void DisplayNewPatient(Patient patient)
    {
        if (patient == null) return;

        // 1. Zwykły opis
        _patientDescriptionText.text = "Nowy pacjent wszedł do gabinetu.";

        // 2. Pobieramy TYLKO widoczne objawy
        List<Symptom> visibleSymptoms = patient.GetVisibleSymptoms();

        // 3. Budujemy tekst z objawami dla UI
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