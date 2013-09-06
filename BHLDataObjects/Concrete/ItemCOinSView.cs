
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class ItemCOinSView : __ItemCOinSView
	{
        private string _doi = string.Empty;
        public string Doi
        {
            get { return _doi; }
            set { _doi = value; }
        }
	}
}
