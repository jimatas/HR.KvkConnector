using HR.KvkConnector.Infrastructure;
using HR.KvkConnector.Model;
using HR.KvkConnector.Model.Errors;
using HR.KvkConnector.Model.Zoeken;

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace HR.KvkConnector
{
    public class ApiClient : IApiClient
    {
        private readonly Uri baseUri;
        private readonly string apiKey;

        public ApiClient(string baseUri, string apiKey)
            : this(new Uri(baseUri.TrimEnd('/')), apiKey)
        {
        }

        public ApiClient(Uri baseUri, string apiKey)
        {
            this.baseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            this.apiKey = apiKey;
        }

        /// <inheritdoc/>
        public async Task<Resultaat> ZoekenAsync(Parameters parameters, CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetFromJsonAsync<Resultaat>(
                    requestUri: $"{baseUri}/zoeken{parameters}",
                    cancellationToken).WithoutCapturingContext();
            }
            catch (ApiException exception) when (exception.StatusCode == HttpStatusCode.NotFound)
            {
                return new Resultaat();
            }
        }

        /// <inheritdoc/>
        public async Task<Vestiging> GetVestigingsprofielAsync(string vestigingsnummer, bool geoData = false, CancellationToken cancellationToken = default)
            => await GetFromJsonAsync<Vestiging>(
                requestUri: $"{baseUri}/vestigingsprofielen/{Uri.EscapeDataString(vestigingsnummer)}?geoData={geoData}",
                cancellationToken).WithoutCapturingContext();

        /// <inheritdoc/>
        public async Task<Basisprofiel> GetBasisprofielAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default)
            => await GetFromJsonAsync<Basisprofiel>(
                requestUri: $"{baseUri}/basisprofielen/{Uri.EscapeDataString(kvkNummer)}?geoData={geoData}",
                cancellationToken).WithoutCapturingContext();

        /// <inheritdoc/>
        public async Task<Eigenaar> GetEigenaarAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default)
            => await GetFromJsonAsync<Eigenaar>(
                requestUri: $"{baseUri}/basisprofielen/{Uri.EscapeDataString(kvkNummer)}/eigenaar?geoData={geoData}",
                cancellationToken).WithoutCapturingContext();

        /// <inheritdoc/>
        public async Task<Vestiging> GetHoofdvestigingAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default)
            => await GetFromJsonAsync<Vestiging>(
                requestUri: $"{baseUri}/basisprofielen/{Uri.EscapeDataString(kvkNummer)}/hoofdvestiging?geoData={geoData}",
                cancellationToken).WithoutCapturingContext();

        /// <inheritdoc/>
        public async Task<VestigingList> GetVestigingenAsync(string kvkNummer, CancellationToken cancellationToken = default)
            => await GetFromJsonAsync<VestigingList>(
                requestUri: $"{baseUri}/basisprofielen/{Uri.EscapeDataString(kvkNummer)}/vestigingen",
                cancellationToken).WithoutCapturingContext();

        private async Task<TResult> GetFromJsonAsync<TResult>(string requestUri, CancellationToken cancellationToken)
        {
            var httpRequest = WebRequest.CreateHttp(requestUri);

            httpRequest.Method = "GET";
            httpRequest.Accept = "application/json";
            httpRequest.Headers.Add("apikey", apiKey);
            httpRequest.AddTrustedRootCertificates();

            using (var httpResponse = await httpRequest.GetHttpResponseAsync(ensureSuccessCode: false).WithCancellation(cancellationToken).WithoutCapturingContext())
            using (var responseBody = new StreamReader(httpResponse.GetResponseStream()))
            {
                var jsonString = await responseBody.ReadToEndAsync().WithoutCapturingContext();

                if (httpResponse.IsErrorStatusCode())
                {
                    var apiErrors = JsonSerializer.Deserialize<ApiErrorResponse>(jsonString);
                    if (apiErrors.Any())
                    {
                        throw new ApiException(httpResponse.StatusCode, apiErrors);
                    }

                    var httpError = JsonSerializer.Deserialize<HttpError>(jsonString);
                    throw new ApiException((HttpStatusCode)httpError.StatusCode, httpError.ToApiError());
                }

                return JsonSerializer.Deserialize<TResult>(jsonString);
            }
        }
    }
}
