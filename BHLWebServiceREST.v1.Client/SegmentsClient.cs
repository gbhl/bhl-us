using System.Collections.Generic;
using System.Threading.Tasks;

namespace BHL.WebServiceREST.v1.Client
{
    public class SegmentsClient : RestClient
    {
        public SegmentsClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<Segment> GetSegmentAsync(int segmentID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentAsync(segmentID).ConfigureAwait(false));
            }
        }

        public Segment GetSegment(int segmentID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegment(segmentID);
            }
        }

        public async Task<Segment> GetSegmentDetailsAsync(int segmentID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentDetailsAsync(segmentID).ConfigureAwait(false));
            }
        }

        public Segment GetSegmentDetails(int segmentID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentDetails(segmentID);
            }
        }
        
        public async Task<Item> GetSegmentFilenamesAsync(int segmentID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentFilenamesAsync(segmentID).ConfigureAwait(false));
            }
        }

        public Item GetSegmentFilenames(int segmentID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentFilenames(segmentID);
            }
        }

        public async Task<ICollection<Institution>> GetSegmentInstitutionsByRoleAsync(int segmentID, string roleName)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentInstitutionsByRoleAsync(segmentID, roleName).ConfigureAwait(false));
            }
        }

        public ICollection<Institution> GetSegmentInstitutionsByRole(int segmentID, string roleName)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentInstitutionsByRole(segmentID, roleName);
            }
        }

        public async Task<ICollection<Page>> GetSegmentPagesAsync(int segmentID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentPagesAsync(segmentID).ConfigureAwait(false));
            }
        }

        public ICollection<Page> GetSegmentPages(int segmentID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentPages(segmentID);
            }
        }

        public async Task<ICollection<DOI>> GetSegmentWithoutDoisAsync(int numberToReturn)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentWithoutDoisAsync(numberToReturn).ConfigureAwait(false));
            }
        }

        public ICollection<DOI> GetSegmentWithoutDois(int numberToReturn)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentWithoutDois(numberToReturn);
            }
        }

        public async Task<ICollection<Segment>> GetSegmentsPublishedAsync()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentsPublishedAsync().ConfigureAwait(false));
            }
        }

        public ICollection<Segment> GetSegmentsPublished()
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentsPublished();
            }
        }

        public async Task<ICollection<Segment>> GetSegmentsRecentlyChangedAsync(string sinceDate)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentsRecentlyChangedAsync(sinceDate).ConfigureAwait(false));
            }
        }

        public ICollection<Segment> GetSegmentsRecentlyChanged(string sinceDate)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentsRecentlyChanged(sinceDate);
            }
        }

        public async Task<Segment> GetSegmentByItemIDAsync(int itemID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return (await restClient.GetSegmentByItemIDAsync(itemID).ConfigureAwait(false));
            }
        }

        public Segment GetSegmentByItemID(int itemID)
        {
            using (var httpClient = GetHttpClient())
            {
                BHLWS restClient = new BHLWS(_baseUrl, httpClient);
                return restClient.GetSegmentByItemID(itemID);
            }
        }
    }
}
