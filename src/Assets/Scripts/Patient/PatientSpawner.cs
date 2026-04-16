using UnityEngine;
using System.Collections.Generic;

/// Losowanie chorób z bazy danych oraz tworzenie nowych obiektów pacjentów.


public class PatientSpawner : MonoBehaviour
{

    /// Referencja do bazy danych.
    public DatabaseManager databaseManager;
    
    /// Ustawienia spawnu pacjenta.
    public GameObject patientPrefab; /// Wizualny obiekt pacjenta.
    public Transform spawnPoint; /// Pozycja spawnu pacjenta.


    void Start()
    {
        /// Wywołuje funkcje z lekkim opóźnieniem, aby DatabaseManager wczytał dane.
        Invoke("SpawnPatient", 0.2f);
    }


    /// Tworzy pacjenta, losuje chorobę i przypisuje jej symptomy.
    public void SpawnPatient()
    {

        /// Sprawdza czy baza danych została poprawnie załadowana.
        if (databaseManager.diseasesList == null || databaseManager.diseasesList.Count == 0)
        {
            Debug.LogError("Baza chorób jest pusta!");
            return;
        }

        /// Losuje chorobe z wczytanej listy DatabaseManager'a.
        Disease randomDisease = databaseManager.diseasesList[Random.Range(0, databaseManager.diseasesList.Count)];

        /// Mapuje identyfikatory ID na pełne obiekty klasy symptom.
        List<Symptom> patientSymptoms = new List<Symptom>();
        foreach (int sId in randomDisease.symptomIds)
        {
            
            /// Szuka symptomów w bazie po ich ID.
            Symptom foundSymptom = databaseManager.symptomsList.Find(s => s.id == sId);
            if (foundSymptom != null)
            {
                patientSymptoms.Add(foundSymptom);
            }

        }

        /// Tworzy fizyczny obiekt pacjenta na scenie w miejscu spawnPoint.
        Vector3 pos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
        GameObject newPatientObj = Instantiate(patientPrefab, spawnPoint.position, Quaternion.identity);
        newPatientObj.transform.SetParent(GameObject.Find("Canvas").transform, false);

        /// Pobiera skrypt Patient i przekazuje mu wylosowane dane.
        Patient patientScript = newPatientObj.GetComponent<Patient>();
        
        if (patientScript != null)
        {
            patientScript.Initialize(randomDisease, patientSymptoms); //
        }
        else 
        {
            Debug.LogError("PatientSpawner: Prefab pacjenta nie posiada skryptu 'Patient'");
        }

    }

}