using System.Collections.Generic;

/// Przechowuje wyniki przeprowadzonego badania, w tym nazwę jego
/// nazwę oraz listę objawów, które udało się wykryć.

public class MedicalTestResult
{

    /// Nazwa wyświetlana badania, które wygenerowało ten wynik.
    public string testName;

    /// Lista obiektów symptomów zidentyfikowanych podczas testu.
    public List<Symptom> detectedSymptoms = new List<Symptom>();


    /// Generuje sformatowany tekst podsumowujący znalezione symptomy przy badaniu.
    public string GetSummary()
    {

        /// Jeśli nie wykryto żadnych objawów
        if (detectedSymptoms.Count == 0) 
            return $"{testName}: Nie wykryto żadnych objawów.";

        /// Konstrukcja listy wykrytych objawów w formie punktów
        string summary = $"{testName} wykrył objawy:\n";
        foreach (var i in detectedSymptoms)
        {
            summary += $"- {i.name}\n";
        }
        
        return summary;

    }

}