
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class InstitutionRole : __InstitutionRole
	{
        static private string _nameContributor = "Contributor";
        static private string _nameRightsHolder= "Rights Holder";
        static private string _nameScanningInstitution = "Scanning Institution";
        static public string Contributor { get { return _nameContributor; } }
        static public string RightsHolder { get { return _nameRightsHolder; } }
        static public string ScanningInstitution { get { return _nameScanningInstitution; } }

    }
}

