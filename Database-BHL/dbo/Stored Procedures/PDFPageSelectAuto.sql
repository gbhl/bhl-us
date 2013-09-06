
-- PDFPageSelectAuto PROCEDURE
-- Generated 11/24/2008 4:39:21 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for PDFPage

CREATE PROCEDURE PDFPageSelectAuto

@PdfPageID INT

AS 

SET NOCOUNT ON

SELECT 

	[PdfPageID],
	[PdfID],
	[PageID]

FROM [dbo].[PDFPage]

WHERE
	[PdfPageID] = @PdfPageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PDFPageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

