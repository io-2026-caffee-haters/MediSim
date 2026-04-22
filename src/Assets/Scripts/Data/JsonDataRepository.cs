using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonDataRepository : IDataRepository
{
    

    // ==========================================
    // STAN WEWNĘTRZNY (Pamięć podręczna / Cache)
    // ==========================================
    
    private readonly Dictionary<string, Symptom> _symptoms = new Dictionary<string, Symptom>();
    private readonly Dictionary<string, Disease> _diseases = new Dictionary<string, Disease>();
    private readonly Dictionary<string, IMedicalTest> _tests = new Dictionary<string, IMedicalTest>();

    // ==========================================
    // IMPLEMENTACJA INTERFEJSU
    // ==========================================

    public void LoadStaticData()
    {
        // Określa ścieżke do folderu z danymi w StreamingAssets.
        string folderPath = Path.Combine(Application.streamingAssetsPath, "Data");

        // Wczytuje symptomy.
        string symptomsJson = File.ReadAllText(Path.Combine(folderPath, "symptoms.json"));

        // Wczytuje choroby.
        string diseasesJson = File.ReadAllText(Path.Combine(folderPath, "diseases.json"));

        // Wczytuje badania
        string testsJson = File.ReadAllText(Path.Combine(folderPath, "medicaltests.json"));

        // Wczytuje dane    
        LoadFromJson(symptomsJson, diseasesJson, testsJson);

        Debug.Log("<color=blue>DatabaseManager:</color> Dane zostały pomyślnie załadowane.");
    }

    public void LoadFromJson(string symptomsJson, string diseasesJson, string testsJson)
    {
        // 1. Czyścimy słowniki na wypadek ponownego ładowania danych
        _symptoms.Clear();
        _diseases.Clear();
        _tests.Clear();

        // 2. Wczytujemy i budujemy OBJAWY (Zawsze jako pierwsze, bo nie mają zależności)
        var symData = JsonUtility.FromJson<SymptomListWrapper>(symptomsJson);
        if (symData?.items != null)
        {
            foreach (var dto in symData.items)
            {
                _symptoms[dto.id] = new Symptom(dto.id, dto.name, dto.isVisibleToNakedEye);
            }
        }

        // 3. Wczytujemy i budujemy CHOROBY (Hydracja / Zszywanie relacji)
        var disData = JsonUtility.FromJson<DiseaseListWrapper>(diseasesJson);
        if (disData?.items != null)
        {
            foreach (var dto in disData.items)
            {
                // Szukamy prawdziwych obiektów Symptom na podstawie ID z DTO
                var actualSymptoms = new List<Symptom>();
                foreach (var symId in dto.symptomIds)
                {
                    if (_symptoms.TryGetValue(symId, out Symptom foundSymptom))
                    {
                        actualSymptoms.Add(foundSymptom);
                    }
                    else
                    {
                        Debug.LogWarning($"Nie znaleziono objawu o ID: {symId} dla choroby {dto.name}");
                    }
                }

                _diseases[dto.id] = new Disease(dto.id, dto.name, actualSymptoms);
            }
        }

        // 4. Wczytujemy i budujemy TESTY MEDYCZNE
        var testData = JsonUtility.FromJson<TestListWrapper>(testsJson);
        if (testData?.items != null)
        {
            foreach (var dto in testData.items)
            {
                // Hydrate the symptoms
                var actualSymptoms = new List<Symptom>();
                foreach (var symId in dto.detectableSymptomIds)
                {
                    if (_symptoms.TryGetValue(symId, out Symptom foundSymptom))
                    {
                        actualSymptoms.Add(foundSymptom);
                    }
                }

                _tests[dto.id] = new MedicalTest(dto.name, dto.baseDuration, actualSymptoms);
            }
        }
    }

    public Disease GetDiseaseById(string id)
    {
        // Zwraca chorobę ze słownika. Jeśli ID nie istnieje, zwraca null.
        return _diseases.TryGetValue(id, out Disease disease) ? disease : null;
    }

    public IMedicalTest GetTestById(string id)
    {
        return _tests.TryGetValue(id, out IMedicalTest test) ? test : null;
    }

    // IEnumerable zwraca wszystkie testy bez możliwości ich edycji
    public IEnumerable<IMedicalTest> GetAllTests()
    {
        return _tests.Values;
    }

    public IEnumerable<Disease> GetAllDiseases()
    {
        return _diseases.Values;
    }



    // ==========================================
    // WRAPPERY DLA JSON UTILITY
    // ==========================================
    // Unity nie potrafi odczytać z JSONa samej listy np. [...]. 
    // Wymaga obiektu, który tę listę posiada {"items": [...]}.
    
    [Serializable]
    private class SymptomListWrapper { public List<SymptomDTO> items; }
    
    [Serializable]
    private class DiseaseListWrapper { public List<DiseaseDTO> items; }
    
    [Serializable]
    private class TestListWrapper { public List<MedicalTestDTO> items; }
}