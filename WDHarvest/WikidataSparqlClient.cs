public class WikidataSparqlClient
{
    public static class Endpoint
    {
        public static readonly string Primary = "https://query.wikidata.org/sparql";
        public static readonly string Scholarly = "https://query-scholarly.wikidata.org";
    }

    public static async Task<string> SubmitSparqlQueryAndSaveTsvAsync(string endpointUrl, string sparqlQuery)
    {
        string cleanedTsv;

        using (HttpClient client = new HttpClient())
        {
            // Set user-agent (required by Wikidata Query Service)
            client.DefaultRequestHeaders.Add("User-Agent", "BHLWDHarvest/1.0 (admin@biodiversitylibrary.org)");

            // Prepare the request content
            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("query", sparqlQuery)
        });

            // Set Accept header to TSV
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/tab-separated-values"));

            // Send POST request
            HttpResponseMessage response = await client.PostAsync(endpointUrl, content);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content as string
                string tsvContent = await response.Content.ReadAsStringAsync();

                // Remove surrounding quotes from each TSV cell (just in case)
                cleanedTsv = string.Join("\n",
                    tsvContent
                        .Split('\n')
                        .Select(line =>
                            string.Join("\t",
                                line.Split('\t')
                                    .Select(cell => cell.Trim('"'))
                            )
                        )
                );
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}\n{errorContent}");
            }
        }

        return cleanedTsv;
    }
}
