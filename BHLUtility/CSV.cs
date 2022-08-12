using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.Utility
{
    public class CSV
    {
        /// <summary>
        /// True to include a Byte-Order-Mark (BOM) in CSV output, False to omit a BOM
        /// </summary>
        private bool _includeBOM = true;
        public bool IncludeBOM { get => _includeBOM; set => _includeBOM = value; }
        
        /// <summary>
        /// True if the data should/does include a header, False if not
        /// </summary>
        private bool _hasHeaderRecord = true;
        public bool HasHeaderRecord { get => _hasHeaderRecord; set => _hasHeaderRecord = value; }

        /// <summary>
        /// True if headers should be validated when reading CSV data
        /// </summary>
        private bool _validateHeader = true;
        public bool ValidateHeader { get => _validateHeader; set => _validateHeader = value; }

        /// <summary>
        /// True if missing fields should be flagged when reading CSV data.
        /// </summary>
        private bool _checkForMissingFields = true;
        public bool CheckForMissingFields { get => _checkForMissingFields; set => _checkForMissingFields = value; }

        /// <summary>
        /// Set the Culture for the CSV data
        /// </summary>
        public CultureInfo CultureInfo { get => _cultureInfo; set => _cultureInfo = value; }

        private CultureInfo _cultureInfo = CultureInfo.InvariantCulture;

        public CSV(bool includeBOM = true, 
            bool hasHeaderRecord = true, 
            bool validateHeader = true, 
            bool checkForMissingFields = true)
        {
            _includeBOM = includeBOM;
            _hasHeaderRecord = hasHeaderRecord;
            ValidateHeader = validateHeader;
            CheckForMissingFields = checkForMissingFields;
        }

        /// <summary>
        /// Returns the specified data formatted as a CSV string.  Create the list of data by doing something like the following:
        ///     var data = new List<dynamic>();
        ///     dynamic record = new System.Dynamic.ExpandoObject();
        ///     record.Id = 1;
        ///     record.Name = "one";
        ///     data.Add(record);
        ///     
        ///     or
        ///     
        ///     var data = new List<dynamic>();
        ///     var record = new ExpandoObject() as IDictionary<string, Object>;
        ///     record.Add("Id", 1);
        ///     record.Add("Name", "one");
        ///     data.Add(record);
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] FormatCSVData(List<dynamic> data)
        {
            byte[] output = null;

            using (var writer = new StringWriter())
            {
                CsvConfiguration config = new CsvConfiguration(_cultureInfo)
                {
                    HasHeaderRecord = _hasHeaderRecord,
                };
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(data);
                }

                output = Encoding.UTF8.GetBytes(writer.ToString());
                if (_includeBOM) output = Encoding.UTF8.GetPreamble().Concat(output).ToArray();
            }

            return output;
        }

        /// <summary>
        /// Read CSV data from the specified file.  The recordFormat argument specifies the format of the rows in the file.
        /// An example of how to create a recordFormat is:
        ///            var recordFormat = new
        ///            {
        ///               PageID = string.Empty,
        ///               SequenceNumber = string.Empty,
        ///               Text = string.Empty
        ///            };
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="recordFormat"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> ReadCSVData(string filename, dynamic recordFormat)
        {
            IEnumerable<dynamic> data = null;

            using (StreamReader reader = File.OpenText(filename))
            {
                var config = new CsvConfiguration(_cultureInfo)
                {
                    HasHeaderRecord = _hasHeaderRecord,
                };
                if (!_validateHeader) config.HeaderValidated = null;
                if (!_checkForMissingFields) config.MissingFieldFound = (_1) => { };

                CsvReader csv = new CsvReader(reader, config);
                data = csv.GetRecords(recordFormat);
            }

            return data;
        }
    }
}
