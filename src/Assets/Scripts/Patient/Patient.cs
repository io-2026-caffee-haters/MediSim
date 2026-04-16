using UnityEngine;
using System.Collections.Generic;

/// Reprezentuje pacjenta w grze, przechowuje przypisaną chorobę
/// oraz listę obiektów symptomów, które pacjent aktualnie wykazuje.

public class Patient : MonoBehaviour
{

    /// Choroba wylosowana dla pacjenta.
    public Disease myDisease; 

    /// Pełna lista obiektów symptomów które posiada pacjent.
    public List<Symptom> allPatientSymptoms;

    /// Referencja do bazy.
    private DatabaseManager databaseManager;


    /// Inicjalizuje pacjenta danymi stworzeniu go.
    /// disease: Wylosowana choroba.
    /// symptoms: Lista przetworzonych obiektów symptomów.
    public void Initialize(Disease disease, List<Symptom> symptoms)
    {

        myDisease = disease;
        allPatientSymptoms = symptoms;

        /// Znajduje managera bazy danych na scenie do obsługi wywiadów.
        databaseManager = Object.FindFirstObjectByType<DatabaseManager>();

        Debug.Log($"<color=green>PACJENT UI GOTOWY:</color> {myDisease.name}");

    }

    
    /// Przeprowadza wywiad (testId = 0).
    public void StartInterview()
    {

        if (databaseManager == null) databaseManager = Object.FindFirstObjectByType<DatabaseManager>();

        /// Pobieranie definicji wywadu z bazy danych.
        MedicalTest interviewTest = databaseManager.medicaltestsList.Find(t => t.id == 0);
        if (interviewTest == null)
        {
            Debug.LogError("Nie znaleziono badania 'Wywiad lekarski' w bazie danych");
            return;
        }


        Debug.Log("<color=yellow>WYWIAD LEKARSKI</color>");
        
        bool foundAny = false;
        foreach (var symptom in allPatientSymptoms)
        {
            
            /// Sprawdza czy dany symptom jest na liście wykrywalnych dla wywiadu.
            if (interviewTest.detectableSymptomIds.Contains(symptom.id))
            {
                Debug.Log($"Pacjent: Mam objaw - {symptom.name}");
                foundAny = true;
            }

        }

        if (!foundAny)
        {
            Debug.Log("Pacjent nie zgłasza żadnych objawów w wywiadzie.");
        }

    }

    /// Metoda wypisująca wszystkie symptomy pacjenta.
    // public void ShowAllSymptomsDebug()
    // {
    //     Debug.Log("<color=cyan>--- WSZYSTKIE OBJAWY PACJENTA (DEBUG) ---</color>");
        
    //     if (allPatientSymptoms == null || allPatientSymptoms.Count == 0)
    //     {
    //         Debug.Log("Ten pacjent nie ma przypisanych żadnych objawów.");
    //         return;
    //     }

    //     // Wypisujemy absolutnie wszystko bez sprawdzania medicaltests.json
    //     foreach (var symptom in allPatientSymptoms)
    //     {
    //         string visibility = symptom.isVisible ? "[Widoczny]" : "[Ukryty]";
    //         Debug.Log($"{visibility} Symptom: {symptom.name} (ID: {symptom.id})");
    //     }
    // }


}