using HR.KvkConnector.Infrastructure;
using HR.KvkConnector.Model;
using HR.KvkConnector.Model.Zoeken;

using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace HR.KvkConnector.Tests.Fixture
{
    public class FakeApiClient : IApiClient
    {
        public Task<Basisprofiel> GetBasisprofielAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default)
            => GetFromJsonAsync<Basisprofiel>("basisprofiel.json");

        public Task<Eigenaar> GetEigenaarAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default)
            => GetFromJsonAsync<Eigenaar>("eigenaar.json");

        public Task<Vestiging> GetHoofdvestigingAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default)
            => GetFromJsonAsync<Vestiging>("hoofdvestiging.json");

        public Task<VestigingList> GetVestigingenAsync(string kvkNummer, CancellationToken cancellationToken = default)
            => GetFromJsonAsync<VestigingList>("vestigingen.json");

        public Task<Vestiging> GetVestigingsprofielAsync(string vestigingsnummer, bool geoData = false, CancellationToken cancellationToken = default)
            => GetFromJsonAsync<Vestiging>("vestigingsprofiel.json");

        public Task<Resultaat> ZoekenAsync(Parameters parameters, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private static async Task<TResult> GetFromJsonAsync<TResult>(string jsonFileName)
        {
            var resourceName = $"HR.KvkConnector.Tests.Fixture.Json.{jsonFileName}";

            using var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            using var fileReader = new StreamReader(fileStream);

            var jsonString = await fileReader.ReadToEndAsync();

            return JsonSerializer.Deserialize<TResult>(jsonString);
        }
    }
}
