using System;
using System.Linq;
using HR.KvkConnector.Tests.Fixture;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;
using FluentAssertions;
using HR.KvkConnector.Model.Zoeken;

namespace HR.KvkConnector.Tests
{
    [TestClass]
    public class ApiClientTests
    {

        [TestMethod]
        public async Task GetVestiging_No_Issue_DateTime()
        {
            IApiClient client = new ApiClient(new Uri("https://api.kvk.nl/api/v1"), "");
            var result = await client.GetVestigingsprofielAsync("000051127369");
            result.MaterieleRegistratie.DatumAanvang.Value.Date.Should().Be(new DateTime(2022, 1,3));
        }

        [TestMethod]
        public async Task GetVestiging_DateTime_Reset_To_Minimum_Value()
        {
            IApiClient client = new ApiClient(new Uri("https://api.kvk.nl/api/v1"), "");
            var result = await client.GetVestigingsprofielAsync("000019061714");
            result.MaterieleRegistratie.DatumAanvang.Value.Date.Should().Be(new DateTime(1, 1, 1));
        }


        [TestMethod]
        public async Task GetZoeken_TestData_From_Live_Service_KVK()
        {
            IApiClient client = new ApiClient(new Uri("https://api.kvk.nl/api/v1"), "");
            var result = await client.ZoekenAsync(new Parameters() { KvkNummer = "85058769" });
            var handelsNaam = result.Resultaten.First().Handelsnaam;
            handelsNaam.Should().Be("the Right Direction BV");
        }

        [TestMethod]
        public async Task GetZoeken_TestData_From_Test_Service_KVK()
        {
            IApiClient client = new ApiClient(new Uri("https://developers.kvk.nl/test/api/v1"), string.Empty);
            var result = await client.ZoekenAsync(new Parameters() {KvkNummer = "68750110"});
            var handelsNaam = result.Resultaten.First().Handelsnaam;
            handelsNaam.Should().Be("Test BV Donald");
        }

        [TestMethod]
        public async Task GetBasisprofielAsync_TestData_From_Test_Service_KVK()
        {
            IApiClient client = new ApiClient(new Uri("https://developers.kvk.nl/test/api/v1"), string.Empty);
            var result = await client.GetBasisprofielAsync("68727720");
            result.Handelsnamen.Count().Should().Be(1);
        }

        [TestMethod]
        public async Task GetVestigingenAsync_TestData_From_Test_Service_KVK()
        {
            IApiClient client = new ApiClient(new Uri("https://developers.kvk.nl/test/api/v1"), string.Empty);

            // Act
            var vestigingen = await client.GetVestigingenAsync(kvkNummer: "90001354");
            vestigingen.Vestigingen.First().Vestigingsnummer.Should().Be("990000541921");
        }

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
