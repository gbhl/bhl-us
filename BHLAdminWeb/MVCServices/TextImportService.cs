﻿using BHL.TextImportUtility;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOBOT.BHL.AdminWeb.MVCServices
{
    public class TextImportService
    {
        public Dictionary<string, string> FileFormats = new Dictionary<string, string> {
            { "", "Unknown" },
            {"dv", "DigiVol" },
            {"ftp", "From The Page" },
            {"stc", "Smithsonian Transcription Center" }
        };

        public static Dictionary<int, string> TextImportBatchStatuses = new Dictionary<int, string>
        {
            { 10, "New" },
            { 20, "Queued" },
            { 30, "Processing" },
            { 40, "Imported" },
            { 50, "Rejected" }
        };

        public static Dictionary<int, string> TextImportBatchFileStatuses = new Dictionary<int, string>
        {
            { 10, "Ready to Import" },
            { 20, "Must Review" },
            { 30, "Imported" },
            { 40, "Rejected" },
            { 50, "Error" }
        };

        public class ImportBatchException : Exception
        {
            public ImportBatchException() : base() { }
            public ImportBatchException(string message) : base(message) { }
            public ImportBatchException(string message, System.Exception inner) : base(message, inner) { }
        }

        public class ImportFileException : Exception
        {
            public ImportFileException() : base() { }
            public ImportFileException(string message) : base(message) { }
            public ImportFileException(string message, System.Exception inner) : base(message, inner) { }
        }

        public static int GetTextImportBatchStatusNew()
        {
            return TextImportBatchStatuses.Where(s => s.Value == "New").Select(k => k.Key).First();
        }

        public static int GetTextImportBatchStatusQueued()
        {
            return TextImportBatchStatuses.Where(s => s.Value == "Queued").Select(k => k.Key).First();
        }

        public static int GetTextImportBatchStatusRejected()
        {
            return TextImportBatchStatuses.Where(s => s.Value == "Rejected").Select(k => k.Key).First();
        }

        public static int GetTextImportBatchFileStatusReady()
        {
            return TextImportBatchFileStatuses.Where(s => s.Value == "Ready to Import").Select(k => k.Key).First();
        }

        public static int GetTextImportBatchFileStatusReview()
        {
            return TextImportBatchFileStatuses.Where(s => s.Value == "Must Review").Select(k => k.Key).First();
        }

        public static int GetTextImportBatchFileStatusError()
        {
            return TextImportBatchFileStatuses.Where(s => s.Value == "Error").Select(k => k.Key).First();
        }

        public string GetFileFormat(string savedFileName)
        {
            return new TextImportTool().GetFileFormat(savedFileName);
        }

        public string GetFileFormatValue(string fileFormatKey)
        {
            return FileFormats[fileFormatKey];
        }

        /// <summary>
        /// Provide the list of institutions (read from the DB) to be used during the text import process
        /// </summary>
        /// <returns></returns>
        public List<Institution> InstitutionList()
        {
            BHLProvider provider = new BHLProvider();
            return provider.InstituationSelectAll();
        }

        public List<TextImportBatchStatus> TextImportBatchStatusList()
        {
            List<TextImportBatchStatus> statusList = new BHLProvider().TextImportBatchStatusSelectAll();

            return statusList;
        }

        public Dictionary<string, string> TextImportBatchFileStatusList()
        {
            Dictionary<string, string> statusList = new Dictionary<string, string>();

            List<TextImportBatchFileStatus> statuses = new BHLProvider().TextImportBatchFileStatusSelectAll();
            foreach (TextImportBatchFileStatus status in statuses)
            {
                if (status.StatusName != "Imported") statusList.Add(status.TextImportBatchFileStatusID.ToString(), status.StatusName);
            }

            return statusList;
        }

        /// <summary>
        /// Provide the list of data sources to be used during the text import process
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ReportDateRangeList()
        {
            Dictionary<string, string> dataSourceTypes = new Dictionary<string, string>();
            dataSourceTypes.Add("1", "Last Day");
            dataSourceTypes.Add("30", "Last 30 Days");
            dataSourceTypes.Add("90", "Last 90 Days");
            dataSourceTypes.Add("180", "Last 180 Days");
            dataSourceTypes.Add("365", "Last Year");

            return dataSourceTypes;
        }
    }
}