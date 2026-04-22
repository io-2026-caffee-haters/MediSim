using UnityEngine;
using TMPro;
using System.Text; 
using System.Collections.Generic;

/// Generuje i wyświetla wszystkie możliwe choroby oraz ich symptomy w postaci podręcznika.

public class EncyclopediaView : MonoBehaviour
{

    /// Referencja do bazy danych.
    public DatabaseManager databaseManager;

    /// Tekstowy komponent wyświetlający treść podręcznika.
    public TextMeshProUGUI diseaseTextDisplay; 

    /// Odświeża listę chorób przy każdorazowym otwarciu panelu encyklopedii.
    private void OnEnable()
    {
        UpdateEncyclopedia();
    }

    /// Składa pełny tekst na podstawie wczytanej bazy chorób i symptomów.
    public void UpdateEncyclopedia()
    {

        if (databaseManager == null) return;

        StringBuilder sb = new StringBuilder();

        /// Format nagłówka podręcznika.
        sb.AppendLine("<align=center><color=#000000><size=130%><b>PODRĘCZNIK MEDYCZNY</b></size></color></align>");
        sb.AppendLine("\n");

        /// Iteracja po wszystkich chorobach by zbudować ich opisy.
        foreach (var disease in databaseManager.diseasesList)
        {

            sb.AppendLine($"<color=#ff0000><b>{disease.name.ToUpper()}</b></color>");
            sb.Append("<b>Objawy:</b> ");
            
            // Tworzymy tymczasową listę nazw symptomów dla tej choroby
            List<string> symptomNames = new List<string>();

            /// Pobiera nazwy symptomów dla każdej choroby na podstawie ich identyfikatorów.
            foreach (int sID in disease.symptomIds)
            {
                // Szukamy w bazie symptomu o danym ID
                Symptom foundSymptom = databaseManager.symptomsList.Find(s => s.id == sID);
                
                if (foundSymptom != null)
                {
                    symptomNames.Add(foundSymptom.name);
                }
            }
            
            /// Łączy nazwy objawów w jeden ciąg tekstowy oddzielony przecinkami.
            string finalSymptoms = string.Join(", ", symptomNames);
            sb.AppendLine(finalSymptoms);
            sb.AppendLine(); 
        }

        diseaseTextDisplay.text = sb.ToString();

    }

}