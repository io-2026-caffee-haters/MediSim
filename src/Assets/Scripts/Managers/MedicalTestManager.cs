using UnityEngine;
using System;
using System.Collections.Generic;

/// Zarządza wykonywaniem badania medycznego.
/// Łączy dane z DatabaseManager'a z konkretym obiektem pacjenta.

public class MedicalTestManager : MonoBehaviour
{

    /// Referencja do managera bazy danych.
    public DatabaseManager databaseManager;

    /// Referencja do czasy i punktów gry.
    public ScoreTimeController scoreTimeController;

    /// Definicja zdarzenia na którym UI musi się zapisać.
    public static event Action<MedicalTestResult> OnTestFinished;

    /// Referencja do obecnego pacjenta (ukryta dla poprawnego działania funkcji).
    [HideInInspector] public Patient currentActivePatient;

    /// Słownik przechowujący informacje o id badania oraz jego obecny czas odnowienia.
    private Dictionary<int, float> nextAvailableTime = new Dictionary<int, float>();

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

        /// Jeśli badanie było robione, sprawdza czy jest na cooldownie.
        if (nextAvailableTime.ContainsKey(testId))
        {
            if (Time.time < nextAvailableTime[testId])
            {
                float remainingCooldown = nextAvailableTime[testId] - Time.time;
                Debug.Log($"<color=red>COOLDOWN {remainingCooldown:F1} s.</color>");
                return;
            }
        }

        /// Znajduje dane badania z bazy.
        MedicalTest test = databaseManager.medicaltestsList.Find(t => t.id == testId);

        /// Sprawdza czy badanie i pacjent istnieje.
        if (test == null || currentActivePatient == null) {
            Debug.LogWarning("MedicalTestManager: Brak definicji badania lub referencji do pacjenta.");
            return;
        }

        /// Odlicza czas gry za użycie badania.
        if (scoreTimeController != null && test.timeCost > 0)
        {
            scoreTimeController.DeductTimeCost(test.timeCost);
        }

        /// Ustawawia cooldown po użyciu badania.
        if (test.cooldown > 0)
        {
            nextAvailableTime[testId] = Time.time + test.cooldown; 
        }

        /// Wywołuje logike badania
        MedicalTestResult result = test.PerformOn(currentActivePatient);

        /// Wyświetla sformatowany wynik badania w konsoli
        Debug.Log(result.GetSummary());

        /// Wysyła wynik każdej klasie nasłuchującej to zdarzenie.
        OnTestFinished?.Invoke(result);

    }

}