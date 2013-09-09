
-- TitleTagUpdateAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleTag

CREATE PROCEDURE TitleTagUpdateAuto

@TitleTagID INT,
@TagText NVARCHAR(50),
@ImportKey NVARCHAR(50),
@ImportStatusID INT,
@ImportSourceID INT,
@MarcDataFieldTag NVARCHAR(50),
@MarcSubFieldCode NVARCHAR(50),
@ExternalCreationDate DATETIME,
@ExternalLastModifiedDate DATETIME,
@ProductionDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleTag]

SET

	[TagText] = @TagText,
	[ImportKey] = @ImportKey,
	[ImportStatusID] = @ImportStatusID,
	[ImportSourceID] = @ImportSourceID,
	[MarcDataFieldTag] = @MarcDataFieldTag,
	[MarcSubFieldCode] = @MarcSubFieldCode,
	[ExternalCreationDate] = @ExternalCreationDate,
	[ExternalLastModifiedDate] = @ExternalLastModifiedDate,
	[ProductionDate] = @ProductionDate,
	[LastModifiedDate] = getdate()

WHERE
	[TitleTagID] = @TitleTagID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleTagUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

