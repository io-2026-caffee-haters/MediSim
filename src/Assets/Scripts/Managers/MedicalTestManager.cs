using UnityEngine;

/// Zarządza wykonywaniem badania medycznego.
/// Łączy dane z DatabaseManager'a z konkretym obiektem pacjenta.

public class MedicalTestManager : MonoBehaviour
{

    /// Referencja do managera bazy danych.
    public DatabaseManager databaseManager;


    /// Pobiera definicje badania po ID i wykonuje na pacjencie.
    /// testId: ID badania z pliku medicaltests.json.
    /// currentPatient: Obiekt pacjenta obecnie badanego.
    public void ExecuteTest(int testId, Patient currentPatient)
    {

        /// Znajduje dane badania z bazy.
        MedicalTest test = databaseManager.medicaltestsList.Find(t => t.id == testId);

        /// Sprawdza czy badanie i pacjent istnieje.
        if (test == null || currentPatient == null) {
            Debug.LogWarning("MedicalTestManager: Brak definicji badania lub referencji do pacjenta.");
            return;
        }

        /// Wywołuje logike badania
        MedicalTestResult result = test.PerformOn(currentPatient);

        /// Wyświetla sformatowany wynik badania w konsoli
        Debug.Log(result.GetSummary());

    }

}