using System.Collections.Generic;
using UnityEngine;

/// Reprezentuje pojedyncze badanie.
/// Implementuje interfejs IMedicalTest.

[System.Serializable]
public class MedicalTest : IMedicalTest
{

    /// Podstawowe informacje badania.
    public int id;
    public string name;
    public float cooldown;
    public List<int> detectableSymptomIds; // Lista ID symptomów.
    public int timeCost; // Koszt czasu gry za wykonanie badanai.

    /// Właściwość nazwy wymagana przez interfejs IMedicalTest.
    public string Name { get => name; set => name = value; }


    /// Wykonuje badanie na pacjencie i zwraca wykryte objawy.
    /// patient: Obiekt pacjenta badanego.
    public MedicalTestResult PerformOn(Patient patient)
    {

        MedicalTestResult result = new MedicalTestResult();
        result.testName = this.Name;

        /// Iteracja po wszystkich objawach pacjenta.
        foreach (Symptom s in patient.allPatientSymptoms)
        {

            /// Sprawdza czy ID symptomu znajduje się na liście wykrywalnych tego badania.
            if (detectableSymptomIds.Contains(s.id))
            {
                result.detectedSymptoms.Add(s);
            }
            
        }

        return result;
        
    }

}