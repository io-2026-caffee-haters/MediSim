using System.Collections.Generic;

public interface IDataRepository {
    void LoadStaticData();
    Disease GetDiseaseById(string id);
    IMedicalTest GetTestById(string id);
    
    // IEnumerable zamiast listy, ponieważ można tylko odczytywać elementy (bez uprawnień edycji)
    IEnumerable<IMedicalTest> GetAllTests();
    IEnumerable<Disease> GetAllDiseases();
}