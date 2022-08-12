﻿using CustomDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBOT.BHL.DataObjects
{
    public class RISCitation : ISetValues
    {
        private string _genre = string.Empty;
        public string Genre {
            get { return _genre; }
            set { _genre = value; }
        }

        private string _title = string.Empty;
        public string Title {
            get { return _title; }
            set { _title = value; }
        }

        private string _journal = string.Empty;
        public string Journal {
            get { return _journal; }
            set { _journal = value.Replace("*", ""); }
        }

        private string _volume = string.Empty;
        public string Volume {
            get { return _volume; }
            set { _volume = value; }
        }

        private string _issue = string.Empty;
        public string Issue {
            get { return _issue; }
            set { _issue = value; }
        }

        private string _url = string.Empty;
        public string Url {
            get { return _url; }
            set { _url = value; }
        }

        private string _publisher = string.Empty;
        public string Publisher {
            get { return _publisher; }
            set { _publisher = value; }
        }

        private string _publicationPlace = string.Empty;
        public string PublicationPlace {
            get { return _publicationPlace; }
            set { _publicationPlace = value; }
        }

        private string _year = string.Empty;
        public string Year {
            get { return _year; }
            set { _year = value; }
        }

        private List<string> _authors = new List<string>();
        public List<string> Authors {
            get { return _authors; }
            set {
                foreach(string author in value)
                {
                    // Remove '*' characters and limit length to 255 characters
                    if (!string.IsNullOrWhiteSpace(author))
                    {
                        int len = author.Replace("*", "").Length;
                        _authors.Add(author.Replace("*", "").Substring(0, Math.Min(len, 255)));
                    }
                }
            }
        }

        private List<string> _keywords = new List<string>();
        public List<string> Keywords {
            get { return _keywords; }
            set {
                foreach(string keyword in value)
                {
                    // Remove '*' characters and limit length to 255 characters
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        int len = keyword.Replace("*", "").Length;
                        _keywords.Add(keyword.Replace("*", "").Substring(0, Math.Min(len, 255)));
                    }
                }
            }
        }

        private string _callNumber = string.Empty;
        public string CallNumber {
            get { return _callNumber; }
            set { _callNumber = value; }
        }

        private string _doi = string.Empty;
        public string Doi {
            get { return _doi; }
            set { _doi = value; }
        }

        private string _edition = string.Empty;
        public string Edition {
            get { return _edition; }
            set { _edition = value; }
        }

        private string _issn = string.Empty;
        public string Issn {
            get { return _issn; }
            set { _issn = value; }
        }

        private string _eissn = string.Empty;
        public string Eissn
        {
            get { return _eissn; }
            set { _eissn = value; }
        }

        private string _isbn = string.Empty;
        public string Isbn
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        private string _language = string.Empty;
        public string Language {
            get { return _language; }
            set { _language = value; }
        }

        private string _notes = string.Empty;
        public string Notes {
            get { return _notes; }
            set { _notes = value; }
        }

        private string _abstract = string.Empty;
        public string Abstract
        {
            get { return _abstract; }
            set { _abstract = value; }
        }

        private string _startPage = string.Empty;
        public string StartPage {
            get { return _startPage; }
            set { _startPage = value; }
        }

        private string _endPage = string.Empty;
        public string EndPage
        {
            get { return _endPage; }
            set { _endPage = value; }
        }

        private bool _hasLocalContent = true;

        public bool HasLocalContent
        {
            get { return _hasLocalContent; }
            set { _hasLocalContent = value; }
        }

        private bool _hasExternalContent = false;

        public bool HasExternalContent
        {
            get { return _hasExternalContent; }
            set { _hasExternalContent = value; }
        }

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "Genre":
                        {
                            Genre = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Title":
                        {
                            Title = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Journal":
                        {
                            Journal = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Volume":
                        {
                            Volume = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Issue":
                        {
                            Issue = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Url":
                        {
                            Url = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Publisher":
                        {
                            Publisher = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "PublicationPlace":
                        {
                            PublicationPlace = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Year":
                        {
                            Year = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Authors":
                        {
                            Authors = Utility.EmptyIfNull((string)column.Value).Split('|').ToList<string>();
                            break;
                        }
                    case "Keywords":
                        {
                            Keywords = Utility.EmptyIfNull((string)column.Value).Split('|').ToList<string>();
                            break;
                        }

                    case "CallNumber":
                        {
                            CallNumber = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "DOI":
                        {
                            Doi = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Edition":
                        {
                            Edition = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "ISSN":
                        {
                            Issn = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "EISSN":
                        {
                            Eissn = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "ISBN":
                        {
                            Isbn = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Language":
                        {
                            Notes = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Notes":
                        {
                            Notes = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "Abstract":
                        {
                            Abstract = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "StartPage":
                        {
                            StartPage = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "EndPage":
                        {
                            EndPage = Utility.EmptyIfNull((string)column.Value);
                            break;
                        }
                    case "HasLocalContent":
                        {
                            _hasLocalContent = Convert.ToInt16(column.Value) == 1;
                            break;
                        }
                    case "HasExternalContent":
                        {
                            _hasExternalContent = Convert.ToInt16(column.Value) == 1;
                            break;
                        }
                }
            }
        }
    }
}
