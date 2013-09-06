
-- NameIdentifierUpdateAuto PROCEDURE
-- Generated 9/18/2012 11:59:15 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for NameIdentifier

CREATE PROCEDURE NameIdentifierUpdateAuto

@NameIdentifierID INT,
@NameResolvedID INT,
@IdentifierID INT,
@IdentifierValue NVARCHAR(100),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[NameIdentifier]

SET

	[NameResolvedID] = @NameResolvedID,
	[IdentifierID] = @IdentifierID,
	[IdentifierValue] = @IdentifierValue,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[NameIdentifierID] = @NameIdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameIdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[NameIdentifierID],
		[NameResolvedID],
		[IdentifierID],
		[IdentifierValue],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[NameIdentifier]
	
	WHERE
		[NameIdentifierID] = @NameIdentifierID
	
	RETURN -- update successful
END

