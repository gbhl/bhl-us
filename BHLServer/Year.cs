using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.Server
{
    public class Year
    {
        private string pageYear;
        private int beginningYear;
        private int endingYear;

        public Year(string pageYear)
        {
            this.pageYear = pageYear;
            if (this.pageYear == null)
                this.pageYear = "";

            Initialize();
        }

        public bool IsProvidedYearValidForPage(string providedYear)
        {
            if (providedYear == pageYear)
            {
                //regardless of range, if there is an exact match, return true
                return true;
            }
            else
            {
                try
                {
                    int providedYearInt = int.Parse(providedYear);
                    return (providedYearInt >= beginningYear && providedYearInt <= endingYear);
                }
                catch
                {
                    //assume that some part of our year or year range was not a numeric value and return false
                    return false;
                }
            }
        }

        private void Initialize()
        {
            try
            {
                string[] yearParts = pageYear.Split('-');
                if (yearParts.Length == 1)
                {
                    beginningYear = int.Parse(yearParts[0]);
                    endingYear = int.Parse(yearParts[0]);
                }
                else
                {
                    //check to make sure that the second year in the range is a four digit year
                    if (yearParts[1].Length == 2)
                    {
                        int firstTwoDigits = int.Parse(yearParts[0].Substring(0, 2));
                        int lastTwoDigitsOfFirstYear = int.Parse(yearParts[0].Substring(2, 2));
                        //adjust the first two digits for crossing centuries
                        if (lastTwoDigitsOfFirstYear > int.Parse(yearParts[1]))
                            firstTwoDigits++;
                        yearParts[1] = firstTwoDigits.ToString() + yearParts[1];
                    }
                    beginningYear = int.Parse(yearParts[0]);
                    endingYear = int.Parse(yearParts[1]);
                }
            }
            catch
            {
                //if our values were not valid integers, initialize to -1 (no matches expected)
                beginningYear = -1;
                endingYear = -1;
            }
        }
    }
}
