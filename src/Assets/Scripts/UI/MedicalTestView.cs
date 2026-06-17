using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

/// Wizualna prezentacja wyników badań w panelach UI (MedicalTestResultPanel).
public class MedicalTestView : MonoBehaviour
{

    public DatabaseManager databaseManager;
    public MedicalTestManager medicalTestManager;
    public TextMeshProUGUI interviewResult; 
    public GameObject medicalTestPanel;      
    public Button closeButton;         
    public GameObject testButtonPrefab; 
    public Transform buttonsContainer;  
    public TextMeshProUGUI medicalTestTitle; 
    public TextMeshProUGUI medicalTestResult;  

    private void OnEnable()
    {
        MedicalTestManager.OnTestFinished += HandleNewResult;
    }

    private void OnDisable()
    {
        MedicalTestManager.OnTestFinished -= HandleNewResult;
    }

    void Start()
    {

        if (closeButton != null)
        closeButton.onClick.AddListener(() => medicalTestPanel.SetActive(false));

        ClearRightSection();
        GenerateTestButtons();
    }

    public void OpenMedicalTestPanel()
    {
        if (medicalTestPanel == null) return;

        medicalTestPanel.SetActive(true);
        ClearRightSection();
    }

    private void GenerateTestButtons()
    {
        if (buttonsContainer == null)
        {
            return; 
        }

        foreach (Transform child in buttonsContainer)
        {
            Destroy(child.gameObject);
        }

        if (databaseManager == null || databaseManager.medicaltestsList == null) 
        {
            Debug.LogError("MedicalTestView: Brak bazy danych przy próbie generowania przycisków!");
            return;
        }

        foreach (MedicalTest test in databaseManager.medicaltestsList)
        {
            if (test.id == 0) continue;

            GameObject btnObj = Instantiate(testButtonPrefab, buttonsContainer);
            
            TextMeshProUGUI btnText = btnObj.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null) btnText.text = test.name;

            Button btn = btnObj.GetComponent<Button>();
            if (btn != null)
            {
                int capturedId = test.id;
                btn.onClick.AddListener(() => {
                    medicalTestManager.ExecuteTest(capturedId);
                });
            }
        }

        if (buttonsContainer != null)
        {
            RectTransform rect = buttonsContainer.GetComponent<RectTransform>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }
    }

    private void HandleNewResult(MedicalTestResult result)
    {
        if (result.testName == "Wywiad lekarski")
        {
            if (interviewResult != null) interviewResult.text = result.GetSummary();
        }
        else
        {
            if (medicalTestPanel != null && medicalTestPanel.activeSelf)
            {
                if (medicalTestTitle != null) medicalTestTitle.text = result.testName;
                if (medicalTestResult != null) medicalTestResult.text = result.GetSummary();
            }
        }
    }

    private void ClearRightSection()
    {
        if (medicalTestTitle != null) medicalTestTitle.text = "Wybierz badanie";
        if (medicalTestResult != null) medicalTestResult.text = "Wyniki pojawią się tutaj po wybraniu testu laboratoryjnego.";
    }
}