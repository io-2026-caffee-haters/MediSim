using UnityEngine;
using System;

/// Zarządza wykonywaniem badania medycznego.
/// Łączy dane z DatabaseManager'a z konkretym obiektem pacjenta.

public class MedicalTestManager : MonoBehaviour
{

    /// Referencja do managera bazy danych.
    public DatabaseManager databaseManager;

    /// Definicja zdarzenia na którym UI musi się zapisać.
    public static event Action<MedicalTestResult> OnTestFinished;

    /// Referencja do obecnego pacjenta (ukryta dla poprawnego działania funkcji).
    [HideInInspector] public Patient currentActivePatient;

    /// Pobiera definicje badania po ID i wykonuje na pacjencie.
    /// testId: ID badania z pliku medicaltests.json.
    /// currentPatient: Obiekt pacjenta obecnie badanego.
    public void ExecuteTest(int testId)
    {
        
        if (currentActivePatient == null)
        {
            Debug.LogWarning("MedicalTestManager: Brak aktywnego pacjenta.");
            return;
        }

        /// Znajduje dane badania z bazy.
        MedicalTest test = databaseManager.medicaltestsList.Find(t => t.id == testId);

        /// Sprawdza czy badanie i pacjent istnieje.
        if (test == null || currentActivePatient == null) {
            Debug.LogWarning("MedicalTestManager: Brak definicji badania lub referencji do pacjenta.");
            return;
        }

        /// Wywołuje logike badania
        MedicalTestResult result = test.PerformOn(currentActivePatient);

        /// Wyświetla sformatowany wynik badania w konsoli
        Debug.Log(result.GetSummary());

        /// Wysyła wynik każdej klasie nasłuchującej to zdarzenie.
        OnTestFinished?.Invoke(result);

    }

}