
-- TitleTagSelectAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleTag

CREATE PROCEDURE TitleTagSelectAuto

@TitleTagID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleTagID],
	[TagText],
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[MarcDataFieldTag],
	[MarcSubFieldCode],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[TitleTag]

WHERE
	[TitleTagID] = @TitleTagID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleTagSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

