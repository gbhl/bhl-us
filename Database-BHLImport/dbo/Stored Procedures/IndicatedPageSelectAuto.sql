
-- IndicatedPageSelectAuto PROCEDURE
-- Generated 1/16/2008 1:54:48 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IndicatedPage

CREATE PROCEDURE IndicatedPageSelectAuto

@IndicatedPageID INT

AS 

SET NOCOUNT ON

SELECT 

	[IndicatedPageID],
	[BarCode],
	[FileNamePrefix],
	[SequenceOrder],
	[Sequence],
	[ImportStatusID],
	[ImportSourceID],
	[PagePrefix],
	[PageNumber],
	[Implied],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ExternalCreationUser],
	[ExternalLastModifiedUser],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IndicatedPage]

WHERE
	[IndicatedPageID] = @IndicatedPageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IndicatedPageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

