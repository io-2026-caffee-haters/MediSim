using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// Wizualna prezentacja wyników badań w panelach UI (MedicalTestResultPanel).

public class MedicalTestView : MonoBehaviour
{

    public TextMeshProUGUI interviewResult; 
    public GameObject medicalTestPanel;      
    public TextMeshProUGUI medicalTestTitle; 
    public TextMeshProUGUI medicalTestResult;  
    public Button closeButton;         

    /// Subskrypcja zdarzenia zakończenia badania przy aktywacji panelu.
    private void OnEnable()
    {
        MedicalTestManager.OnTestFinished += HandleNewResult;
    }

    /// Odpięcie zdarzenia przy dezaktywacji w celu uniknięcia wycieków pamięci.
    private void OnDisable()
    {
        MedicalTestManager.OnTestFinished -= HandleNewResult;
    }

    void Start()
    {

        /// Ukrycie paneli i ustawienie domyślnych tekstów.
        if (medicalTestPanel != null) medicalTestPanel.SetActive(false);
        
        if (closeButton != null)
            closeButton.onClick.AddListener(() => medicalTestPanel.SetActive(false));
            
        if (interviewResult != null)
            interviewResult.text = "Czekam na wywiad...";
    }

    /// Reaguje na nowy wynik badania i kieruje go do odpowiedniego elementu UI.
    private void HandleNewResult(MedicalTestResult result)
    {

        if (result.testName == "Wywiad lekarski")
        {
            if (interviewResult != null) interviewResult.text = result.GetSummary();
        }
        else
        {
            if (medicalTestPanel != null)
            {
                medicalTestTitle.text = result.testName;
                medicalTestResult.text = result.GetSummary();
                medicalTestPanel.SetActive(true);
            }
        }

    }

}