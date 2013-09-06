using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
	public partial class BHLProvider
	{
		public CustomGenericList<Language> LanguageSelectAll()
		{
			return new LanguageDAL().SelectAll( null, null );
		}

		public Language LanguageSelectAuto( string languageCode )
		{
			return ( new LanguageDAL().LanguageSelectAuto( null, null, languageCode ) );
		}

        public CustomGenericList<Language> LanguageSelectWithPublishedItems()
        {
            return (new LanguageDAL().LanguageSelectWithPublishedItems(null, null));
        }

        public void SaveLanguage(Language language)
		{
			new LanguageDAL().Save( null, null, language );
		}
	}
}
