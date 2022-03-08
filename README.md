# HR.KvkConnector
.NET API client for the Netherlands Chamber of Commerce's new REST APIs. Supports the Zoeken (search), Basisprofiel (company profile), and Vestigingsprofiel (branch profile) APIs.

No external dependencies, uses standard .NET libraries such as `HttpWebRequest` to communicate with the backend server and `DataContractJsonSerializer` to deserialize JSON payloads.

Targets .NET Standard 2.0.

## Example

```csharp
const string baseUri = "https://api.kvk.nl/api/v1";
const string apiKey = "your-api-key";

IApiClient apiClient = new ApiClient(baseUri, apiKey);

var zoekparameters = new Parameters
{
    Handelsnaam = "Hogeschool",
    Plaats = "Rotterdam",
    Type = Vestigingstype.Hoofdvestiging | Vestigingstype.Nevenvestiging
};

var zoekresultaat = await apiClient.ZoekenAsync(zoekparameters, cancellationToken);

foreach (var resultaat in zoekresultaat.Resultaten)
{
    var kvkNummer = resultaat.KvkNummer;
    var basisprofiel = await apiClient.GetBasisprofielAsync(kvkNummer, geoData: false, cancellationToken);
    // Do something with retrieved company info.

    var vestigingsnummer = resultaat.Vestigingsnummer;
    var vestigingsprofiel = await apiClient.GetVestigingsprofielAsync(vestigingsnummer, geoData: true, cancellationToken);
    // Do something with retrieved branch info.
}
```
