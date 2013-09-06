
-- ItemLanguageUpdateAuto PROCEDURE
-- Generated 2/4/2011 12:08:43 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for ItemLanguage

CREATE PROCEDURE ItemLanguageUpdateAuto

@ItemLanguageID INT,
@ItemID INT,
@LanguageCode NVARCHAR(10)

AS 

SET NOCOUNT ON

UPDATE [dbo].[ItemLanguage]

SET

	[ItemID] = @ItemID,
	[LanguageCode] = @LanguageCode

WHERE
	[ItemLanguageID] = @ItemLanguageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemLanguageUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ItemLanguageID],
		[ItemID],
		[LanguageCode],
		[CreationDate],
		[CreationUserID]

	FROM [dbo].[ItemLanguage]
	
	WHERE
		[ItemLanguageID] = @ItemLanguageID
	
	RETURN -- update successful
END

