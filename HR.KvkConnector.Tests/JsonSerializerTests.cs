using HR.KvkConnector.Infrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Runtime.Serialization;

namespace HR.KvkConnector.Tests.Fixture
{
    [TestClass]
    public class JsonSerializerTests
    {
        [TestMethod]
        public void Serialize_GivenNullObject_ReturnsEmptyString()
        {
            // Arrange
            object objectGraph = null;

            // Act
            var jsonString = JsonSerializer.Serialize(objectGraph);

            // Assert
            Assert.AreEqual(string.Empty, jsonString);
        }

        [TestMethod]
        public void Serialize_GivenNonNullObject_ReturnsJsonString()
        {
            // Arrange
            var objectGraph = new Model.Handelsnaam();

            // Act
            var jsonString = JsonSerializer.Serialize(objectGraph);

            // Assert
            Assert.AreEqual("{\"naam\":null,\"volgorde\":null}", jsonString);
        }

        [TestMethod]
        public void Deserialize_GivenNullString_ReturnsNullObject()
        {
            // Arrange
            string jsonString = null;

            // Act
            var objectGraph = JsonSerializer.Deserialize<Model.Handelsnaam>(jsonString);

            // Assert
            Assert.AreEqual(null, objectGraph);
        }

        [TestMethod]
        public void Deserialize_GivenEmptyString_ReturnsNullObject()
        {
            // Arrange
            var jsonString = string.Empty;

            // Act
            var objectGraph = JsonSerializer.Deserialize<Model.Handelsnaam>(jsonString);

            // Assert
            Assert.AreEqual(null, objectGraph);
        }

        [DataTestMethod]
        [DataRow(" ")]
        [DataRow("x")]
        [DataRow(" { ] ")]
        public void Deserialize_GivenInvalidJsonString_ThrowsException(string jsonString)
        {
            // Arrange

            // Act
            void action() => JsonSerializer.Deserialize<Model.Handelsnaam>(jsonString);

            // Assert
            Assert.ThrowsException<SerializationException>(action);
        }
    }
}
