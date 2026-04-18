public interface IDataRepository {
    void LoadStaticData();
    Disease GetDiseaseById(string id);
    IMedicalTest GetTestById(string id);
}