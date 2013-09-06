
-- TitleLanguageUpdateAuto PROCEDURE
-- Generated 2/4/2011 12:08:43 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleLanguage

CREATE PROCEDURE TitleLanguageUpdateAuto

@TitleLanguageID INT,
@TitleID INT,
@LanguageCode NVARCHAR(10)

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleLanguage]

SET

	[TitleID] = @TitleID,
	[LanguageCode] = @LanguageCode

WHERE
	[TitleLanguageID] = @TitleLanguageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleLanguageUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

