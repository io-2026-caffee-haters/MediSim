using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SessionResultView : PopupView
{
    [Header("UI - Wybór Diagnozy")]
    [SerializeField] private TMP_Dropdown _diseaseDropdown; // Wybór choroby
    [SerializeField] private Button _submitButton;

    [Header("UI - Wynik")]
    [SerializeField] private GameObject _resultPanel; // Panel pojawiający się PO diagnozie
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private Button _nextPatientButton;

    private PatientManager _patientManager;
    private GameLoopManager _gameLoopManager;
    private List<Disease> _availableDiseases;

    public void Inject(PatientManager patientManager, GameLoopManager gameLoopManager, IDataRepository dataRepository)
    {
        _patientManager = patientManager;
        _gameLoopManager = gameLoopManager;
        
        // Zapisujemy listę, żeby potem wiedzieć, który indeks z Dropdowna to która choroba
        _availableDiseases = dataRepository.GetAllDiseases().ToList();

        PopulateDropdown();

        // Podpinamy przyciski z kodu!
        _submitButton.onClick.AddListener(EvaluateDiagnosis);
        _nextPatientButton.onClick.AddListener(NextPatient);
    }

    public override void Show()
    {
        base.Show();
        // Resetujemy widok za każdym razem, gdy gracz go otwiera
        _resultPanel.SetActive(false);
        _submitButton.interactable = true;
        _diseaseDropdown.interactable = true;
    }

    private void PopulateDropdown()
    {
        _diseaseDropdown.ClearOptions();
        
        // Magia LINQ: Tworzymy opcje do dropdowna bezpośrednio z nazw chorób
        var options = _availableDiseases.Select(d => new TMP_Dropdown.OptionData(d.Name)).ToList();
        _diseaseDropdown.AddOptions(options);
    }

    private void EvaluateDiagnosis()
    {
        // 1. Zablokuj przyciski, żeby gracz nie kliknął dwa razy
        _submitButton.interactable = false;
        _diseaseDropdown.interactable = false;

        // 2. Pobierz chorobę, którą wybrał gracz (indeks z Dropdowna zgadza się z indeksem na liście)
        int selectedIndex = _diseaseDropdown.value;
        Disease selectedDisease = _availableDiseases[selectedIndex];

        // 3. Zapytaj menedżera, czy gracz miał rację
        bool isCorrect = _patientManager.EvaluateDiagnosis(selectedDisease);

        // 4. Pokaż wynik
        _resultPanel.SetActive(true);
        if (isCorrect)
        {
            _resultText.text = $"<color=green>Prawidłowa diagnoza!</color>\nRozpoznano: {selectedDisease.Name}.";
        }
        else
        {
            Disease actualDisease = _patientManager.CurrentPatient.ActualDisease;
            _resultText.text = $"<color=red>Błąd w sztuce!</color>\nTo nie {selectedDisease.Name}.\nPacjent cierpiał na: {actualDisease.Name}.";
        }
    }

    private void NextPatient()
    {
        // Zamykamy ten popup
        _uiManager.CloseCurrentPopup();
        
        // Zlecamy głównemu dyrygentowi wczytanie nowego pacjenta
        _gameLoopManager.StartNewSession(); 
    }
}