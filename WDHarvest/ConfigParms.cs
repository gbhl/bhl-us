using System.Configuration;

public class ConfigParms
{
    public string EmailFromAddress { get; set; } = string.Empty;
    public string AdminEmailToAddress { get; set; } = string.Empty;
    public string StaffEmailToAddress { get; set; } = string.Empty;
    public bool EmailOnError { get; set; } = false;

    public bool HarvestTitleIDs { get; set; } = true;
    public bool HarvestAuthorIDs { get; set; } = true;
    public bool PublishTitleIDs { get; set; } = true;
    public bool PublishAuthorIDs { get; set; } = true;
    public bool DoAnalysis { get; set; } = true;

    public string BHLWSEndpoint { get; set; } = string.Empty;

    public void LoadAppConfig()
    {
        EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
        AdminEmailToAddress = ConfigurationManager.AppSettings["AdminEmailToAddress"];
        StaffEmailToAddress = ConfigurationManager.AppSettings["StaffEmailToAddress"];
        EmailOnError = ConfigurationManager.AppSettings["EmailOnError"].ToLower() == "true";
        HarvestTitleIDs = ConfigurationManager.AppSettings["HarvestTitleIDs"].ToLower() == "true";
        HarvestAuthorIDs = ConfigurationManager.AppSettings["HarvestAuthorIDs"].ToLower() == "true";
        PublishTitleIDs = ConfigurationManager.AppSettings["PublishTitleIDs"].ToLower() == "true";
        PublishAuthorIDs = ConfigurationManager.AppSettings["PublishAuthorIDs"].ToLower() == "true";
        DoAnalysis = ConfigurationManager.AppSettings["DoAnalysis"].ToLower() == "true";
        BHLWSEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
    }
}
