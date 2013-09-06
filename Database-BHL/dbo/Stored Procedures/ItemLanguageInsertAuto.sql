
-- ItemLanguageInsertAuto PROCEDURE
-- Generated 2/4/2011 12:08:43 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for ItemLanguage

CREATE PROCEDURE ItemLanguageInsertAuto

@ItemLanguageID INT OUTPUT,
@ItemID INT,
@LanguageCode NVARCHAR(10),
@CreationUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[ItemLanguage]
(
	[ItemID],
	[LanguageCode],
	[CreationDate],
	[CreationUserID]
)
VALUES
(
	@ItemID,
	@LanguageCode,
	getdate(),
	@CreationUserID
)

SET @ItemLanguageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemLanguageInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

