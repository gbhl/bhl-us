
-- Title_IdentifierUpdateAuto PROCEDURE
-- Generated 5/1/2012 2:41:41 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Title_Identifier

CREATE PROCEDURE Title_IdentifierUpdateAuto

@TitleIdentifierID INT,
@TitleID INT,
@IdentifierID INT,
@IdentifierValue NVARCHAR(125),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Title_Identifier]

SET

	[TitleID] = @TitleID,
	[IdentifierID] = @IdentifierID,
	[IdentifierValue] = @IdentifierValue,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[TitleIdentifierID] = @TitleIdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_IdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleIdentifierID],
		[TitleID],
		[IdentifierID],
		[IdentifierValue],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[Title_Identifier]
	
	WHERE
		[TitleIdentifierID] = @TitleIdentifierID
	
	RETURN -- update successful
END


