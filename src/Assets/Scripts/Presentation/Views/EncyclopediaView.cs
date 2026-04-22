using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class EncyclopediaView : PopupView
{
    [Header("Lista Chorób (Lewa Strona)")]
    [SerializeField] private Transform _diseaseListContainer;
    [SerializeField] private Button _diseaseButtonPrefab;

    [Header("Szczegóły Choroby (Prawa Strona)")]
    [SerializeField] private TMP_Text _diseaseNameText;
    [SerializeField] private TMP_Text _symptomsText;

    private IDataRepository _dataRepository;

    public void Inject(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
        GenerateDiseaseList();
    }

    private void GenerateDiseaseList()
    {
        var allDiseases = _dataRepository.GetAllDiseases();

        foreach (var disease in allDiseases)
        {
            Button newBtn = Instantiate(_diseaseButtonPrefab, _diseaseListContainer);
            newBtn.GetComponentInChildren<TMP_Text>().text = disease.Name;

            // Używamy lambdy, by po kliknięciu wywołać detale KONKRETNEJ choroby
            newBtn.onClick.AddListener(() => DisplayDiseaseDetails(disease));
        }

        // Domyślny stan przy otwarciu gry
        _diseaseNameText.text = "Wybierz chorobę z listy";
        _symptomsText.text = "Tutaj pojawią się szczegóły dotyczące objawów.";
    }

    private void DisplayDiseaseDetails(Disease disease)
    {
        _diseaseNameText.text = disease.Name;

        if (disease.Symptoms == null || disease.Symptoms.Count == 0)
        {
            _symptomsText.text = "Brak znanych objawów dla tej jednostki chorobowej.";
            return;
        }

        List<string> symptomLines = new List<string>();
        foreach (var sym in disease.Symptoms)
        {
            // Oznaczamy w UI, które objawy widać od razu, a które wymagają testów
            string visibilityTag = sym.IsVisibleToNakedEye ? "<color=#A0FFA0>(Widoczny)</color>" : "<color=#FFA0A0>(Ukryty - Wymaga Badań)</color>";
            symptomLines.Add($"• {sym.Name} {visibilityTag}");
        }

        _symptomsText.text = string.Join("\n", symptomLines);
    }
}