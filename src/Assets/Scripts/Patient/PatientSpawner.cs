using UnityEngine;
using UnityEngine.UI; // DODANE: Wymagane do obsługi komponentu Image
using System.Collections.Generic;

/// Losowanie chorób z bazy danych oraz tworzenie nowych obiektów pacjentów.
public class PatientSpawner : MonoBehaviour
{
    /// Referencja do bazy danych.
    public DatabaseManager databaseManager;
    
    /// Ustawienia spawnu pacjenta.
    public GameObject patientPrefab; /// Wizualny obiekt pacjenta.
    public Transform spawnPoint; /// Pozycja spawnu pacjenta.
    public Transform patientContainer; /// Kontener w którym tworzy się pacjent.

    public Sprite[] patientSprites; 

    void Start()
    {
        if (SaveLoadController.IsLoadingGame)
        {
            return; 
        }

        Invoke("SpawnPatient", 0.2f);
    }

    public void SpawnPatient()
    {
        if (databaseManager.diseasesList == null || databaseManager.diseasesList.Count == 0)
        {
            Debug.LogError("PatientSpawner: Baza chorób jest pusta!");
            return;
        }

        Disease randomDisease = databaseManager.diseasesList[Random.Range(0, databaseManager.diseasesList.Count)];

        List<Symptom> patientSymptoms = new List<Symptom>();
        foreach (int sId in randomDisease.symptomIds)
        {
            Symptom foundSymptom = databaseManager.symptomsList.Find(s => s.id == sId);
            if (foundSymptom != null)
            {
                patientSymptoms.Add(foundSymptom);
            }
        }

        Vector3 pos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
        GameObject newPatientObj = Instantiate(patientPrefab, spawnPoint.position, Quaternion.identity);

        if (patientContainer != null)
        {
            newPatientObj.transform.SetParent(patientContainer, false);
        }
        else
        {
            newPatientObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }

        if (patientSprites != null && patientSprites.Length > 0)
        {
            Sprite randomSprite = patientSprites[Random.Range(0, patientSprites.Length)];
            
            Image patientImage = newPatientObj.GetComponentInChildren<Image>();
            if (patientImage != null)
            {
                patientImage.sprite = randomSprite;
            }
            else
            {
                Debug.LogWarning("PatientSpawner: Nie znaleziono komponentu Image na prefabie pacjenta!");
            }
        }

        Patient patientScript = newPatientObj.GetComponent<Patient>();
        if (patientScript != null)
        {
            patientScript.Initialize(randomDisease, patientSymptoms);
            Object.FindFirstObjectByType<MedicalTestManager>().currentActivePatient = patientScript;
        }
    }

    public void SpawnPatientFromLoad(string diseaseId)
    {
        CancelInvoke("SpawnPatient");

        if (databaseManager.diseasesList == null || databaseManager.diseasesList.Count == 0)
        {
            Debug.LogError("PatientSpawner (Load): Baza chorób jest pusta!");
            return;
        }

        Disease loadedDisease = databaseManager.diseasesList.Find(d => d.id.ToString() == diseaseId);
        if (loadedDisease == null)
        {
            Debug.LogError($"PatientSpawner (Load): Nie znaleziono choroby o ID {diseaseId}");
            return;
        }

        List<Symptom> patientSymptoms = new List<Symptom>();
        foreach (int sId in loadedDisease.symptomIds)
        {
            Symptom foundSymptom = databaseManager.symptomsList.Find(s => s.id == sId);
            if (foundSymptom != null)
            {
                patientSymptoms.Add(foundSymptom);
            }
        }

        GameObject newPatientObj = Instantiate(patientPrefab, spawnPoint.position, Quaternion.identity);

        if (patientContainer != null)
        {
            newPatientObj.transform.SetParent(patientContainer, false);
        }
        else
        {
            newPatientObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }

        if (patientSprites != null && patientSprites.Length > 0)
        {
            Sprite randomSprite = patientSprites[Random.Range(0, patientSprites.Length)];
            Image patientImage = newPatientObj.GetComponentInChildren<Image>();
            if (patientImage != null)
            {
                patientImage.sprite = randomSprite;
            }
        }

        Patient patientScript = newPatientObj.GetComponent<Patient>();
        if (patientScript != null)
        {
            patientScript.Initialize(loadedDisease, patientSymptoms);
            
            MedicalTestManager testManager = Object.FindFirstObjectByType<MedicalTestManager>();
            if (testManager != null)
            {
                testManager.currentActivePatient = patientScript;
            }
        }
    }
}