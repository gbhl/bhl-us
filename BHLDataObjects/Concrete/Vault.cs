
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Vault : __Vault
	{
		public Vault()
		{
		}

		public Vault( int vaultID, string server, string folderShare, string webVirtualDirectory, string ocrFolderShare, byte isCurrent )
			: base( vaultID, server, folderShare, webVirtualDirectory, ocrFolderShare, isCurrent )
		{
		}

		public string Description
		{
			get
			{
				if ( this.Server == null || this.Server.Length == 0 )
				{
					if ( this.FolderShare == null || this.FolderShare.Length == 0 )
					{
						return "";
					}
					else
					{
						return @"\" + this.FolderShare;
					}
				}
				else
				{
					return @"\\" + this.Server + @"\" + this.FolderShare;
				}
			}
			set { }
		}
	}
}