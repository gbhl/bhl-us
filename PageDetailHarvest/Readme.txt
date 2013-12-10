December 10, 2013

The code for this service is non-standard, compared to the rest of the BHL code base.

Wait until this data harvest service is ready for ongoing production use before taking
the time to standarize the code.

To align the code for this utility with the rest of the BHL code base, move the
PageClassifierExport, PageDetail, and PageIllustration classes into the BHLDataObjects
project/assembly.  Likewise, move the data access methods contained in the 
HarvestProcessor class to the BHLCoreDAL project/assembly.  Wrappers for the data
access methods should then be added to the BHLProvider project/assembly.
