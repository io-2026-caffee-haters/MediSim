using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DatabaseManagerTests
{
    private DatabaseManager databaseManager;

    [SetUp]
    public void SetUp()
    {

        GameObject testObject = new GameObject("TestDatabaseManager");
        databaseManager = testObject.AddComponent<DatabaseManager>();
    
    }

    [TearDown]
    public void TearDown()
    {

        GameObject.DestroyImmediate(databaseManager.gameObject);
    
    }

    [Test]
    public void LoadDataFromJSON_PowinnaPrzeczytacWszystkieDaneZPlikowJSON()
    {

        databaseManager.LoadDataFromJSON();

        Disease randomDisease = databaseManager.GetRandomDisease();

        Assert.IsNotNull(randomDisease, "Po załadowaniu JSONa baza chorób nie powinna być pusta");
    
    }

    [Test]
    public void GetRandomDisease_PowinnaZwrocicLosowaChorobeZListy()
    {

        databaseManager.LoadDataFromJSON();

        Disease result = databaseManager.GetRandomDisease();

        Assert.IsNotNull(result, "Funkcja GetRandomDisease powinna zwrócić obiekt Disease, a nie null");
        Assert.IsFalse(string.IsNullOrEmpty(result.nazwa), "Wylosowana choroba powinna mieć przypisaną nazwę");
    
    }

    [Test]
    public void SaveGameState_PowinnoZapisacDoPlikuJSONStanGry()
    {

        SaveData testData = new SaveData();
        testData.savedScore = 500;
        testData.savedTime = 120.5f;
        testData.currentPatientDiseaseId = "1";

        databaseManager.SaveGameState(testData);

        SaveData loadedData = databaseManager.LoadGameState();

        Assert.IsNotNull(loadedData, "Załadowane dane nie powinny być nullem po zapisie");
        Assert.AreEqual(500, loadedData.savedScore, "Zapisany wynik powinien wynosić 500");
        Assert.AreEqual(120.5f, loadedData.savedTime, "Zapisany czas powinien wynosić 120,5");
        Assert.AreEqual("1", loadedData.currentPatientDiseaseId, "Zapisane ID choroby pacjenta powinno się zgadzać");
    
    }
}