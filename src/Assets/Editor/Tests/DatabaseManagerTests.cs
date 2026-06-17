using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class DatabaseManagerTests
{
    
    [Test]
    public void DatabaseLists_AreNotNullAfterAwake()
    {

        GameObject go = new GameObject();
        DatabaseManager db = go.AddComponent<DatabaseManager>();
        
        Assert.IsNotNull(db.symptomsList, "Lista symptomów powinna być zainicjalizowana.");
        Assert.IsNotNull(db.diseasesList, "Lista chorób powinna być zainicjalizowana.");
        Assert.IsNotNull(db.medicaltestsList, "Lista badań powinna być zainicjalizowana.");
    
    }

}