using HR.KvkConnector.Tests.Fixture;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;

namespace HR.KvkConnector.Tests
{
    [TestClass]
    public class ApiClientTests
    {
        [TestMethod]
        public async Task GetBasisprofielAsync_ByDefault_ReturnsObjectDeserializedFromJson()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient();

            // Act
            var basisprofiel = await apiClient.GetBasisprofielAsync(kvkNummer: "41129830");

            // Assert
            Assert.IsNotNull(basisprofiel);
        }

        [TestMethod]
        public async Task GetEigenaarAsync_ByDefault_ReturnsObjectDeserializedFromJson()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient();

            // Act
            var eigenaar = await apiClient.GetEigenaarAsync(kvkNummer: "41129830");

            // Assert
            Assert.IsNotNull(eigenaar);
        }

        [TestMethod]
        public async Task GetHoofdvestigingAsync_ByDefault_ReturnsObjectDeserializedFromJson()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient();

            // Act
            var hoofdvestiging = await apiClient.GetHoofdvestigingAsync(kvkNummer: "41129830");

            // Assert
            Assert.IsNotNull(hoofdvestiging);
        }

        [TestMethod]
        public async Task GetVestigingenAsync_ByDefault_ReturnsObjectDeserializedFromJson()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient();

            // Act
            var vestigingen = await apiClient.GetVestigingenAsync(kvkNummer: "41129830");

            // Assert
            Assert.IsNotNull(vestigingen);
        }

        [TestMethod]
        public async Task GetVestigingsprofielAsync_ByDefault_ReturnsObjectDeserializedFromJson()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient();

            // Act
            var vestigingsprofiel = await apiClient.GetVestigingsprofielAsync(vestigingsnummer: "000000080896");

            // Assert
            Assert.IsNotNull(vestigingsprofiel);
        }
    }
}
