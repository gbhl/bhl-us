
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Collection : __Collection
	{
        /// <summary>
        /// A string containing the name of the collection, as well as details of what
        /// type of items the collection may contain
        /// </summary>
        public string CollectionNameDetail
        {
            get
            {
                if (this.CollectionName == string.Empty)
                {
                    return string.Empty;
                }
                else
                {
                    string nameDetail = this.CollectionName + " (";
                    if (this.CanContainTitles == 1) nameDetail += "Titles";
                    if (this.CanContainTitles + this.CanContainItems == 2) nameDetail += " and ";
                    if (this.CanContainItems == 1) nameDetail += "Items";
                    nameDetail += ")";
                    return nameDetail;
                }
            }
        }

        public string PreferredUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.CollectionURL))
                    return this.CollectionURL;
                else
                    return this.CollectionID.ToString();
            }
        }
    }
}
