using UnityEngine;
using System.Collections.Generic;
using System.IO;

/// Zarządza bazą danych gry, odpowiada za wczytywanie danych o sympotomach,
/// chorobach i badaniach z plików JSON znajdujących się w folderze StreamingAssets.

public class DatabaseManager : MonoBehaviour
{

    /// Listy wszystkich wczytanych danych z JSON'a.
    public List<Symptom> symptomsList;
    public List<Disease> diseasesList;
    public List<MedicalTest> medicaltestsList;


    /// Metada inicjalizacji od początku odpalenia gry.
    void Awake() 
    {

        /// Określa ścieżke do folderu z danymi w StreamingAssets.
        string folderPath = Path.Combine(Application.streamingAssetsPath, "Data");

        /// Wczytuje symptomy.
        string symptomsJson = File.ReadAllText(Path.Combine(folderPath, "symptoms.json"));
        symptomsList = new List<Symptom>(JsonUtility.FromJson<SymptomWrapper>(symptomsJson).symptoms);

        // Wczytuje choroby.
        string diseasesJson = File.ReadAllText(Path.Combine(folderPath, "diseases.json"));
        diseasesList = new List<Disease>(JsonUtility.FromJson<DiseaseWrapper>(diseasesJson).diseases);

        // Wczytujemy badania.
        string medicaltestsJson = File.ReadAllText(Path.Combine(folderPath, "medicaltests.json"));
        medicaltestsList = new List<MedicalTest>(JsonUtility.FromJson<MedicalTestWrapper>(medicaltestsJson).medicaltests);
    
        Debug.Log("<color=blue>DatabaseManager:</color> Dane zostały pomyślnie załadowane.");

    }


    /// Kontenery pomocnicze dla tablic (niezbędne do wczytania).
    [System.Serializable] private class SymptomWrapper { public Symptom[] symptoms; }
    [System.Serializable] private class DiseaseWrapper { public Disease[] diseases; }
    [System.Serializable] private class MedicalTestWrapper { public MedicalTest[] medicaltests; }

}