
-- MarcSelectAuto PROCEDURE
-- Generated 4/21/2009 3:39:50 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Marc

CREATE PROCEDURE MarcSelectAuto

@MarcID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcID],
	[MarcImportStatusID],
	[MarcImportBatchID],
	[MarcFileLocation],
	[InstitutionCode],
	[Leader],
	[TitleID],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[Marc]

WHERE
	[MarcID] = @MarcID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

