using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BHLApi3Web.Models
{
    [Serializable]
    public class Title : DataObjectBase
    {
		#region Constructors
		
		public Title()
		{
		}

		#endregion Constructors

        #region Properities

        [JsonProperty("id")]
        private int _titleID;

        [JsonIgnore]
        public int TitleId 
        { 
            get { return _titleID; }
            set { _titleID = value; }
        }

        [JsonProperty("fulltitle")]
        private string _fullTitle = null;

        [JsonIgnore]
        public string FullTitle
        {
            get { return _fullTitle; }
            set { _fullTitle = value; }
        }

        [JsonProperty("partnumber")]
        private string _partNumber = null;

        [JsonIgnore]
        public string PartNumber
        {
            get { return _partNumber; }
            set { _partNumber = value; }
        }

        [JsonProperty("partname")]
        private string _partName = null;

        [JsonIgnore]
        public string PartName
        {
            get { return _partName; }
            set { _partName = value; }
        }

        [JsonProperty("callnumber")]
        private string _callNumber = null;

        [JsonIgnore]
        public string CallNumber
        {
            get { return _callNumber; }
            set { _callNumber = value; }
        }

        [JsonProperty("edition")]
        private string _edition = null;

        [JsonIgnore]
        public string Edition
        {
            get { return _edition; }
            set { _edition = value; }
        }

        [JsonProperty("publisherplace")]
        private string _publisherPlace = null;

        [JsonIgnore]
        public string PublisherPlace
        {
            get { return _publisherPlace; }
            set { _publisherPlace = value; }
        }

        [JsonProperty("publishername")]
        private string _publisherName = null;

        [JsonIgnore]
        public string PublisherName
        {
            get { return _publisherName; }
            set { _publisherName = value; }
        }

        [JsonProperty("publicationdate")]
        private string _publicationDate = null;

        [JsonIgnore]
        public string PublicationDate
        {
            get { return _publicationDate; }
            set { _publicationDate = value; }
        }

        [JsonProperty("publicationfrequency")]
        private string _publicationFrequency = null;

        [JsonIgnore]
        public string PublicationFrequency
        {
            get { return _publicationFrequency; }
            set { _publicationFrequency = value; }
        }

        [JsonProperty("bibliographiclevel")]
        private string _bibliographicLevel = null;

        [JsonIgnore]
        public string BibliographicLevel
        {
            get { return _bibliographicLevel; }
            set { _bibliographicLevel = value; }
        }

        [JsonProperty("keywords")]
        private IEnumerable<Subject> _subjects;

        [JsonIgnore]
        public IEnumerable<Subject> Subjects {
            get { return _subjects; }
            set { _subjects = value; }
        }

        [JsonProperty("authors")]
        private IEnumerable<Author> _authors;

        [JsonIgnore]
        public IEnumerable<Author> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        [JsonProperty("identifiers")]
        private IEnumerable<Identifier> _identifiers;

        [JsonIgnore]
        public IEnumerable<Identifier> Identifiers
        {
            get { return _identifiers; }
            set { _identifiers = value; }
        }

        #endregion Properties

    }
}