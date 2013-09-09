
-- TitleTagInsertAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleTag

CREATE PROCEDURE TitleTagInsertAuto

@TitleTagID INT OUTPUT,
@TagText NVARCHAR(50),
@ImportKey NVARCHAR(50),
@ImportStatusID INT,
@ImportSourceID INT = null,
@MarcDataFieldTag NVARCHAR(50) = null,
@MarcSubFieldCode NVARCHAR(50) = null,
@ExternalCreationDate DATETIME = null,
@ExternalLastModifiedDate DATETIME = null,
@ProductionDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleTag]
(
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
)
VALUES
(
	@TagText,
	@ImportKey,
	@ImportStatusID,
	@ImportSourceID,
	@MarcDataFieldTag,
	@MarcSubFieldCode,
	@ExternalCreationDate,
	@ExternalLastModifiedDate,
	@ProductionDate,
	getdate(),
	getdate()
)

SET @TitleTagID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleTagInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

