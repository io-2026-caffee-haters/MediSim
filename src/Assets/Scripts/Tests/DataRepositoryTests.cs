using NUnit.Framework;
using UnityEngine;

public class DataRepositoryTests
{
    private DataRepository repo;

    [SetUp]
    public void Setup()
    {

        GameObject go = new GameObject("TestDataRepository");
        repo = go.AddComponent<DataRepository>();
    }

    [Test]
    public void LoadStaticData_ShouldPopulateDatabase()
    {

        repo.LoadStaticData();

        var result = repo.GetRandomDisease();

        Assert.IsNotNull(result, "DataRepository nie załadowało danych - baza jest pusta.");
    }

    [Test]
    public void GetRandomDisease_ShouldReturnValidObject()
    {

        Disease disease = repo.GetRandomDisease();

        Assert.IsNotNull(disease, "Metoda GetRandomDisease zwróciła null.");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(repo.gameObject);
    }
}