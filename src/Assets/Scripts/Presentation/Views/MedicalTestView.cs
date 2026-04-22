using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

// Piękny Data-Driven MedicalTest. Zobacz jak foczka się cieszy!
public class MedicalTestView : PopupView
{
    [Header("Referencje UI")]
    [SerializeField] private TMP_Text _resultText;
    
    [Header("Generator Menu")]
    [SerializeField] private Transform _buttonContainer; // Tu wpadną wygenerowane przyciski (np. obiekt z VerticalLayoutGroup)
    [SerializeField] private Button _testButtonPrefab;   // Prefab pojedynczego przycisku (z tekstem w środku)

    private MedicalTestManager _medicalTestManager;
    private PatientManager _patientManager;
    private IDataRepository _dataRepository;

    public void Inject(MedicalTestManager medicalTestManager, PatientManager patientManager, IDataRepository dataRepository)
    {
        _medicalTestManager = medicalTestManager;
        _patientManager = patientManager;
        _dataRepository = dataRepository;

        // Kiedy tylko okienko zostanie zainicjowane przez Bootstrappera, od razu budujemy menu!
        GenerateTestMenu();
    }

    private void GenerateTestMenu()
    {
        // 1. Pobieramy wszystkie testy z bazy
        var allTests = _dataRepository.GetAllTests();

        // 2. Dla każdego testu tworzymy fizyczny przycisk
        foreach (var test in allTests)
        {
            Button newButton = Instantiate(_testButtonPrefab, _buttonContainer);
            
            // 3. Ustawiamy nazwę na przycisku
            TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = test.Name;

            // 4. Podpinamy obiekt testu prosto do metody OnClick za pomocą lambdy '() =>'
            newButton.onClick.AddListener(() => OnTestButtonClicked(test));
        }
    }

    public override void Show()
    {
        base.Show();
        _resultText.text = "Wybierz badanie, aby zobaczyć wynik.";
    }

    private void OnTestButtonClicked(IMedicalTest testToPerform)
    {
        Patient currentPatient = _patientManager.CurrentPatient;
        if (currentPatient == null)
        {
            _resultText.text = "Błąd: Brak pacjenta w gabinecie!";
            return;
        }

        MedicalTestResult result = _medicalTestManager.PerformMedicalTest(testToPerform, currentPatient);
        DisplayResult(result);
    }

    private void DisplayResult(MedicalTestResult result)
    {
        if (result.DetectedSymptoms.Count == 0)
        {
            _resultText.text = $"Wynik badania '{result.TestName}':\n<color=green>Nie wykryto żadnych anomalii.</color>";
        }
        else
        {
            var symptomNames = result.DetectedSymptoms.Select(s => s.Name);
            _resultText.text = $"Wynik badania '{result.TestName}':\n<color=red>Wykryto: {string.Join(", ", symptomNames)}</color>";
        }
    }
}