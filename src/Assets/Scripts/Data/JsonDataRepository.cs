using System;
using System.Collections.Generic;
using UnityEngine;

public class JsonDataRepository : IDataRepository
{
    // Główna metoda z interfejsu (do użycia w grze)
    public void LoadStaticData()
    {
        throw new NotImplementedException();
    }

    // Metoda pomocnicza ułatwiająca testowanie TDD i wstrzykiwanie danych
    public void LoadFromJson(string symptomsJson, string diseasesJson, string testsJson)
    {
        throw new NotImplementedException();
    }

    public Disease GetDiseaseById(string id)
    {
        throw new NotImplementedException();
    }

    public IMedicalTest GetTestById(string id)
    {
        throw new NotImplementedException();
    }
}