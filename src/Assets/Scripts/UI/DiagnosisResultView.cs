using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiagnosisResultView : MonoBehaviour
{
    [Header("Główny kontener panelu")]
    public GameObject resultPanel; // Przeciągnij tutaj cały obiekt panelu

    [Header("Referencje UI")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI detailsText;
    public Button nextPatientButton;

    [Header("Referencje do logiki gry")]
    public PatientSpawner patientSpawner;

    /// <summary>
    /// Uruchamia panel, ustawia teksty i modyfikuje statystyki.
    /// </summary>
    public void DisplayResult(bool isCorrect, string chosenDisease, string actualDisease)
    {
        if (resultPanel != null) 
            resultPanel.SetActive(true);

        if (isCorrect)
        {
            titleText.text = "<color=green>Zwycięstwo!</color>";
            detailsText.text = $"Poprawna diagnoza: {chosenDisease}.\nZyskujesz punkty i czas.";
        }
        else
        {
            titleText.text = "<color=red>Błąd!</color>";
            detailsText.text = $"Wybrałeś: {chosenDisease}.\nPoprawna choroba to: {actualDisease}.\nTracisz punkty.";
        }
    }

    /// <summary>
    /// Zamyka sam panel wyników.
    /// </summary>
    public void CloseResultWindow()
    {
        if (resultPanel != null) 
            resultPanel.SetActive(false);
    }

    /// <summary>
    /// Metoda do podpięcia pod przycisk "Następny Pacjent".
    /// </summary>
    public void LoadNextPatient()
    {
        // 1. Zamykamy okno wyników
        CloseResultWindow();

        // 2. Szukamy managera testów, w którym zapisany jest obecny pacjent
        MedicalTestManager testManager = Object.FindFirstObjectByType<MedicalTestManager>();
        
        if (testManager != null && testManager.currentActivePatient != null)
        {
            // Niszczymy fizyczny obiekt "starego" pacjenta na scenie
            Destroy(testManager.currentActivePatient.gameObject);
            
            // Czyścimy pamięć
            testManager.currentActivePatient = null;
        }

        // 3. Wywołujemy PatientSpawner, aby wygenerował nową osobę
        if (patientSpawner != null)
        {
            patientSpawner.SpawnPatient();
        }
        else
        {
            Debug.LogError("DiagnosisResultView: Nie podpięto PatientSpawner w Inspektorze!");
        }
    }
}