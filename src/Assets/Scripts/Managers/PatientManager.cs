using System;
using System.Collections.Generic;
using System.Linq; // Wymagane dla metody ToList() przy losowaniu

public class PatientManager
{
    // Zdarzenie odpalane, gdy w gabinecie pojawia się nowy pacjent
    public event Action<Patient> OnPatientSpawned;
    
    public Patient CurrentPatient { get; private set; }
    public string PlayerNotes { get; set; } = string.Empty;

    // Repozytorium, z którego będziemy losować choroby
    private readonly IDataRepository _dataRepository;
    private readonly Random _random;

    // Wstrzykujemy repozytorium przez konstruktor (zgodnie z naszym Bootstrapperem)
    public PatientManager(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
        _random = new Random();
    }

    /// <summary>
    /// Tworzy nowego pacjenta losując chorobę z bazy danych.
    /// </summary>
    public void SpawnNewPatient()
    {
        // 1. Pobieramy wszystkie dostępne choroby z bazy
        List<Disease> allDiseases = _dataRepository.GetAllDiseases().ToList();

        // 2. Zabezpieczenie (Guard Clause) - jeśli baza jest pusta, rzucamy błędem
        if (allDiseases.Count == 0)
        {
            throw new Exception("Błąd krytyczny: Baza chorób jest pusta! Sprawdź pliki JSON.");
        }

        // 3. Losujemy jedną z chorób
        int randomIndex = _random.Next(allDiseases.Count);
        Disease randomlySelectedDisease = allDiseases[randomIndex];

        // 4. Tworzymy nowego pacjenta z wylosowaną chorobą
        CurrentPatient = new Patient(randomlySelectedDisease);

        // 5. Czyścimy notatki gracza po poprzednim pacjencie
        PlayerNotes = string.Empty;

        // 6. Powiadamiamy UI (PatientView), że pacjent czeka w gabinecie
        OnPatientSpawned?.Invoke(CurrentPatient);
    }

    /// <summary>
    /// Służy do odtworzenia pacjenta na podstawie pliku zapisu.
    /// </summary>
    public void RestorePatientState(string savedDiseaseId, string savedNotes)
    {
        Disease savedDisease = _dataRepository.GetDiseaseById(savedDiseaseId);
        
        if (savedDisease != null)
        {
            CurrentPatient = new Patient(savedDisease);
            PlayerNotes = savedNotes;
            
            // UI musi wiedzieć, że wczytaliśmy pacjenta
            OnPatientSpawned?.Invoke(CurrentPatient); 
        }
        else
        {
            // Jeśli z jakiegoś powodu ID z zapisu nie istnieje w bazie (np. usunąłeś chorobę z JSONa),
            // po prostu generujemy nowego pacjenta, żeby nie zepsuć gry.
            SpawnNewPatient();
        }
    }

    /// <summary>
    /// Sprawdza, czy gracz poprawnie rozpoznał chorobę.
    /// </summary>
    public bool EvaluateDiagnosis(Disease selectedDisease)
    {
        // Edge Cases: Upewniamy się, że obiekty w ogóle istnieją
        if (CurrentPatient == null || CurrentPatient.ActualDisease == null || selectedDisease == null)
        {
            return false;
        }

        // Diagnoza jest poprawna, jeśli ID wybranej choroby zgadza się z ID choroby pacjenta
        return CurrentPatient.ActualDisease.Id == selectedDisease.Id;
    }
}