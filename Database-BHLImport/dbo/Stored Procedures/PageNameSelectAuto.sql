
-- PageNameSelectAuto PROCEDURE
-- Generated 1/16/2008 1:54:48 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for PageName

CREATE PROCEDURE PageNameSelectAuto

@PageNameID INT

AS 

SET NOCOUNT ON

SELECT 

	[PageNameID],
	[ImportStatusID],
	[ImportSourceID],
	[BarCode],
	[FileNamePrefix],
	[SequenceOrder],
	[Source],
	[NameFound],
	[NameConfirmed],
	[NameBankID],
	[Active],
	[ExternalCreateDate],
	[ExternalLastUpdateDate],
	[IsCommonName],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[PageName]

WHERE
	[PageNameID] = @PageNameID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageNameSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

