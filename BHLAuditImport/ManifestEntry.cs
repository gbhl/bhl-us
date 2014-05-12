using System;

namespace MOBOT.BHL.BHLAuditImport
{
    class ManifestEntry : IComparable
    {
        public DateTime date { get; set; }
        public string filename { get; set; }
        public int numberOfFiles { get; set; }
        public Int64 filesize { get; set; }

        public ManifestEntry(string myDate, string myFilename, Int64  myfilesize, int myNumberOfFiles)
        {
            this.date = Convert.ToDateTime(myDate);
            this.filename = myFilename;
            this.numberOfFiles = myNumberOfFiles;
            this.filesize = myfilesize;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            ManifestEntry compareObject = obj as ManifestEntry;
            return this.date.CompareTo(compareObject.date);
        }
    }
}
