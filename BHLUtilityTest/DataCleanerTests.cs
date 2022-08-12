using MOBOT.BHL.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BHLUtilityTest
{
    [TestClass()]
    public class DataCleanerTests
    {
        [TestMethod]
        public void ValidateItemYearTest001()
        {
            bool expected = true;
            bool actual = DataCleaner.ValidateItemYear("1900");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValidateItemYearTest002()
        {
            bool expected = true;
            bool actual = DataCleaner.ValidateItemYear("1900-1901");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValidateItemYearTest003()
        {
            bool expected = true;
            bool actual = DataCleaner.ValidateItemYear("1900,1901");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValidateItemYearTest004()
        {
            bool expected = true;
            bool actual = DataCleaner.ValidateItemYear("1900/1901");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValidateItemYearTest005()
        {
            bool expected = false;
            bool actual = DataCleaner.ValidateItemYear("[1900]");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValidateItemYearTest006()
        {
            bool expected = false;
            bool actual = DataCleaner.ValidateItemYear("19XX");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValidateItemYearTest007()
        {
            bool expected = false;
            bool actual = DataCleaner.ValidateItemYear("19??");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValidateItemYearTest008()
        {
            bool expected = false;
            bool actual = DataCleaner.ValidateItemYear("2001:Jan-Sept.");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValidateItemYearTest009()
        {
            bool expected = false;
            bool actual = DataCleaner.ValidateItemYear("April 2001");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValidateItemYearTest010()
        {
            bool expected = false;
            bool actual = DataCleaner.ValidateItemYear("c1901");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParseYearStringTest001()
        {
            RunParseYearStringTest(
                year: "1901",
                startYear: "1901",
                endYear: "");
        }

        [TestMethod]
        public void ParseYearStringTest002()
        {
            RunParseYearStringTest(
                year: "1901-1902",
                startYear: "1901",
                endYear: "1902");
        }

        [TestMethod]
        public void ParseYearStringTest003()
        {
            RunParseYearStringTest(
                year: "1901,1902",
                startYear: "1901",
                endYear: "1902");
        }

        [TestMethod]
        public void ParseYearStringTest004()
        {
            RunParseYearStringTest(
                year: "1901/1902",
                startYear: "1901",
                endYear: "1902");
        }

        [TestMethod]
        public void ParseVolumeStringTest001()
        {
            RunParseVolumeStringTest(
                volumeString: "v1",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest002()
        {
            RunParseVolumeStringTest(
                volumeString: "v.A pt.0",
                startVolume: "A", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "0", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest003()
        {
            RunParseVolumeStringTest(
                volumeString: "1,pts.2-3",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "3",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest004()
        {
            RunParseVolumeStringTest(
                volumeString: "Bd 10 pt.1",
                startVolume: "10", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "1", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest005()
        {
            RunParseVolumeStringTest(
                volumeString: "1 pt.2-3",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "3",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest006()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1pts.2-3(1900)",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "3",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest007()
        {
            RunParseVolumeStringTest(
                volumeString: "1 pt. 2",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest008()
        {
            RunParseVolumeStringTest(
                volumeString: "v.I: no.1",
                startVolume: "I", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "1", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest009()
        {
            RunParseVolumeStringTest(
                volumeString: "Bd.1;pt.2;no.3",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "",
                startNumber: "3", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest010()
        {
            RunParseVolumeStringTest(
                volumeString: "10, no.11",
                startVolume: "10", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "11", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest011()
        {
            RunParseVolumeStringTest(
                volumeString: "v. II, part 2",
                startVolume: "II", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest012()
        {
            RunParseVolumeStringTest(
                volumeString: "1, pts.2-3",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "3",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest013()
        {
            RunParseVolumeStringTest(
                volumeString: "v 10, heft. 11-222",
                startVolume: "10", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "11", endNumber: "222", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest014()
        {
            RunParseVolumeStringTest(
                volumeString: "new ser. v. 1 1900",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "new", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest015()
        {
            RunParseVolumeStringTest(
                volumeString: "2nd ser. v.1 1900",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "2", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest016()
        {
            RunParseVolumeStringTest(
                volumeString: "3rd ser. v. 1 1900-01",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "1901",
                startSeries: "3", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest017()
        {
            RunParseVolumeStringTest(
                volumeString: "3rd ser. v. 10 (1900)",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "3", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest018()
        {
            RunParseVolumeStringTest(
                volumeString: "new ser.:v.10=no.100-101 (1900-1901)",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "1901",
                startSeries: "new", endSeries: "", startPart: "", endPart: "",
                startNumber: "100", endNumber: "101", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest019()
        {
            RunParseVolumeStringTest(
                volumeString: "10 (New Series)",
                startVolume: "10", endVolume: "", startYear: "", endYear: "",
                startSeries: "New", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest020()
        {
            RunParseVolumeStringTest(
                volumeString: "10 (4th Series)",
                startVolume: "10", endVolume: "", startYear: "", endYear: "",
                startSeries: "4", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest021()
        {
            RunParseVolumeStringTest(
                volumeString: "new ser. 1 (1900)",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "new", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest022()
        {
            RunParseVolumeStringTest(
                volumeString: "vol. 1/no. 2",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest023()
        {
            RunParseVolumeStringTest(
                volumeString: "Box 7: Folder 13: Cactaceae: 1862",
                startVolume: "", endVolume: "", startYear: "1862", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest024()
        {
            RunParseVolumeStringTest(
                volumeString: "Box 7: Folder 14: Cactaceae: 1873-1883",
                startVolume: "", endVolume: "", startYear: "1873", endYear: "1883",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest025()
        {
            RunParseVolumeStringTest(
                volumeString: "10 1900-2000",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest026()
        {
            RunParseVolumeStringTest(
                volumeString: "10 1900 - 2000",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest027()
        {
            RunParseVolumeStringTest(
                volumeString: "10, 1900-2000",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest028()
        {
            RunParseVolumeStringTest(
                volumeString: "10 (1900-2000)",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest029()
        {
            RunParseVolumeStringTest(
                volumeString: "1-2, 1900-2000",
                startVolume: "1", endVolume: "2", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest030()
        {
            RunParseVolumeStringTest(
                volumeString: "10-100, 1900-2000",
                startVolume: "10", endVolume: "100", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest031()
        {
            RunParseVolumeStringTest(
                volumeString: "10 1900",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest032()
        {
            RunParseVolumeStringTest(
                volumeString: "10, 1900",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest033()
        {
            RunParseVolumeStringTest(
                volumeString: "10 (1900)",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest034()
        {
            RunParseVolumeStringTest(
                volumeString: "v.10-11 1900-2000",
                startVolume: "10", endVolume: "11", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest035()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1-10 1900-2000",
                startVolume: "1", endVolume: "10", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest036()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1-2 1900-2000",
                startVolume: "1", endVolume: "2", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest037()
        {
            RunParseVolumeStringTest(
                volumeString: "v.10-11 (1900-2000)",
                startVolume: "10", endVolume: "11", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest038()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1-10 (1900-2000)",
                startVolume: "1", endVolume: "10", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest039()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1-2 (1900-2000)",
                startVolume: "1", endVolume: "2", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest040()
        {
            RunParseVolumeStringTest(
                volumeString: "jahrg 10-11 1900-2000",
                startVolume: "10", endVolume: "11", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest041()
        {
            RunParseVolumeStringTest(
                volumeString: "v 1 1900",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest042()
        {
            RunParseVolumeStringTest(
                volumeString: "v 1-2 (1900-2000)",
                startVolume: "1", endVolume: "2", startYear: "1900", endYear: "2000",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest043()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1 (1900)",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest044()
        {
            RunParseVolumeStringTest(
                volumeString: "v.10 (1900)",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest045()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1:no.2 (1900)",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest046()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1:no.2 (1900-1901)",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "1901",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest047()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1:no.2 (1900:march)",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest048()
        {
            RunParseVolumeStringTest(
                volumeString: "heft.1-10 1900-1901",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "1901",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "1", endNumber: "10", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest049()
        {
            RunParseVolumeStringTest(
                volumeString: "heft.1-10 (1900-1901)",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "1901",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "1", endNumber: "10", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest050()
        {
            RunParseVolumeStringTest(
                volumeString: "10000",
                startVolume: "10000", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest051()
        {
            RunParseVolumeStringTest(
                volumeString: "100",
                startVolume: "100", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest052()
        {
            RunParseVolumeStringTest(
                volumeString: "10",
                startVolume: "10", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest053()
        {
            RunParseVolumeStringTest(
                volumeString: "1",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest054()
        {
            RunParseVolumeStringTest(
                volumeString: "1-2",
                startVolume: "1", endVolume: "2", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest055()
        {
            RunParseVolumeStringTest(
                volumeString: "10-11",
                startVolume: "10", endVolume: "11", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest056()
        {
            RunParseVolumeStringTest(
                volumeString: "100-101",
                startVolume: "100", endVolume: "101", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest057()
        {
            RunParseVolumeStringTest(
                volumeString: "v. IV",
                startVolume: "IV", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest058()
        {
            RunParseVolumeStringTest(
                volumeString: "v. 1",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest059()
        {
            RunParseVolumeStringTest(
                volumeString: "Band II",
                startVolume: "II", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest060()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest061()
        {
            RunParseVolumeStringTest(
                volumeString: "bd 1",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest062()
        {
            RunParseVolumeStringTest(
                volumeString: "vol.1",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest063()
        {
            RunParseVolumeStringTest(
                volumeString: "Tom 1 - Tom 2",
                startVolume: "1", endVolume: "2", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest064()
        {
            RunParseVolumeStringTest(
                volumeString: "Tom. 10 - Tom. 11",
                startVolume: "10", endVolume: "11", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest065()
        {
            RunParseVolumeStringTest(
                volumeString: "Tome 1 - Tome 2",
                startVolume: "1", endVolume: "2", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest066()
        {
            RunParseVolumeStringTest(
                volumeString: "Tome 1",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest067()
        {
            RunParseVolumeStringTest(
                volumeString: "Tom. 100",
                startVolume: "100", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest068()
        {
            RunParseVolumeStringTest(
                volumeString: "1900 January",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest069()
        {
            RunParseVolumeStringTest(
                volumeString: "1900:winter",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest070()
        {
            RunParseVolumeStringTest(
                volumeString: "1900 (july)",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest071()
        {
            RunParseVolumeStringTest(
                volumeString: "Shaffer to Engelmann, 1900",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest072()
        {
            RunParseVolumeStringTest(
                volumeString: "v1:no.2 (1900)",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest073()
        {
            RunParseVolumeStringTest(
                volumeString: "v1:no.2 (1900:Jun.)",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest074()
        {
            RunParseVolumeStringTest(
                volumeString: "v 10, heft. 99-100",
                startVolume: "10", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "99", endNumber: "100", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest075()
        {
            RunParseVolumeStringTest(
                volumeString: "v  10, heft. 98-99",
                startVolume: "10", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "98", endNumber: "99", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest076()
        {
            RunParseVolumeStringTest(
                volumeString: "Bd.1:Abt.2b",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest077()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1:pt.2:no.3",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "",
                startNumber: "3", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest078()
        {
            RunParseVolumeStringTest(
                volumeString: "Bd.1;pt.2;no.3",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "",
                startNumber: "3", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest079()
        {
            RunParseVolumeStringTest(
                volumeString: "vol. 1; pt. 2",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest080()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1:no.9-10:index",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "9", endNumber: "10", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest081()
        {
            RunParseVolumeStringTest(
                volumeString: "fasc. 10 oct. 1900",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "10", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest082()
        {
            RunParseVolumeStringTest(
                volumeString: "ser. 1 no. 2 nov. 1900",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "1", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest083()
        {
            RunParseVolumeStringTest(
                volumeString: "100, rev. 10",
                startVolume: "100", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest084()
        {
            RunParseVolumeStringTest(
                volumeString: "100 rev. 10",
                startVolume: "100", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest085()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1:no.2/3",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "3", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest086()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1,no.2(Sept 1900)",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest087()
        {
            RunParseVolumeStringTest(
                volumeString: "v. 1 Apr.-Sept. 1900",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest088()
        {
            RunParseVolumeStringTest(
                volumeString: "v. 1 no. 2 Sept 1900",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest089()
        {
            RunParseVolumeStringTest(
                volumeString: "Jahrg. 10 no. 1 Sept 1900",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "1", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest090()
        {
            RunParseVolumeStringTest(
                volumeString: "no. 100 Sept 1900",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "100", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest091()
        {
            RunParseVolumeStringTest(
                volumeString: "Vol. 10, no. 1 (Sept. 1900)",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "1", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest092()
        {
            RunParseVolumeStringTest(
                volumeString: "n.s. v.1 (o.s. v. 10) (April-Sept. 1900)",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest093()
        {
            RunParseVolumeStringTest(
                volumeString: "Sept 1900 annual number",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest094()
        {
            RunParseVolumeStringTest(
                volumeString: "Sept 1900",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest095()
        {
            RunParseVolumeStringTest(
                volumeString: "v. 1 (July-Sept. 1900)",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest096()
        {
            RunParseVolumeStringTest(
                volumeString: "agosto-sept 1900",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest097()
        {
            RunParseVolumeStringTest(
                volumeString: "v.1. no.2 Sept. 1900",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest098()
        {
            RunParseVolumeStringTest(
                volumeString: "Sept. 1900 Preliminary Price List to Dealers Only",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest099()
        {
            RunParseVolumeStringTest(
                volumeString: "no.100c",
                startVolume: "", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "100", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest100()
        {
            RunParseVolumeStringTest(
                volumeString: "No. 100D",
                startVolume: "", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "100", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest101()
        {
            RunParseVolumeStringTest(
                volumeString: "1-2, 1900",
                startVolume: "1", endVolume: "2", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest102()
        {
            RunParseVolumeStringTest(
                volumeString: "1/2",
                startVolume: "1", endVolume: "2", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest103()
        {
            RunParseVolumeStringTest(
                volumeString: "1900-1",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "1901",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest104()
        {
            RunParseVolumeStringTest(
                volumeString: "d.10(1900)",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest105()
        {
            RunParseVolumeStringTest(
                volumeString: "t.100:ent.1 - 2(1900)",
                startVolume: "100", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest106()
        {
            RunParseVolumeStringTest(
                volumeString: "1:2",
                startVolume: "1", endVolume: "2", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest107()
        {
            RunParseVolumeStringTest(
                volumeString: "1 fascicle 2, 1900",
                startVolume: "1", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "2", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest108()
        {
            RunParseVolumeStringTest(
                volumeString: "v.10:n.2 (1900:May)",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "2", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest109()
        {
            RunParseVolumeStringTest(
                volumeString: "Jahr.10 (1900)",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest110()
        {
            RunParseVolumeStringTest(
                volumeString: "Tome 1 (Series 2)",
                startVolume: "1", endVolume: "", startYear: "", endYear: "",
                startSeries: "2", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest111()
        {
            RunParseVolumeStringTest(
                volumeString: "Issue 10 (1900)",
                startVolume: "", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "10", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest112()
        {
            RunParseVolumeStringTest(
                volumeString: "v.10 iss.100 (1900)",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "100", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest113()
        {
            // Make sure we recognize the brackets around the numbers
            RunParseVolumeStringTest(
                volumeString: "Bd.10=Heft.[1 - 11] (1900)",
                startVolume: "10", endVolume: "", startYear: "1900", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "1", endNumber: "11", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest114()
        {
            RunParseVolumeStringTest(
                volumeString: "PA-3000",
                startVolume: "", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest115()
        {
            RunParseVolumeStringTest(
                volumeString: "3910",
                startVolume: "3910", endVolume: "", startYear: "", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        [TestMethod]
        public void ParseVolumeStringTest116()
        {
            RunParseVolumeStringTest(
                volumeString: "1907",
                startVolume: "", endVolume: "", startYear: "1907", endYear: "",
                startSeries: "", endSeries: "", startPart: "", endPart: "",
                startNumber: "", endNumber: "", startIssue: "", endIssue: "");
        }

        #region ParseVolumeStringTest supporting methods

        private void RunParseVolumeStringTest(string volumeString, string startVolume,
            string endVolume, string startYear, string endYear, string startSeries,
            string endSeries, string startPart, string endPart, string startNumber,
            string endNumber, string startIssue, string endIssue)
        {
            VolumeData v = DataCleaner.ParseVolumeString(volumeString);

            string expected = ConvertVolumeDataToString(volumeString, startVolume,
                endVolume, startYear, endYear, startSeries, endSeries, startPart,
                endPart, startNumber, endNumber, startIssue, endIssue);

            string actual = ConvertVolumeDataToString(v.Volume, v.StartVolume,
                v.EndVolume, v.StartYear, v.EndYear, v.StartSeries, v.EndSeries, v.StartPart,
                v.EndPart, v.StartNumber, v.EndNumber, v.StartIssue, v.EndIssue);

            Assert.AreEqual(expected, actual);
        }

        private void RunParseYearStringTest(string year, string startYear, string endYear)
        {
            YearData y = DataCleaner.ParseYearString(year);

            string expected = ConvertYearDataToString(year, startYear, endYear);
            string actual = ConvertYearDataToString(y.Year, y.StartYear, y.EndYear);

            Assert.AreEqual(expected, actual);
        }

        private string ConvertVolumeDataToString(string volumeString, string startVolume,
            string endVolume, string startYear, string endYear, string startSeries,
            string endSeries, string startPart, string endPart, string startNumber,
            string endNumber, string startIssue, string endIssue)
        {
            return string.Format(
                "Volume: {0}\n" +
                "SVol: {1}\n" +
                "EVol: {2}\n" +
                "SYr: {3}\n" +
                "EYr: {4}\n" +
                "SSer: {5}\n" +
                "ESer: {6}\n" +
                "SPt: {7}\n" +
                "EPt: {8}\n" +
                "SNo: {9}\n" +
                "ENo: {10}\n" +
                "SIss: {11}\n" +
                "EIss: {12}\n",
                volumeString, startVolume, endVolume, startYear, endYear, startSeries,
                endSeries, startPart, endPart, startNumber, endNumber, startIssue,
                endIssue);
        }

        private string ConvertYearDataToString(string year, string startYear, string endYear)
        {
            return string.Format(
                "Year: {0}\n" +
                "SYr: {1}\n" +
                "EYr: {2}\n",
                year, startYear, endYear);
        }

        #endregion ParseVolumeStringTest supporting methods

        [TestMethod]
        public void CleanSortTitleTest1()
        {
            string expected = "Annales de l Institut Pasteur";
            string actual = DataCleaner.CleanSortTitle("Annales de l'Institut Pasteur.");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanSortTitleTest2()
        {
            string expected = "Atlas des champignons comestibles et ve ne neux";
            string actual = DataCleaner.CleanSortTitle("Atlas des champignons comestibles et ve'ne'neux.");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanSortTitleTest3()
        {
            string expected = "Our reptiles and batrachians a plain and easy account of the lizards snakes newts toads frogs and tortoises indigenous to Great Britain by MCCooke with original figures of every species and numerous woodcuts";
            string actual = DataCleaner.CleanSortTitle("Our reptiles and batrachians : a plain and easy account of the lizards, snakes, newts, toads, frogs and tortoises indigenous to Great Britain / by M.C.Cooke... with original figures of every species and numerous woodcuts.");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanSortTitleTest4()
        {
            string expected = "Man s place in nature and other anthropological essays by Thomas H Huxley";
            string actual = DataCleaner.CleanSortTitle("Man's place in nature : and other anthropological essays / by Thomas H. Huxley.");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanSortTitleTest5()
        {
            string expected = "Year book Carnegie Institution of Washington";
            string actual = DataCleaner.CleanSortTitle("Year book -Carnegie Institution of Washington.");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanSortTitleTest6()
        {
            string expected = "a a";
            string actual = DataCleaner.CleanSortTitle("a'-/\\a");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanSortTitleTest7()
        {
            string expected = "";
            string actual = DataCleaner.CleanSortTitle(".,;:()[]{}<>\"!?");
            Assert.AreEqual(expected, actual);
        }
    }
}
