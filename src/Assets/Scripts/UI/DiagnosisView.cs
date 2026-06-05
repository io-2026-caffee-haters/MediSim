using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiagnosisView : MonoBehaviour 
{
    [Header("Referencje do obiektów")]
    public DatabaseManager databaseManager; 
    public GameObject diagnosisPanel; 
    public Transform contentContainer;
    public GameObject diseaseButtonPrefab;
    public DiagnosisResultView diagnosisResultView;
    public ScoreTimeController scoreTimeController;

    /// <summary>
    /// Metoda do przypięcia pod przycisk "Postaw Diagnozę" na głównym ekranie.
    /// </summary>
    public void OpenPanel()
    {
        diagnosisPanel.SetActive(true);
        PopulateDiseaseList();
    }

    /// <summary>
    /// Metoda zamykająca panel. Możesz przypiąć do przycisku "X" lub "Anuluj".
    /// </summary>
    public void ClosePanel()
    {
        diagnosisPanel.SetActive(false);
    }

    /// <summary>
    /// Generuje listę przycisków na podstawie wczytanych chorób.
    /// </summary>
    private void PopulateDiseaseList()
    {
        // 1. Sprawdzamy czy w ogóle mamy dostęp do bazy i czy nie jest pusta
        if (databaseManager == null)
        {
            Debug.LogError("DiagnosisView: Nie podpięto DatabaseManager w Inspektorze!");
            return;
        }

        if (databaseManager.diseasesList == null || databaseManager.diseasesList.Count == 0)
        {
            Debug.LogWarning("DiagnosisView: Lista chorób jest pusta lub null! Sprawdź czy JSON się wczytał.");
            return;
        }

        Debug.Log($"DiagnosisView: Generuję przyciski. Znaleziono chorób: {databaseManager.diseasesList.Count}");

        // 2. Czyścimy starą zawartość
        foreach (Transform child in contentContainer)
        {
            Destroy(child.gameObject);
        }

        // 3. Iterujemy po chorobach
        foreach (Disease disease in databaseManager.diseasesList)
        {
            // WAŻNE: Dodano 'false' jako trzeci parametr. Zmusza to Unity do zachowania lokalnej skali przycisku (np. 1, 1, 1).
            GameObject btnObj = Instantiate(diseaseButtonPrefab, contentContainer, false);
            
            TextMeshProUGUI btnText = btnObj.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null)
            {
                btnText.text = disease.name;
            }

            Button btn = btnObj.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() => SubmitDiagnosis(disease));
            }
        }
    }

    /// <summary>
    /// Sprawdza, czy wybrana przez gracza choroba zgadza się z chorobą pacjenta.
    /// </summary>
    private void SubmitDiagnosis(Disease chosenDisease)
    {
        // Pobieramy referencję do aktualnie badanego pacjenta z menedżera testów 
        MedicalTestManager testManager = Object.FindFirstObjectByType<MedicalTestManager>();
        
        if (testManager == null || testManager.currentActivePatient == null)
        {
            Debug.LogWarning("DiagnosisView: Brak aktywnego pacjenta do zdiagnozowania!");
            return;
        }

        Patient currentPatient = testManager.currentActivePatient;
        
        // Wywołujemy weryfikację bezpośrednio na obiekcie pacjenta 
        bool isCorrect = currentPatient.EvaluateDiagnosis(chosenDisease);

        if (isCorrect)
        {
            Debug.Log($"<color=green>Zwycięstwo!</color> Poprawna diagnoza: {chosenDisease.name}");
            scoreTimeController.AddPoints(50);
            scoreTimeController.AddTimeCost(50);
        }
        else
        {
            Debug.Log($"<color=red>Błąd!</color> Wybrano złą chorobę: {chosenDisease.name}");
            scoreTimeController.DeductPoints(30);
            scoreTimeController.DeductTimeCost(5);
        }

        // Przekazanie gotowych danych do panelu UI
        if (diagnosisResultView != null)
        {
            diagnosisResultView.DisplayResult(isCorrect, chosenDisease.name, currentPatient.myDisease.name);
        }
        else
        {
            Debug.LogError("DiagnosisView: Nie przypisano 'diagnosisResultView' w Inspektorze!");
        }
        
        // Zamykamy okno po wybraniu opcji
        ClosePanel();

        
    }
}