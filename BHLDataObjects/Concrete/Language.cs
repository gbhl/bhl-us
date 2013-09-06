
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Language : __Language
	{
		public Language( string languageCode, string languageName, string note )
			: base( languageCode, languageName, note )
		{
		}

		public Language()
		{
		}
	}
}
