using System.Configuration;

public class ConfigParms
{
    public string EmailFromAddress { get; set; } = string.Empty;
    public string EmailToAddress { get; set; } = string.Empty;
    public bool EmailOnError { get; set; } = false;

    public bool HarvestTitleIDs { get; set; } = true;
    public bool HarvestAuthorIDs { get; set; } = true;
    public bool EmailAnalysis { get; set; } = true;

    public string BHLWSEndpoint { get; set; } = string.Empty;

    public void LoadAppConfig()
    {
        EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
        EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
        EmailOnError = ConfigurationManager.AppSettings["EmailOnError"].ToLower() == "true";
        HarvestTitleIDs = ConfigurationManager.AppSettings["HarvestTitleIDs"].ToLower() == "true";
        HarvestAuthorIDs = ConfigurationManager.AppSettings["HarvestAuthorIDs"].ToLower() == "true";
        EmailAnalysis = ConfigurationManager.AppSettings["EmailAnalysis"].ToLower() == "true";
        BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
    }
}
