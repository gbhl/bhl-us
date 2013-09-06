
-- TitleLanguageInsertAuto PROCEDURE
-- Generated 2/4/2011 12:08:43 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleLanguage

CREATE PROCEDURE TitleLanguageInsertAuto

@TitleLanguageID INT OUTPUT,
@TitleID INT,
@LanguageCode NVARCHAR(10),
@CreationUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleLanguage]
(
	[TitleID],
	[LanguageCode],
	[CreationDate],
	[CreationUserID]
)
VALUES
(
	@TitleID,
	@LanguageCode,
	getdate(),
	@CreationUserID
)

SET @TitleLanguageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleLanguageInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleLanguageID],
		[TitleID],
		[LanguageCode],
		[CreationDate],
		[CreationUserID]	

	FROM [dbo].[TitleLanguage]
	
	WHERE
		[TitleLanguageID] = @TitleLanguageID
	
	RETURN -- insert successful
END

