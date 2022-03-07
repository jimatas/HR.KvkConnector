using HR.KvkConnector.Model;
using HR.KvkConnector.Model.Zoeken;

using System.Threading;
using System.Threading.Tasks;

namespace HR.KvkConnector
{
    public interface IApiClient
    {
        /// <summary>
        /// Voor een bedrijf zoeken naar basisinformatie.
        /// Er wordt max. 1000 resultaten getoond.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Resultaat> ZoekenAsync(Parameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Voor een specifieke vestiging informatie opvragen.
        /// </summary>
        /// <param name="vestigingsnummer">Vestigingsnummer: uniek nummer dat bestaat uit 12 cijfers.</param>
        /// <param name="geoData">GeoData: (true/false) geef aan of BAG data opgehaald moet worden.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Vestiging> GetVestigingsprofielAsync(string vestigingsnummer, bool geoData = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Voor een specifiek bedrijf basisinformatie opvragen.
        /// </summary>
        /// <param name="kvkNummer">Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.</param>
        /// <param name="geoData"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Basisprofiel> GetBasisprofielAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Voor een specifiek bedrijf eigenaar informatie opvragen.
        /// </summary>
        /// <param name="kvkNummer">Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.</param>
        /// <param name="geoData"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Eigenaar> GetEigenaarAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Voor een specifiek bedrijf hoofdvestigingsinformatie opvragen.
        /// </summary>
        /// <param name="kvkNummer">Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.</param>
        /// <param name="geoData"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Vestiging> GetHoofdvestigingAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Voor een specifiek bedrijf een lijst met vestigingen opvragen.
        /// </summary>
        /// <param name="kvkNummer">Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<VestigingList> GetVestigingenAsync(string kvkNummer, CancellationToken cancellationToken = default);
    }
}
