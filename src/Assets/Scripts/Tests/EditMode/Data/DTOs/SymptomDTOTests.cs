using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class SymptomDTOTests
{
    [Test]
    public void SymptomDTO_ShouldSerializeAndDeserializeCorrectly()
    {
        // Arrange
        var original = new SymptomDTO
        {
            id = 10,
            name = "Gorączka",
            isVisible = false
        };

        // Act
        string json = JsonUtility.ToJson(original);
        var deserialized = JsonUtility.FromJson<SymptomDTO>(json);

        // Assert
        Assert.IsNotNull(deserialized);
        Assert.AreEqual(original.id, deserialized.id);
        Assert.AreEqual(original.name, deserialized.name);
        Assert.AreEqual(original.isVisible, deserialized.isVisible);
    }
}